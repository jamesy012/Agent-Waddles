using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator m_animator;
    bool m_prone = false;
    // Use this for initialization
    void Start()
    {
        m_animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");

        if(Input.GetKey(KeyCode.W) || Mathf.Abs(mouseX) > 0.5f)
        {
            m_animator.SetBool("WalkingForward", true);
        }
        else
        {
            m_animator.SetBool("WalkingForward", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_animator.SetBool("WalkingBackward", true);
        }
        else
        {
            m_animator.SetBool("WalkingBackward", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_animator.SetBool("SideStepRight", true);
        }
        else
        {
            m_animator.SetBool("SideStepRight", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_animator.SetBool("SideStepLeft", true);
        }
        else
        {
            m_animator.SetBool("SideStepLeft", false);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            m_prone ^= true;
            m_animator.SetBool("Prone", m_prone);
        }
    }
}
