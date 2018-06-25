using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMover : MonoBehaviour {



    public string enemyHitBoxTag = "Enemy";
    public string groundTag = "Ground";


    //**** Private variables
    private Rigidbody2D boltRB;
    private Transform boltTransofrm;
    private float startTime;


    void Start()
    {
        startTime = Time.time;
        AudioManager.instance.PlaySoundOnce(AudioParams.SoundPoolGroups.PLAYER, AudioParams.SoundPools.RANGED,gameObject);
        boltRB = gameObject.GetComponent<Rigidbody2D>();
        boltTransofrm = gameObject.GetComponent<Transform>();
        boltTransofrm.localScale = new Vector3(boltTransofrm.localScale.x * Player.Instance.movementInfo.facingDirection, boltTransofrm.localScale.x, boltTransofrm.localScale.x);
        boltRB.velocity = Vector2.right * Player.Instance.projectileStats.projectileSpeed * Mathf.Sign(Player.Instance.movementInfo.facingDirection) ;
        transform.parent = null;
        Destroy(gameObject, Player.Instance.projectileStats.projectileLifeTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.tag == enemyHitBoxTag || other.tag == groundTag)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            if (audio != null && audio.clip.length > Time.time - startTime)
            {
                SpriteRenderer vfx = gameObject.GetComponentInChildren<SpriteRenderer>();
                Destroy(vfx);
                Destroy(collider);
                Destroy(gameObject, audio.clip.length - (Time.time - startTime));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
