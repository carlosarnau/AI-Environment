using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.Burst;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.RobberPlan;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Generated.AI.Planner.Plans.RobberPlan
{
    [BurstCompile]
    struct Wander : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_CopIndex = 0;
        const int k_RobberIndex = 1;
        const int k_TreasureIndex = 2;
        const int k_MaxArguments = 3;

        public static readonly string[] parameterNames = {
            "Cop",
            "Robber",
            "Treasure",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> CopFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> CopObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> RobberFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> RobberObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> TreasureFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> TreasureObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<WanderFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Wander(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            CopFilter = default;
            CopObjectIndices = default;
            RobberFilter = default;
            RobberObjectIndices = default;
            TreasureFilter = default;
            TreasureObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            CopFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Cop>(),[1] = ComponentType.ReadWrite<Location>(),  };
            CopObjectIndices = new NativeList<int>(2, Allocator.Temp);
            RobberFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Robber>(),[1] = ComponentType.ReadWrite<Location>(),  };
            RobberObjectIndices = new NativeList<int>(2, Allocator.Temp);
            TreasureFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Treasure>(),[1] = ComponentType.ReadWrite<Location>(),  };
            TreasureObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<WanderFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "Cop", StringComparison.OrdinalIgnoreCase))
                 return k_CopIndex;
            if (string.Equals(parameterName, "Robber", StringComparison.OrdinalIgnoreCase))
                 return k_RobberIndex;
            if (string.Equals(parameterName, "Treasure", StringComparison.OrdinalIgnoreCase))
                 return k_TreasureIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            CopObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(CopObjectIndices, CopFilter);
            
            RobberObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(RobberObjectIndices, RobberFilter);
            
            TreasureObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(TreasureObjectIndices, TreasureFilter);
            
            var RobberBuffer = stateData.RobberBuffer;
            
            

            for (int i0 = 0; i0 < CopObjectIndices.Length; i0++)
            {
                var CopIndex = CopObjectIndices[i0];
                var CopObject = stateData.TraitBasedObjects[CopIndex];
                
                
                
                
            
            

            for (int i1 = 0; i1 < RobberObjectIndices.Length; i1++)
            {
                var RobberIndex = RobberObjectIndices[i1];
                var RobberObject = stateData.TraitBasedObjects[RobberIndex];
                
                if (!(RobberBuffer[RobberObject.RobberIndex].CopAway == false))
                    continue;
                
                
                
            
            

            for (int i2 = 0; i2 < TreasureObjectIndices.Length; i2++)
            {
                var TreasureIndex = TreasureObjectIndices[i2];
                var TreasureObject = stateData.TraitBasedObjects[TreasureIndex];
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_CopIndex] = CopIndex,
                                                       [k_RobberIndex] = RobberIndex,
                                                       [k_TreasureIndex] = TreasureIndex,
                                                    };
                argumentPermutations.Add(actionKey);
            
            }
            
            }
            
            }
        }

        StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> ApplyEffects(ActionKey action, StateEntityKey originalStateEntityKey)
        {
            var originalState = m_StateDataContext.GetStateData(originalStateEntityKey);
            var originalStateObjectBuffer = originalState.TraitBasedObjects;
            var originalRobberObject = originalStateObjectBuffer[action[k_RobberIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newRobberBuffer = newState.RobberBuffer;
            {
                    var @Robber = newRobberBuffer[originalRobberObject.RobberIndex];
                    @Robber.@CopAway = true;
                    newRobberBuffer[originalRobberObject.RobberIndex] = @Robber;
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = 0f;

            return reward;
        }

        public void Execute(int jobIndex)
        {
            if (!LocalContainersInitialized)
                InitializeLocalContainers();

            m_StateDataContext.JobIndex = jobIndex;

            var stateEntityKey = m_StatesToExpand[jobIndex];
            var stateData = m_StateDataContext.GetStateData(stateEntityKey);

            ArgumentPermutations.Clear();
            GenerateArgumentPermutations(stateData, ArgumentPermutations);

            TransitionInfo.Clear();
            TransitionInfo.Capacity = math.max(TransitionInfo.Capacity, ArgumentPermutations.Length);
            for (var i = 0; i < ArgumentPermutations.Length; i++)
            {
                TransitionInfo.Add(new WanderFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<WanderFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetCopTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_CopIndex]);
        }
        
        public static T GetRobberTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_RobberIndex]);
        }
        
        public static T GetTreasureTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_TreasureIndex]);
        }
        
    }

    public struct WanderFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


