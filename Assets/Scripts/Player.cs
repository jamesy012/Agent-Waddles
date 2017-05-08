using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    CharacterController m_characterController;
    public float m_speed;
    public bool m_walking;
    // Use this for initialization
    void Start()
    {
        m_characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //float rotationX = Input.GetAxis("Mouse X") * 5;

        //this.transform.Rotate(new Vector3(0, rotationX, 0), Space.Self);

        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;
        transform.rotation = Quaternion.LookRotation(camDir);

        movement = this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        movement.y = 0;

        if(movement.sqrMagnitude != 0.0f)
        {
            m_walking = true;
        }
        else
        {
            m_walking = false;
        }

        m_characterController.Move(movement.normalized * m_speed);
    }
}
