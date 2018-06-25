using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject {

    //*****Physics
    [Header("Physics parameters")]
    public float acceleration = 25f;
    public float fallMultiplier = 2.5f;     //how much to multiply gravity when character is falling down
    public float lowJumpMultiplier = 2f;
    //*****Player stats
    [Header("Stats")]
    public float maxSpeed = 4.5f;
    public float jumpVelocity = 7f;
    public float fireRate = 0.7f;
    public float baseHealth = 100f;
    public float maxTechPoints = 10f;
    public float startingTechPoints = 2f;
    public float immortalityTime = 1.5f;
    public float techPointsRegenRate = 1f;
    public float recievedDamageAmplifier = 1f;

    [Space]
    [Header("Projectiles Stats")]
    //****Projectile stats
    public float projectileBaseDamage = 25f;
    public float projectileLifeTime = 1f;
    public float projectileSpeed = 9f;
}
