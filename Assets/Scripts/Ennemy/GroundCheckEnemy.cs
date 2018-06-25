using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckEnemy : MonoBehaviour {

    //**** Public variables
    public string groundTagName = "Ground";

    //**** Private variables
    private Enemy enemy;

	void Start(){
        enemy = gameObject.GetComponentInParent<Enemy> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == groundTagName)
        {
            enemy.enemyMovementInfo.isGrounded = true;
        }
            
        
	}

	void OnTriggerStay2D(Collider2D col) {
        if (col.tag == groundTagName)
            enemy.enemyMovementInfo.isGrounded = true;
	}

	void OnTriggerExit2D(Collider2D col) {
        if (col.tag == groundTagName)
            enemy.enemyMovementInfo.isGrounded = false;
	}

}
