using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    private CharacterController2D controller;


    [SerializeField] private float movementSpeed = 40;
    [SerializeField] private float dashCoolDown = 3f;
    private float horizontalMove = 0f;
    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool crouch = false;
    private Vector2 dash = Vector2.zero;
    private float dashCoolDownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCoolDownTimer > 0) dashCoolDownTimer -= Time.deltaTime;
        horizontalMove = moveDirection.x * movementSpeed;
        //managePlayerInput();
    }

    private void OnJump()
    {
        jump = true;
    }

    private void OnMovement(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    private void OnDash()
    {
        tryToDash();
    }

    private void OnCrouchDown()
    {
        crouch = true;
    }

    private void OnCrouchUp()
    {
        crouch = false;
    }


    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = Vector2.zero;
    }

   /* private void OnBecameInvisible()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/

    private void tryToDash()
    {
        if (dashCoolDownTimer <= 0)
        {
            //Nur dashen, wenn der Spieler eine Richtun angibt
            if (moveDirection != Vector2.zero)
            {
                dash = moveDirection;
                dashCoolDownTimer = dashCoolDown;
            }
        }
    }
}
