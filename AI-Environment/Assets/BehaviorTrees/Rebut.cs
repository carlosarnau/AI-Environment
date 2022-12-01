using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework; 

[Condition("MyActions/Rebut?")]
[Help("Checks bool Valor.")]

public class Rebut : ConditionBase
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject go;

    public override bool Check()
    {
        Target tgt = go.GetComponent<Target>(); 
        return tgt.Activat;
    }
} 

