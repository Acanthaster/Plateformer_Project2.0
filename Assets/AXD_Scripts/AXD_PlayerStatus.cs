using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_PlayerStatus : MonoBehaviour
{
    float invincible;
    float invincibilityCoolDown;
    public bool dead;
    [Header("World")]
    public bool LivingWorld;
    public Vector2 LastCheckpoint;

    [Header("Health")]
    public int MaxHealthPoint;
    public int HealthPoint;

    [Header("Collectibles")]
    public int Corn;
    public int Cacao;

    private void Awake()
    {
        dead = false;
        LastCheckpoint = this.transform.position;
        invincibilityCoolDown = 1f;
        invincible = Time.deltaTime;
        LivingWorld = true;
        HealthPoint = MaxHealthPoint = 5;
        Corn = 0;
        Cacao = 0;
    }

    private void Update()
    {
        if (HealthPoint <= 0)
        {
            this.transform.position = LastCheckpoint;
            HealthPoint = MaxHealthPoint;
            dead = true;
        }
    }

    public void TakeDamage()
    {
        if (Time.time > invincible)
        {
            Debug.Log("Damage !");
            HealthPoint--;
            invincible = Time.time + invincibilityCoolDown;
        }
    }

    public void ChangeWorld()
    {
        LivingWorld = !LivingWorld;
    }
    
}
