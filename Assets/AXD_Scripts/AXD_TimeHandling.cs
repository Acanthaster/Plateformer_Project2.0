using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_TimeHandling : MonoBehaviour
{
    private static int seconds;
    public bool counting;
    private Coroutine countingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        counting = true;
    }

    public void StartCounting()
    {
        countingCoroutine = StartCoroutine(CountSeconds());
    }

    public void StopCounting()
    {
        StopCoroutine(countingCoroutine);
    }
    IEnumerator CountSeconds()
    {
        while (counting)
        { 
            yield return new WaitForSeconds(1);
            
            seconds++;

        }
    }
    public int GetSeconds()
    {
        return seconds;
    }

    public string GetTime()
    {
        return (seconds/60)+" : "+(seconds%60);
    }


}
