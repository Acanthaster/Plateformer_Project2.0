using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AXD_LifeUI : MonoBehaviour
{

    public Image icon;
    public AXD_LifeSprites sp;
    public AXD_PlayerStatus status;
    Transform trans;
    int lastHP;
    void Start()
    {
        lastHP = status.MaxHealthPoint;
        trans = GetComponent<Transform>();
        for (int i = 0; i < status.MaxHealthPoint; i++)
        {
             
            Instantiate(icon, new Vector3(transform.position.x-GetComponent<RectTransform>().rect.xMax + icon.rectTransform.rect.width + i * 50,
                transform.position.y+icon.rectTransform.rect.height, transform.position.z), transform.rotation, transform);
        }
    }
    private void Update()
    {
        Debug.Log("Last HP : " + lastHP+"\nHP : "+status.HealthPoint);
        UI_Update();
    }

    public void UI_Update()
    {
        if (status.HealthPoint < lastHP)
        {
            for (int i = status.HealthPoint; i < lastHP; i++)
            {
                GameObject obj = trans.GetChild(i).gameObject;
                if(obj != null)
                {
                    obj.GetComponent<AXD_LifeSprites>().ChangeSprite(false);
                }
                
            }
        }else if(status.HealthPoint > lastHP)
        {
            for (int i = lastHP; i < status.HealthPoint; i++)
            {
                GameObject obj = trans.GetChild(i).gameObject;
                if (obj != null)
                {
                    obj.GetComponent<AXD_LifeSprites>().ChangeSprite(true);
                }

            }
        }
    }
}
