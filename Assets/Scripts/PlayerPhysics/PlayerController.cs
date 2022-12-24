//using Mono.Cecil;
//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms.GameCenter;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    public GameObject groundCheck;
    public float checkRadius = .4f;
    public float g = 9.81f;
    public float jumpHeight = 10f;

    private CharacterController controller;
    private Vector3 move;
    private bool isGrounded;
    private LayerMask ground;
    private Vector3 velocity;

    // Animations
    private bool IsWalking;
    private bool IsRunning;
    private bool IsJumping;

    // Refferences
    private InputManager iManager;
    private WeaponInfo gun;
    private WeaponSwitch weaponSwitch;
    private Animator animator;

    [Header("References")]
    [HideInInspector]public GameObject currentWeapon;
    public GameObject weaponHolder;
    public GameObject _animator;


    private void Awake()
    {
        GameObject inputManager = GameObject.Find("Input Manager");
        iManager = inputManager.GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        weaponSwitch = weaponHolder.GetComponent<WeaponSwitch>();
        //animator = _animator.GetComponent<Animator>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ground = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        groundChecking();
        movement();    
        Jump();
        fire();
        //animating();
    }

    public void fire()
    {
        currentWeapon = weaponSwitch.currentWeapon;
        gun = currentWeapon.GetComponent<WeaponInfo>();
        if (Input.GetButton("Fire1"))
        {
            currentWeapon = weaponSwitch.currentWeapon;
            gun = currentWeapon.GetComponent<WeaponInfo>();
            gun.fire();
            gun.TimePressed += Time.deltaTime;
        }
        else
        {
            gun.TimePressed = 0f;
            gun.shooting = false;
        }
    }

    private void groundChecking()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, ground);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void movement()
    {
        //Movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            iManager.Horizontal *= 2;
            iManager.Vertical *= 2; 
        }
        move = transform.right * iManager.Horizontal + transform.forward * iManager.Vertical;
        controller.Move(move * speed * Time.fixedDeltaTime);

        //Gravity
        velocity.y -= g * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);

    }

    private void animating()
    {
        animator.SetFloat("Velocity Y", iManager.Vertical);
        animator.SetFloat("Velocity X", iManager.Horizontal);
        if (iManager.Horizontal !=0 || iManager.Vertical != 0) 
        {
            //animator.SetBool("IsWalking", true);
        }
        else
        {
            //animator.SetBool("IsWalking", false);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * g * 2f);

            // anim
            //animator.SetBool("Jumping", true);
        }
        else
        {
            //animator.SetBool("Jumping", false);
        }
        
    }
}