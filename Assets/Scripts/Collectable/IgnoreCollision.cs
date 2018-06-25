using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
#pragma warning disable CS0649 // Le champ 'IgnoreCollision.other' n'est jamais assigné et aura toujours sa valeur par défaut null
    private Collider2D other;
#pragma warning restore CS0649 // Le champ 'IgnoreCollision.other' n'est jamais assigné et aura toujours sa valeur par défaut null

    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
    }

}
