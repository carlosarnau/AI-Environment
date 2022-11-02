using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Robber : ITrait, IBufferElementData, IEquatable<Robber>
    {
        public const string FieldReady2steal = "Ready2steal";
        public const string FieldStolen = "Stolen";
        public const string FieldCopAway = "CopAway";
        public System.Boolean Ready2steal;
        public System.Boolean Stolen;
        public System.Boolean CopAway;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(Ready2steal):
                    Ready2steal = (System.Boolean)value;
                    break;
                case nameof(Stolen):
                    Stolen = (System.Boolean)value;
                    break;
                case nameof(CopAway):
                    CopAway = (System.Boolean)value;
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Robber.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(Ready2steal):
                    return Ready2steal;
                case nameof(Stolen):
                    return Stolen;
                case nameof(CopAway):
                    return CopAway;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Robber.");
            }
        }

        public bool Equals(Robber other)
        {
            return Ready2steal == other.Ready2steal && Stolen == other.Stolen && CopAway == other.CopAway;
        }

        public override string ToString()
        {
            return $"Robber\n  Ready2steal: {Ready2steal}\n  Stolen: {Stolen}\n  CopAway: {CopAway}";
        }
    }
}
