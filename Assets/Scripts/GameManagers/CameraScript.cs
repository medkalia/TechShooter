using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    public Transform target;
    public Vector2 horizontalLimits = new Vector2 (9,81);
    public Vector2 verticalLimits = new Vector2(5.5f, 8);
    public float smoothSpeed = 0.125f;
    public bool hasMoved = false;
    public Vector3 offset;
    private Vector3 lastPosition;

    private void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(
            target.position.x + (offset.x * Player.Instance.movementInfo.facingDirection),
            target.position.y,
            target.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(
                (new Vector3(transform.position.x, transform.position.y, transform.position.z)),
                (new Vector3(Mathf.Clamp(desiredPosition.x, horizontalLimits.x, horizontalLimits.y), Mathf.Clamp(desiredPosition.y, verticalLimits.x, verticalLimits.y), transform.position.z)),
                smoothSpeed);
            transform.position = smoothedPosition;
            if (lastPosition != transform.position) hasMoved = true;
            else hasMoved = false;
            lastPosition = transform.position;
        }


    }

    private void Start()
    {
        lastPosition = transform.position;
        GetComponent<Camera>().aspect = 1.77f;
    }
}
