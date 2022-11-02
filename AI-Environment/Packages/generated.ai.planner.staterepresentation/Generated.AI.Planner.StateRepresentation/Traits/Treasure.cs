using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Treasure : ITrait, IBufferElementData, IEquatable<Treasure>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait Treasure.");
        }

        public bool Equals(Treasure other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Treasure";
        }
    }
}
