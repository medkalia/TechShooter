using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {
    //**** Public variables
    [Header("Player Interaction")]
    public string targetTag = "Player";
    public float originalSightSize;
    public string[] layersToDetectByRaycast = {"Player", "Obstacle"};
    private Enemy enemy;
    

    private void Start()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            originalSightSize = GetComponent<BoxCollider2D>().size.x;
        }
        enemy = (Enemy)GetComponentInParent(typeof(Enemy));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            RaycastHit2D hitInfo;          
            LayerMask mask = LayerMask.GetMask(layersToDetectByRaycast);
            Vector2 rayDirection = other.transform.position - transform.position;
            hitInfo = Physics2D.Raycast(transform.parent.position, rayDirection, Vector3.Distance(transform.parent.position, other.transform.position), mask);
            if (hitInfo)
            {
                if (hitInfo.transform.tag != targetTag)
                {
                    enemy.target = null;
                }
                else
                {
                    enemy.target = other.gameObject;
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            RaycastHit2D hitInfo;
            LayerMask mask = LayerMask.GetMask(layersToDetectByRaycast);
            Vector2 rayDirection = other.transform.position - transform.position;
            hitInfo = Physics2D.Raycast(transform.parent.position, rayDirection, Vector3.Distance(transform.parent.position, other.transform.position), mask);
            if (hitInfo)
            {
                if (hitInfo.transform.tag != targetTag)
                {
                    enemy.target = null;
                }
                else
                {
                    enemy.target = other.gameObject;
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            enemy.target = null;
        }
    }
}
