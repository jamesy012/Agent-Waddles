﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    Rigidbody m_rigidBody;

    Quaternion m_proneRotation;
    public float m_speed;
    public bool m_walking;
    public bool m_jumping;
    public bool m_prone;

    OffSholderCam cameraController;
    ShootHandler shootHandler;
    Inventory inventoryHandler;
    bool isInventory = false;

    // Use this for initialization
    void Start()
    {
        m_proneRotation = Quaternion.LookRotation(-this.transform.up);
        m_rigidBody = this.GetComponent<Rigidbody>();

        // inventory
        cameraController = GameObject.FindObjectOfType<OffSholderCam>();
        shootHandler = this.GetComponent<ShootHandler>();
        inventoryHandler = this.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_jumping = false;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_jumping = true;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            m_prone ^= true;
        }
        // inventory controls
        if (Input.GetKeyDown(KeyCode.I))
        {

            isInventory = !isInventory;
            // Locks camera rotation
            cameraController.IsLookAt = !isInventory;
            // Disables shooting while in inventory
            shootHandler.enabled = !isInventory;
            // hides/shows inventory canvas.
            inventoryHandler.SetView(isInventory);
        }

   

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



        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;

        if (m_prone)
        {
            Vector3 proneMovement = this.transform.right * Input.GetAxis("Horizontal") + this.transform.up * Input.GetAxis("Vertical");
            m_rigidBody.transform.localRotation = Quaternion.RotateTowards(m_rigidBody.transform.localRotation, m_proneRotation , 10);
            //m_rigidBody.transform.up = camDir;
            m_rigidBody.AddForce(proneMovement.normalized * m_speed);
        }
        else
        {
 
            m_rigidBody.transform.rotation = Quaternion.RotateTowards(m_rigidBody.transform.localRotation, Quaternion.LookRotation(camDir), 10) ;
            m_rigidBody.AddForce(movement.normalized * m_speed);
        }
    }

    void GoProne()
    {
    }
}
