using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Player Stats")]
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    
    public int startHealth;
    public int startMana;
    public int startStamina;

    void Awake()
    { 
       
    }

  
}