using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSholderCam : MonoBehaviour {

	public Transform m_Target;
	public Vector3 m_Offset;

	public float m_MaxXRot = 75;

	public float m_Distance;
	//local distance from targets forward
	public Vector3 m_DistanceScale = new Vector3(1,1,1);

	public Vector2 m_RotateSpeedScale = new Vector3(5, 5);
	public Vector2 m_RotateSpeedScaleGunOut = new Vector3(2, 2);
	public bool m_InvertY = true;

	public bool m_StopCameraInput = false;
	public Vector3 mouseMovement;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Vector3 rotateSpeedScale;
		if (Input.GetButton("Fire2")) {
			rotateSpeedScale = m_RotateSpeedScaleGunOut;
		} else {
			rotateSpeedScale = m_RotateSpeedScale;
		}


		if (m_InvertY) {
			rotateSpeedScale.y *= -1;
		}

		if (!m_StopCameraInput) {
			mouseMovement.y += Input.GetAxis("Mouse X") * rotateSpeedScale.x;
			mouseMovement.x += Input.GetAxis("Mouse Y") * rotateSpeedScale.y;
		}

		mouseMovement.x = Mathf.Clamp(mouseMovement.x, -m_MaxXRot, m_MaxXRot);
		

		transform.rotation =  Quaternion.Euler(mouseMovement);
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.z = 0;
		//rotation.x = rotation.x > 80 ? 80 : rotation.x < -80 ? -80 : rotation.x;
		//this may not be nessary anymore since i am adding the rotations in a different way now
		if (rotation.x < 180) {
			if (rotation.x > m_MaxXRot) {
				rotation.x = m_MaxXRot;
			}
		} else {
			if (rotation.x < 360 - m_MaxXRot) {
				rotation.x = 360 - m_MaxXRot;
			}
		}
		transform.rotation = m_Target.rotation * Quaternion.Euler(rotation);

		float distance = m_Distance;


		transform.position = m_Target.transform.position + m_Target.transform.rotation * m_Offset + Vector3.Scale(transform.rotation * - m_Target.forward,m_DistanceScale) * m_Distance ;
		//transform.position = m_Target.transform.position + m_Offset + Vector3.Scale(transform.rotation * -m_Target.forward, m_DistanceScale) * m_Distance;
	}

	public static float ClampAngle(float angle, float min, float max) {
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
