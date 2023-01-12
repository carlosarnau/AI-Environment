using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Action("Cops/FindClosestCop")]
[Help("Get the Closest Free Cop.")]
public class BusyCopsChecker : BasePrimitiveAction
{
    [OutParam("game object")]
    [Help("Nearest free cop.")]
    public GameObject go;

    /*
    public override TaskStatus OnUpdate()
    {
        var l = GameObject.FindGameObjectsWithTag("Cop").Where(x => !x.GetComponent<Moves>().found);
        if (l.Count() == 0)
            return TaskStatus.FAILED;
        go = l.First();
        return TaskStatus.COMPLETED;
    }
    */
}
