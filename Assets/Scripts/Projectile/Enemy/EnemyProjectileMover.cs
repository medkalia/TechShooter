using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMover : MonoBehaviour {

    public ProjectileData projectileData;

    protected Rigidbody2D projectileRB;
    protected Transform projectileTransofrm;
    protected Enemy enemy;
    protected GameObject player;
    public string playerHitBoxTag = "Player";
    public string groundTag = "Ground";
    protected float startTime;

    protected virtual void Start()
    {
        player = Player.Instance.gameObject;
        startTime = Time.time;
        enemy = gameObject.GetComponentInParent<Enemy>();
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
        projectileTransofrm = gameObject.GetComponent<Transform>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerHitBoxTag || other.tag == groundTag)
        {
            
            gameObject.GetComponentInChildren<Animator>().SetTrigger(parameterDestroyHash);
            projectileRB.velocity = Vector2.zero;
        }
    }

    public void Destroy()
    {
        AudioSource audio = gameObject.GetComponentInChildren<AudioSource>();
        Collider2D collider = gameObject.GetComponent<Collider2D>();

        if (audio.clip.length > Time.time - startTime)
        {
            Destroy(collider);
            Destroy(gameObject, audio.clip.length - (Time.time - startTime));
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [HideInInspector]
    public int parameterDestroyHash = Animator.StringToHash("destroy");

}
