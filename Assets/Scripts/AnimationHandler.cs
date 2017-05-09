using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator m_animator;
    // Use this for initialization
    void Start()
    {
        m_animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_animator.SetBool("WalkingForward", true);
        }
        else
        {
            m_animator.SetBool("WalkingForward", false);
        }
    }
}
