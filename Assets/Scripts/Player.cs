using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    Rigidbody m_rigidBody;
    public float m_jumpForce = 5;
    Quaternion m_proneRotation;
    public float m_waddleSpeed;
    public float m_proneSpeed;
    public bool m_walking;
    public bool m_jumping;
    public bool m_prone;
    public bool m_grounded;

    private CapsuleCollider m_collider;
    // Use this for initialization
    void Start()
    {
        m_proneRotation = Quaternion.LookRotation(-this.transform.up);
        m_collider = this.GetComponent<CapsuleCollider>();
        m_rigidBody = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            m_jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_prone ^= true;
        }



        
    }

    private void FixedUpdate()
    {
       
        m_grounded = Physics.Raycast(this.transform.position , Vector3.down,1.05f);
        Debug.DrawRay(this.transform.position , Vector3.down *2, Color.cyan);

        movement = this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical");
        movement.y = 0;

        if (movement.sqrMagnitude != 0.0f)
        {
            m_walking = true;
        }
        else
        {
            //less slippery
            //m_rigidBody.velocity -= new Vector3(m_rigidBody.velocity.x * 0.9f, 0, m_rigidBody.velocity.z * 0.9f);

            m_walking = false;
        }

        if (m_jumping)
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_jumping = false;
        }

        if(m_grounded)
        {
            m_rigidBody.velocity = Vector3.zero;
        }




        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;
        this.transform.rotation = Quaternion.RotateTowards(m_rigidBody.transform.localRotation, Quaternion.LookRotation(camDir), 10);
       
        if (m_prone)
        {

            m_collider.direction = 2;
            m_rigidBody.velocity = (movement.normalized * m_proneSpeed);
        }
        else
        {
            m_collider.direction = 1;
            m_rigidBody.velocity = (movement.normalized * m_waddleSpeed);

        }


    }


}
