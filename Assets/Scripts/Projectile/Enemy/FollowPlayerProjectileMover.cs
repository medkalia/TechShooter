using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerProjectileMover : EnemyProjectileMover {

    override protected void Start()
    {
        base.Start();
        projectileTransofrm.localScale = new Vector3(projectileTransofrm.localScale.x * enemy.enemyMovementInfo.FacingDirection, projectileTransofrm.localScale.y, projectileTransofrm.localScale.z);
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 90);
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * projectileData.speed;
        //Note that by using Slerp, you will get an eased rotation. If you don't want eased, change Slerp() to RotateTowards() and increase 'speed' (will be degrees per second).
        transform.parent = null;
    }

    void Update()
    {
        
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
