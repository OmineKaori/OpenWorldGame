using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private AudioSource audioSource;
    private GameManager gameManager;

    // JoyStickを使っての移動 
    public Joystick joystick;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public AudioClip walkingSound;
    
    // SwipeでRotateする
    public Camera cam;
    public float cameraSensitivity = 1f;
    public float swipeAvailableWidth; // 画面の右半分をSwipeすると視点を変えることができる(JoyStickと重ならないようにするため)
    private Touch initTouch;
    private float xRotation = 0f;
    private Vector2 lookInput;
    
    // Jump
    private Vector3 velocity;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
        swipeAvailableWidth = Screen.width / 2;
    }

    void Update()
    {
        Move();
        RotateView();
        
        // Playerが街から落ちるとGameOver
        if (transform.position.y < -100f) 
            Player.instance.KillPlayer();
    }

    void Move()
    {
        Vector3 move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        if (GroundCheck.instance.isGrounded)
        {
            audioSource.clip = walkingSound;
            controller.Move(move * (speed * Time.deltaTime));
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if (GroundCheck.instance.isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
    
    void RotateView()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < swipeAvailableWidth)
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    initTouch = touch;
                    break;
                
                case TouchPhase.Moved:
                    lookInput = touch.deltaPosition * (cameraSensitivity * Time.deltaTime);
                    break;
                
                case TouchPhase.Canceled:
                    initTouch = new Touch();
                    break;
                
                case TouchPhase.Stationary:
                    lookInput = Vector2.zero;
                    break;
            }
        }
        
        xRotation = Mathf.Clamp(xRotation - lookInput.y, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        transform.Rotate(transform.up * lookInput.x);
    }
    
    public void Jump()
    {
        if (GroundCheck.instance.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            GroundCheck.instance.isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // GoalのgameObjectに触れるとGame終了
        if (other.gameObject.CompareTag("Goal"))
            GameManager.instance.GameClear();
    }
}
