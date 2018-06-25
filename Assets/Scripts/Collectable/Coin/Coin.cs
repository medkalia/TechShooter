using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float timeToDespawn = 30f ;
    private float timer;
    private float nextTimeMilestone = 0;
    private float milestonePercentage = 20f;
    private int despawnLevel = 0;
    private float timeSteP = 0;
    // Use this for initialization
    void Start () {
        timer = 0;
        AudioManager.instance.PlaySoundOnce(AudioParams.SoundPoolGroups.COIN, AudioParams.SoundPools.SPAWNING, gameObject);
        timeSteP  = ((milestonePercentage * timeToDespawn) / 100);
        nextTimeMilestone = timeSteP;
        StartCoroutine(CountDespawnLevels());
        if (Player.Instance != null)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
    }
	
	// Update is called once per frame
	void Update () {  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Coin")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
    }

    private IEnumerator CountDespawnLevels()
    {
        while (timer < timeToDespawn)
        {
            yield return new WaitForSeconds(timeSteP);
            timer += timeSteP;
            gameObject.GetComponent<Animator>().SetInteger("despawnLevel", ++despawnLevel);
        }
        Destroy(gameObject);
    }
}
