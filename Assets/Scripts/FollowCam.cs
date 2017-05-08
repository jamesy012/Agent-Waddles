using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowCam : MonoBehaviour
{
    public GameObject target;
    public float damping = 1;
    Vector3 offset;
    float y;
    void Start()
    {
        
        offset = target.transform.position - transform.position;
    }

    private void Update()
    {

    }

    void LateUpdate()
    {

        y -= Input.GetAxis("Mouse Y") * 5 * 0.02f;

        y = ClampAngle(y, -90, 90);

       

        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, damping);

        Quaternion rotation = Quaternion.Euler(y, angle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}