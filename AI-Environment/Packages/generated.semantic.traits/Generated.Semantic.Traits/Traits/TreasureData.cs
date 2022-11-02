using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct TreasureData : ITraitData, IEquatable<TreasureData>
    {

        public bool Equals(TreasureData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Treasure";
        }
    }
}
