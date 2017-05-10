using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    Rigidbody m_rigidBody;

    public float m_jumpForce = 5;
 
    public float m_waddleSpeed = 300;
    public float m_proneSpeed = 20;
    public float m_airControl = 10.0f;
    public float m_decelerationRate = 0.5f;
    public bool m_walking;
    public bool m_jumping;
    public bool m_prone;
    public bool m_grounded;

    private Vector3 m_currGroundNormal;

    private CapsuleCollider m_collider;
    // Use this for initialization
    void Start()
    {
        m_collider = this.GetComponent<CapsuleCollider>();
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_currGroundNormal = this.transform.up;
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
        RaycastHit hit;
        m_grounded = Physics.Raycast(this.transform.position, Vector3.down,out hit, 1.01f);
        Debug.DrawRay(this.transform.position, Vector3.down * 2, Color.cyan);

        movement = this.transform.right * Input.GetAxis("Horizontal") + this.transform.forward * Input.GetAxis("Vertical");
        movement.y = 0;

        if (movement.sqrMagnitude != 0.0f)
        {
            m_walking = true;
        }
        else
        {
            //less slippery


            m_walking = false;
        }


        //if (!m_prone)
        //{
        //    m_rigidBody.velocity = Vector3.zero;
        //}

        if (m_jumping)
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_jumping = false;
        }



        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;
        this.transform.rotation = Quaternion.RotateTowards(m_rigidBody.transform.localRotation, Quaternion.LookRotation(camDir), 10);
        if (m_grounded)
        {
            

            if (m_prone)
            {
                Vector3 proneMovement = this.transform.forward * Input.GetAxis("Vertical");
                m_collider.direction = 2;
                m_rigidBody.AddForce(proneMovement.normalized * m_proneSpeed);
            }
            else
            {
                m_rigidBody.velocity -= new Vector3(m_rigidBody.velocity.x * m_decelerationRate, 0, m_rigidBody.velocity.z * m_decelerationRate);
                m_collider.direction = 1;
                m_rigidBody.AddForce(movement.normalized * m_waddleSpeed);

            }
        }
        else
        {
            //air control
            m_rigidBody.AddForce(movement.normalized * m_airControl );
        }



    }





}
