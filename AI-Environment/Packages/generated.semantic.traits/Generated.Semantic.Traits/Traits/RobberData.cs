using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct RobberData : ITraitData, IEquatable<RobberData>
    {
        public System.Boolean Ready2steal;
        public System.Boolean Stolen;
        public System.Boolean CopAway;

        public bool Equals(RobberData other)
        {
            return Ready2steal.Equals(other.Ready2steal) && Stolen.Equals(other.Stolen) && CopAway.Equals(other.CopAway);
        }

        public override string ToString()
        {
            return $"Robber: {Ready2steal} {Stolen} {CopAway}";
        }
    }
}
