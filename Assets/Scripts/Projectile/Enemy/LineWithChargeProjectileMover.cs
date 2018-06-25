using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWithChargeProjectileMover : EnemyProjectileMover {


    override protected void Start()
    {
        base.Start();
        projectileTransofrm.localScale = new Vector3(projectileTransofrm.localScale.x , projectileTransofrm.localScale.y, projectileTransofrm.localScale.z);
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerHitBoxTag )
        {
            Destroy(gameObject);
        }
    }
    public void Release()
    {
        AudioManager audioManager;
        audioManager = AudioManager.instance;
        audioManager.PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.MELEEEFFECT, gameObject);
        projectileRB.velocity = Vector2.right * projectileData.speed * Mathf.Sign(enemy.enemyMovementInfo.FacingDirection) * -1;//-1 specific to volibear
        GetComponentInChildren<Animator>().SetTrigger(parameterForwardHash);
        gameObject.transform.parent = null;
    }

    //trigger parameters
    int parameterForwardHash = Animator.StringToHash("forward");
}
