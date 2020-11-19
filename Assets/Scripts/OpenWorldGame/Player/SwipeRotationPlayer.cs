using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeRotationPlayer : MonoBehaviour
{
    private Touch initTouch = new Touch();
    public Camera cam;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector3 originRotation;

    public float rotationSpeed = 0.3f;

    void Start()
    {
        originRotation = cam.transform.eulerAngles;
        xRotation = originRotation.x;
        yRotation = originRotation.y;
    }
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float x = initTouch.position.x - touch.position.x;
                float y = initTouch.position.y - touch.position.y;
                
                xRotation -= Mathf.Clamp(y * Time.deltaTime * rotationSpeed, -90f, 90f);
                yRotation += Mathf.Clamp(x * Time.deltaTime * rotationSpeed, -90f, 90f);
                
                cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }
        }
    }
}
