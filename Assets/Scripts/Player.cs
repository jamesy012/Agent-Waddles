using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    Rigidbody m_rigidBody;
    public float m_speed;
    // Use this for initialization
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float rotationX = Input.GetAxis("Mouse X") * 5;

        this.transform.Rotate(new Vector3(0, rotationX, 0), Space.Self);

        movement = this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        movement.y = 0;
        m_rigidBody.velocity = (movement * m_speed);
    }
}
