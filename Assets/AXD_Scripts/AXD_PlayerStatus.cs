using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_PlayerStatus : MonoBehaviour
{
    float invincible;
    float invincibilityCoolDown;
    [Header("World")]
    public bool LivingWorld;
    public Vector2 LastCheckpoint;

    [Header("Health")]
    public int MaxHealthPoint;
    public int HealthPoint;

    [Header("Collectibles")]
    public int Corn;
    public int Cacao;

    public ALR_PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        invincibilityCoolDown = 1f;
        invincible = Time.deltaTime;
        LivingWorld = true;
        HealthPoint = MaxHealthPoint = 5;
        Corn = 0;
        Cacao = 0;
    }

    public void TakingDamage()
    {
        if (Time.time > invincible)
        {
            HealthPoint--;
            invincible = Time.time + invincibilityCoolDown;
        }
    }
    
}
