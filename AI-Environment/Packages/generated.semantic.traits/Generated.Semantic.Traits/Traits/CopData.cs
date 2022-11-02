using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct CopData : ITraitData, IEquatable<CopData>
    {

        public bool Equals(CopData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Cop";
        }
    }
}
