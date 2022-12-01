using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Treasure gone?")]
[Help("Checks whether Cop is near the Treasure.")]

public class IsTreasureGone : ConditionBase
{
    [InParam("Trasure")]
    public GameObject go;

    public override bool Check()
    {
        return !go.activeSelf;
    }
}
