using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrier : MonoBehaviour {

    public string enemiesProjectileTag;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemiesProjectileTag)
        {
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySoundOnce(AudioParams.SoundPoolGroups.PLAYER, AudioParams.SoundPools.MELEEEFFECT, gameObject);
        }
            
    }
}
