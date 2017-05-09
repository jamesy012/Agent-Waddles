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
        
        
        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }




      
    }


    private void FixedUpdate()
    {

		Vector3 position = Camera.main.transform.position;
		Vector3 forward = Camera.main.transform.forward;
		//Vector3 position = m_ShootPoint.position;
		//Vector3 forward = m_ShootPoint.forward;
		forward *= 1000;

		Vector3 bulletTrajectory;
        Quaternion desiredRotation;
        if (isAiming)
        {
            bulletTrajectory = Camera.main.transform.forward*1000 - m_ShootPoint.position;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 1000, out hit))
            {

                bulletTrajectory = hit.point - m_ShootPoint.position;

                
            }

            desiredRotation = Quaternion.LookRotation(bulletTrajectory.normalized) * Quaternion.Euler(m_ShootCameraOffsetRotation);
            m_ShootArm.rotation = Quaternion.RotateTowards(m_ShootArm.rotation, desiredRotation, Time.deltaTime * m_ArmRotationSpeed);

            Debug.DrawRay(position, forward, Color.green);
            if (Input.GetButtonDown("Fire1"))
            {

                Debug.DrawRay(position, forward, Color.red, 5);


            }
          

        }
        else
        {
            m_ShootArm.localRotation = Quaternion.RotateTowards(m_ShootArm.localRotation, m_StartingRot, Time.deltaTime * m_ArmRotationSpeed);
        }
    }
}
