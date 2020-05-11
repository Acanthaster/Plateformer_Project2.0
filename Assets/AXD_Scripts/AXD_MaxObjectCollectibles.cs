using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_MaxObjectCollectibles : MonoBehaviour
{
    private static int maxCorn;
    private static int maxCacao;

    // Start is called before the first frame update
    private void Start()
    {
        
        if (this.CompareTag("Corn"))
        {
            maxCorn = transform.childCount;
        }
        else if (this.CompareTag("Cacao"))
        {
            
           maxCacao = transform.childCount;
            
        }
    }

    public int getMaxCorn()
    {
        return maxCorn;
    }

    public int getMaxCacao()
    {
        return maxCacao;
    }

}
