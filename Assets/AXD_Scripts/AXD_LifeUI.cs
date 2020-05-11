using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AXD_LifeUI : MonoBehaviour
{

    public Image icon;
    public AXD_LifeSprites sp;
    public AXD_PlayerStatus status;
    GameObject lifePoints;
    int lastHP;
    void Start()
    {
        lastHP = status.MaxHealthPoint;
        lifePoints = new GameObject("LifePoints");
        lifePoints.transform.SetParent(this.transform);
        //Debug.Log("X : " + (transform.position.x - GetComponent<RectTransform>().rect.width / 2 + icon.sprite.rect.width / 2));
        //Debug.Log("Y : " + (transform.position.y + GetComponent<RectTransform>().rect.height / 2 - icon.sprite.rect.height / 2));
        lifePoints.transform.position = new Vector2(transform.position.x - GetComponent<RectTransform>().rect.width / 2 + icon.sprite.rect.width,
            transform.position.y + GetComponent<RectTransform>().rect.height / 2 - icon.sprite.rect.height);
        Debug.Log("Max HP : "+ status.MaxHealthPoint);
        for (int i = 0; i < status.MaxHealthPoint; i++)
        {
            Instantiate(icon, new Vector3(transform.position.x-GetComponent<RectTransform>().rect.xMax + icon.rectTransform.rect.width + i * 50,
                transform.position.y+icon.rectTransform.rect.height, transform.position.z), transform.rotation, lifePoints.transform);
        }
    }
    private void Update()
    {
        //Debug.Log("Last HP : " + lastHP+"\nHP : "+status.HealthPoint);
        UI_Update();
    }

    public void UI_Update()
    {
        //Si les PV réels sont inférieurs aux PV affichés
        if(status.dead)
        {
            for(int i = 0; i < status.MaxHealthPoint; i++)
            {
                GameObject obj = lifePoints.transform.GetChild(i).gameObject;
                if (obj != null)
                {
                    obj.GetComponent<AXD_LifeSprites>().ChangeSprite(true);
                }
            }
            lastHP = status.MaxHealthPoint;
            status.dead = false;
        }
        else if (status.HealthPoint <= lastHP)
        {
            //Pour chaque PV qui sont mal affichés (affichés rempli alors qu'ils doivent être vide)
            for (int i = status.HealthPoint; i < lastHP; i++)
            {
                GameObject obj = lifePoints.transform.GetChild(i).gameObject;
                if (obj != null)
                {
                    obj.GetComponent<AXD_LifeSprites>().ChangeSprite(false);
                }
                lastHP = i;

            }
            //Si les PV réels sont suppérieurs aux PV indiqués
        }
        else if (status.HealthPoint > lastHP)
        {
            for (int i = lastHP; i < status.HealthPoint; i++)
            {
                GameObject obj = lifePoints.transform.GetChild(i).gameObject;
                if (obj != null)
                {
                    obj.GetComponent<AXD_LifeSprites>().ChangeSprite(true);

                }
                lastHP = i;

            }
        }
    }
}
