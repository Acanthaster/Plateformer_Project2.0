using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AXD_UIText : MonoBehaviour
{
    public AXD_PlayerStatus status;
    Text display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag.Equals("Corn") && !status.Corn.ToString().Equals(display.text))
        {
            AXD_MaxObjectCollectibles corns = GameObject.Find("Corns").GetComponent<AXD_MaxObjectCollectibles>();
            display.text = status.Corn.ToString()+" / "+corns.getMaxCorn();

        }else if (this.tag.Equals("Cacao") && !status.Cacao.ToString().Equals(display.text))
        {
            AXD_MaxObjectCollectibles cacaos = GameObject.Find("Cacaos").GetComponent<AXD_MaxObjectCollectibles>();
            display.text = status.Cacao.ToString()+" / "+cacaos.getMaxCacao();
        }
    }
}
