using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingNew : MonoBehaviour {

    public List<Transform> backgrounds = new List<Transform>();
    public List<float> parallaxScales = new List<float>();
    public float smoothing;

    private Vector3 previousCameraPosition;


	void Start () {
        previousCameraPosition = transform.position;

        for (int i=0; i < backgrounds.Count; i++)
        {
            parallaxScales.Add(backgrounds[i].transform.position.z * -1);
        }
    }
	
	void LateUpdate () {

        for (int i = 0; i < backgrounds.Count; i++)
        {
            float parallax = (previousCameraPosition.x - transform.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            previousCameraPosition = transform.position;
        }

        //for (int i = 0; i < backgrounds.Count; i++)
        //{
        //    Vector3 parallax = (previousCameraPosition - transform.position) * (parallaxScales[i] / smoothing);

        //    backgrounds[i].position = new Vector3(
        //        backgrounds[i].position.x + parallax.x,
        //        backgrounds[i].position.y + parallax.y,
        //        backgrounds[i].position.z);
        //}

        //previousCameraPosition = transform.position;
    }
}
