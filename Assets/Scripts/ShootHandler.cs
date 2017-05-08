using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{

    public Transform m_ShootPoint;
    public Transform m_ShootArm;
    public Vector3 m_ShootCameraOffsetRotation = new Vector3(90, -180, 0);
    public float m_ArmRotationSpeed = 5;
    private Quaternion m_StartingRot;
    bool isAiming = false;
    // Use this for initialization
    void Start()
    {
        m_StartingRot = m_ShootArm.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        isAiming = false;
        Quaternion desiredRotation;
        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
        }
        if (isAiming)
        {
            desiredRotation = Camera.main.transform.rotation * Quaternion.Euler(m_ShootCameraOffsetRotation);
            m_ShootArm.rotation = Quaternion.RotateTowards(m_ShootArm.rotation, desiredRotation, Time.deltaTime * m_ArmRotationSpeed);
        }
        else
        {
            m_ShootArm.localRotation = Quaternion.RotateTowards(m_ShootArm.localRotation, m_StartingRot, Time.deltaTime * m_ArmRotationSpeed);
        }


      
    }


    private void FixedUpdate()
    {
        if (isAiming)
        {

            Debug.DrawRay(m_ShootPoint.position, m_ShootPoint.forward * 1000, Color.green);
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 bulletTrajectory = m_ShootPoint.forward;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, m_ShootPoint.forward * 1000, out hit))
                {

                    bulletTrajectory = hit.point - m_ShootPoint.position;
                   
                }

                Debug.DrawRay(m_ShootPoint.position, bulletTrajectory * 1000, Color.red, 5);
            }
        }
    }
}
