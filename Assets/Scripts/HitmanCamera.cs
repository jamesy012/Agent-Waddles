using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmanCamera : MonoBehaviour {

	public bool m_Zoomed = false;

	public float m_MoveSpeed = 10;
	public float m_RotateSpeed = 5;

	public Transform m_Target;
	public Vector3 m_Offset;

	public float m_Distance;
	public float m_ZoomDistance;

	public Vector2 m_RotateSpeedScale = new Vector3(5, 5);

	private Quaternion m_DesiredRotation;
	private Vector3 m_DesiredPosition;

	// Use this for initialization
	void Start() {
		m_DesiredRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void LateUpdate() {
		updateRotation();
		updatePosition();
	}

	void updateRotation() {
		Vector3 mouseMovement = new Vector3(0, 0, 0);
		Vector3 rotateSpeedScale = new Vector3();

		if (m_Zoomed) {
			//todo change to another values when zoomed
			rotateSpeedScale = m_RotateSpeedScale;
		} else {
			rotateSpeedScale = m_RotateSpeedScale;
		}


        mouseMovement.y = Input.GetAxis("Mouse X") * rotateSpeedScale.x;
        mouseMovement.x = Input.GetAxis("Mouse Y") * -rotateSpeedScale.y;

        Quaternion mouseMovementQuat = Quaternion.Euler(mouseMovement);

		m_DesiredRotation *= mouseMovementQuat;

		transform.rotation = Quaternion.Slerp(transform.rotation, m_DesiredRotation, Time.deltaTime * m_RotateSpeed);

		Vector3 removeZ = transform.rotation.eulerAngles;
		removeZ.z = 0;
		transform.rotation = Quaternion.Euler(removeZ);
	}

	void updatePosition() {
		m_DesiredPosition = calcDesiredPos();

		transform.position = Vector3.LerpUnclamped(transform.position, m_DesiredPosition, Time.deltaTime* m_MoveSpeed);
    }

	private Vector3 calcOffsetPos() {
		return m_Target.position + m_Target.rotation * m_Offset;
	}

	private Vector3 calcDesiredPos() {
		//todo fix camera when belly diving
		//perhaps have a empty object that has the same position but only has the Y component of the target?

		//todo dynamically calculate distance by checking if were going to hit stuff ie: raycast behind, and if were hitting something then lower the distance

		Vector3 targetForward = Vector3.Scale(m_Target.forward, new Vector3(1, 0, 1));

		//todo have camera slightly rotate up and down
		//Vector3 upDownRot = transform.rotation.eulerAngles;
		//upDownRot.y = upDownRot.z = 0;
		//Quaternion ourRot = Quaternion.Euler(upDownRot);
		//return calcOffsetPos() - (ourRot * targetForward * m_Distance);
		//^ semi working implementation

		return calcOffsetPos() - (targetForward * m_Distance);
	}

	public void OnDrawGizmosSelected() {
        float gizmoSize = 0.25f;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(calcOffsetPos(), gizmoSize);
        Gizmos.DrawLine(calcOffsetPos(), calcDesiredPos());
        Gizmos.DrawSphere(calcDesiredPos(), gizmoSize);
    }
}
