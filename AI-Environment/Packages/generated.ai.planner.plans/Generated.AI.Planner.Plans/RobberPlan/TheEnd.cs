using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.RobberPlan;

namespace Generated.AI.Planner.Plans.RobberPlan
{
    public struct TheEnd
    {
        public bool IsTerminal(StateData stateData)
        {
            var RobberFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Robber>(),  };
            var RobberObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(RobberObjectIndices, RobberFilter);
            var RobberBuffer = stateData.RobberBuffer;
            for (int i0 = 0; i0 < RobberObjectIndices.Length; i0++)
            {
                var RobberIndex = RobberObjectIndices[i0];
                var RobberObject = stateData.TraitBasedObjects[RobberIndex];
            
                
                if (!(RobberBuffer[RobberObject.RobberIndex].Stolen == true))
                    continue;
                RobberObjectIndices.Dispose();
                RobberFilter.Dispose();
                return true;
            }
            RobberObjectIndices.Dispose();
            RobberFilter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 0f;

            return reward;
        }
    }
}
