using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //  Start is called before the first frame update
    void Start()
    {}

    public bool fog;
    float fogDensity = 0.005f;

    //  Update is called once per frame
    void Update()
    {
        if (fog == true) RenderSettings.fog = true;
        else RenderSettings.fog = false;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogColor = Color.blue;
    }
}
