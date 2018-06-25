using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : ScriptableObject
{

    [Space]
    [Header("Projectiles Stats")]
    //****Projectile stats
    public float baseDamage = 25f;
    public float lifeTime = 1f;
    public float speed = 9f;
}
