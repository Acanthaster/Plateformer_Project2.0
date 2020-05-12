using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions { up, right, down, left };
public class AXD_Launcher : MonoBehaviour
{

    public Transform toLaunch;
    public float launchingRythm;
    private float lastLaunch;
    public Directions direction;
    // Start is called before the first frame update
    void Start()
    {
        lastLaunch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastLaunch + launchingRythm)
        {
            Launch();
            lastLaunch = Time.time;
        }
    }

    void Launch()
    {
        Transform arrow = Instantiate(toLaunch, transform);
        arrow.GetComponent<AXD_Arrow>().SetDirection(direction);
    }
}
