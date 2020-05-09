using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_FireRain : MonoBehaviour
{
    public List<Sprite> sprites;
    public AXD_PlayerStatus pStatus;
    SpriteRenderer display;
    public bool damaging;
    bool world;
    // Start is called before the first frame update
    void Start()
    {
        world = true;
        damaging = true;
        display = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pStatus.LivingWorld != world)
        {
            world = pStatus.LivingWorld;
            if (world)
            {
                damaging = true;
                display.sprite = sprites[0];
            }
            else
            {
                damaging = false;
                display.sprite = sprites[1];
            }
        }
    }
}
