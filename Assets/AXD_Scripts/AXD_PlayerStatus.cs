using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_PlayerStatus : MonoBehaviour
{
    [Header("World")]
    public bool LivingWorld;

    [Header("Health")]
    public int HealthPoint;

    [Header("Collectibles")]
    public int Corn;
    public int Cacao;

    public ALR_PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        LivingWorld = true;
        HealthPoint = 5;
        Corn = 0;
        Cacao = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
