using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow : MonoBehaviour
{

    //Credit: Brackeys - Youtube
    //Link: https://www.youtube.com/watch?v=MFQhpwc6cKE

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }

    public void CamToRight(bool b)
    {
        float curX;

        curX = offset.x;
        if (b)
        {
            if(curX < 0)
            {
                offset.x = -curX;
            }
        }
        else
        {
            if(curX > 0)
            {
                offset.x = -curX;
            }
        }
    }
}
