using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Target : MonoBehaviour
{
    public bool Activat = false;

    public void Activar()
    {
        Debug.Log("Activat!");
        Activat = true;
    }

    public void Rebre()
    {
        Debug.Log("Rebut!");
    }
}
