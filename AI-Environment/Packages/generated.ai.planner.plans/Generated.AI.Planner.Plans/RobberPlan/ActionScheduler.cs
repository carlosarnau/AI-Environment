using System;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.AI.Planner.Jobs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.RobberPlan;

namespace Generated.AI.Planner.Plans.RobberPlan
{
    public struct ActionScheduler :
        ITraitBasedActionScheduler<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager, ActionKey>
    {
        public static readonly Guid WanderGuid = Guid.NewGuid();
        public static readonly Guid ApproachGuid = Guid.NewGuid();
        public static readonly Guid StealGuid = Guid.NewGuid();

        // Input
        public NativeList<StateEntityKey> UnexpandedStates { get; set; }
        public StateManager StateManager { get; set; }

        // Output
        NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> IActionScheduler<StateEntityKey, StateData, StateDataContext, StateManager, ActionKey>.CreatedStateInfo
        {
            set => m_CreatedStateInfo = value;
        }

        NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> m_CreatedStateInfo;

        struct PlaybackECB : IJob
        {
            public ExclusiveEntityTransaction ExclusiveEntityTransaction;

            [ReadOnly]
            public NativeList<StateEntityKey> UnexpandedStates;
            public NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> CreatedStateInfo;
            public EntityCommandBuffer WanderECB;
            public EntityCommandBuffer ApproachECB;
            public EntityCommandBuffer StealECB;

            public void Execute()
            {
                // Playback entity changes and output state transition info
                var entityManager = ExclusiveEntityTransaction;

                WanderECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var WanderRefs = entityManager.GetBuffer<WanderFixupReference>(stateEntity);
                    for (int j = 0; j < WanderRefs.Length; j++)
                        CreatedStateInfo.Enqueue(WanderRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(WanderFixupReference));
                }

                ApproachECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var ApproachRefs = entityManager.GetBuffer<ApproachFixupReference>(stateEntity);
                    for (int j = 0; j < ApproachRefs.Length; j++)
                        CreatedStateInfo.Enqueue(ApproachRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(ApproachFixupReference));
                }

                StealECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var StealRefs = entityManager.GetBuffer<StealFixupReference>(stateEntity);
                    for (int j = 0; j < StealRefs.Length; j++)
                        CreatedStateInfo.Enqueue(StealRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(StealFixupReference));
                }
            }
        }

        public JobHandle Schedule(JobHandle inputDeps)
        {
            var entityManager = StateManager.ExclusiveEntityTransaction.EntityManager;
            var WanderDataContext = StateManager.StateDataContext;
            var WanderECB = StateManager.GetEntityCommandBuffer();
            WanderDataContext.EntityCommandBuffer = WanderECB.AsParallelWriter();
            var ApproachDataContext = StateManager.StateDataContext;
            var ApproachECB = StateManager.GetEntityCommandBuffer();
            ApproachDataContext.EntityCommandBuffer = ApproachECB.AsParallelWriter();
            var StealDataContext = StateManager.StateDataContext;
            var StealECB = StateManager.GetEntityCommandBuffer();
            StealDataContext.EntityCommandBuffer = StealECB.AsParallelWriter();

            var allActionJobs = new NativeArray<JobHandle>(4, Allocator.TempJob)
            {
                [0] = new Wander(WanderGuid, UnexpandedStates, WanderDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [1] = new Approach(ApproachGuid, UnexpandedStates, ApproachDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [2] = new Steal(StealGuid, UnexpandedStates, StealDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [3] = entityManager.ExclusiveEntityTransactionDependency
            };

            var allActionJobsHandle = JobHandle.CombineDependencies(allActionJobs);
            allActionJobs.Dispose();

            // Playback entity changes and output state transition info
            var playbackJob = new PlaybackECB()
            {
                ExclusiveEntityTransaction = StateManager.ExclusiveEntityTransaction,
                UnexpandedStates = UnexpandedStates,
                CreatedStateInfo = m_CreatedStateInfo,
                WanderECB = WanderECB,
                ApproachECB = ApproachECB,
                StealECB = StealECB,
            };

            var playbackJobHandle = playbackJob.Schedule(allActionJobsHandle);
            entityManager.ExclusiveEntityTransactionDependency = playbackJobHandle;

            return playbackJobHandle;
        }
    }
}
