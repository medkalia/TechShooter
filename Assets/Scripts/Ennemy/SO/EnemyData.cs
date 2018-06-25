using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject {


    //*****Enemy stats
    [Header("Stats")]
    public float currentHealth = 100f;
    public float baseHealth = 100f;
    public float contactDamage = 35f;
    public float meleeDamage = 40f;
    public float movementSpeed = 3f;
    public float rangedCD = 1f;
    public float rangedRange = 7f;
    public float meleeCD = 2f;
    public float meleeRange = 2f;
}
