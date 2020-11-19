using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    #region Singleton

    public static GroundCheck instance;

    void Awake()
    {
        instance = this;
    }   

    #endregion

    public bool isGrounded = false;
 
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }   
}
