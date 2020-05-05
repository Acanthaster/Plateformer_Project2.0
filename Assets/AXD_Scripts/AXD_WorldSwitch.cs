using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AXD_WorldSwitch : MonoBehaviour
{
    public AXD_PlayerStatus PlayerStatus;
    TilemapRenderer mapRenderer;
    TilemapCollider2D mapCollider;
    [Header("Active World")]
    //If livingWorld is true, then the player is in the world of the livings, 
    public bool CurrentWorld;

    // Start is called before the first frame update
    void Start()
    {
        mapRenderer = GetComponent<TilemapRenderer>();
        mapCollider = GetComponent<TilemapCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

        //If we are in the living world tilemap
        if(gameObject.name == "LivingWorld")
        {
            //If the player is in the living world
            if(PlayerStatus.LivingWorld)
            {
                if (!mapRenderer.enabled && !mapCollider.enabled)
                {
                    //The player goes in the dead world
                    CurrentWorld = true;
                    mapRenderer.enabled = !mapRenderer.enabled;
                    mapCollider.enabled = !mapCollider.enabled;
                }

            }
            else
            {
                //The player goes in the living world
                if (mapRenderer.enabled && mapCollider.enabled)
                {
                    CurrentWorld = false;
                    mapRenderer.enabled = !mapRenderer.enabled;
                    mapCollider.enabled = !mapCollider.enabled;
                }

            }
        }else if(gameObject.name == "DeadWorld") // If we are in the dead world tilemap
        {
            //If the player is in the living world
            if (PlayerStatus.LivingWorld)
            {
                if (mapRenderer.enabled && mapCollider.enabled)
                {
                    //The player goes in the dead world
                    CurrentWorld = false;
                    mapRenderer.enabled = !mapRenderer.enabled;
                    mapCollider.enabled = !mapCollider.enabled;
                }

            }
            else
            {
                //The player goes in the living world
                if (!mapRenderer.enabled && !mapCollider.enabled)
                {
                    CurrentWorld = true;
                    mapRenderer.enabled = !mapRenderer.enabled;
                    mapCollider.enabled = !mapCollider.enabled;
                }
            }

        }
        
    }
}
