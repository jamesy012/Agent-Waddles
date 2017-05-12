using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camDir = -Camera.main.transform.right;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(camDir), 10);
    }
}
