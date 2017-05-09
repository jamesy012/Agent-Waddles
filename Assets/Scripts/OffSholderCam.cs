using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSholderCam : MonoBehaviour
{

    public Transform m_Target;
    public Vector3 m_Offset;
    public Vector3 m_proneOffset;

    public float m_MaxXRot = 75;

    public float m_Distance;
    //local distance from targets forward
    public Vector3 m_DistanceScale = new Vector3(1, 1, 1);

    public Vector2 m_RotateSpeedScale = new Vector3(5, 5);
    public Vector2 m_RotateSpeedScaleGunOut = new Vector3(2, 2);
    public bool m_InvertY = true;

    public bool m_StopCameraInput = false;
    public Vector3 mouseMovement;

    bool isLookAt = true;

    bool m_prone = false;

    public bool IsLookAt
    {
        get
        {
            return isLookAt;
        }

        set
        {
            isLookAt = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotateSpeedScale;
        if (Input.GetButton("Fire2"))
        {
            rotateSpeedScale = m_RotateSpeedScaleGunOut;
        }
        else
        {
            rotateSpeedScale = m_RotateSpeedScale;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_prone ^= true;
        }

        if (m_InvertY)
        {
            rotateSpeedScale.y *= -1;
        }

        if (!m_StopCameraInput)
        {
            mouseMovement.y += Input.GetAxis("Mouse X") * rotateSpeedScale.x;
            mouseMovement.x += Input.GetAxis("Mouse Y") * rotateSpeedScale.y;
        }

        mouseMovement.x = Mathf.Clamp(mouseMovement.x, -m_MaxXRot, m_MaxXRot);


        if (isLookAt)
            transform.rotation = Quaternion.Euler(mouseMovement);

        float distance = m_Distance;

        if(m_prone)
        {
            transform.position = m_Target.transform.position + m_Target.transform.rotation * m_proneOffset + Vector3.Scale(transform.rotation * -m_Target.forward, m_DistanceScale) * m_Distance;
        }
        else
        {
            transform.position = m_Target.transform.position + m_Target.transform.rotation * m_Offset + Vector3.Scale(transform.rotation * -m_Target.forward, m_DistanceScale) * m_Distance;
        }
        
        //transform.position = m_Target.transform.position + m_Offset + Vector3.Scale(transform.rotation * -m_Target.forward, m_DistanceScale) * m_Distance;
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
