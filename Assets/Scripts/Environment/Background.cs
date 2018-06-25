using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public FreeParallax parallax;
    private Player player;
    public Transform clouds;
    public Transform mainCamera;


    // Use this for initialization
    void Start () {
        player = Player.Instance;
        clouds.GetComponent<Rigidbody2D>().velocity = new Vector2(0.35f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null && player.GetComponent<Rigidbody2D>() != null)
        {
            if (parallax != null && mainCamera.gameObject.GetComponent<CameraScript>().hasMoved)
            {
                parallax.Speed = player.GetComponent<Rigidbody2D>().velocity.x * -1;
            }
        }
        

    }
}
