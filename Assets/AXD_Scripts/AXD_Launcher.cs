using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Launcher : MonoBehaviour
{
    public Transform toLaunch;
    public float launchingRythm;
    private float lastLaunch;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        lastLaunch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastLaunch + launchingRythm)
        {
            if (!stop)
            {
                Launch();
                lastLaunch = Time.time;
                stop = true;
            }

        }
    }

    void Launch()
    {
        Instantiate(toLaunch, transform);
    }
}
