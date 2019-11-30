using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private CharacterController2D controller;
    private PlayerControls controls;


    [SerializeField] private float movementSpeed = 40;
    [SerializeField] private float dashCoolDown = 3f;
    private float horizontalMove = 0f;
    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool crouch = false;
    private Vector2 dash = Vector2.zero;
    private float dashCoolDownTimer = 0f;

    public KeyCode leftKey, rightKey, jumpKey, crouchKey, dashKey;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();

        controls.GamePlay.Jump.performed += ctx => jump = true;
        controls.GamePlay.Movement.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.GamePlay.Movement.canceled += ctx => moveDirection = Vector2.zero;
        controls.GamePlay.Dash.performed += ctx => tryToDash();
        //controls.GamePlay.Crouch.performed += ctx => crouch = true;
        //controls.GamePlay.Crouch.canceled += ctx => crouch = false;
    }

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCoolDownTimer > 0) dashCoolDownTimer -= Time.deltaTime;
        horizontalMove = moveDirection.x * movementSpeed;
        //managePlayerInput();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void managePlayerInput()
    {
        horizontalMove = getPlayerHorizontalAxis() * movementSpeed;
        if (Input.GetKeyDown(jumpKey))
        {
            jump = true;
        }
        if (Input.GetKeyDown(crouchKey))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            crouch = false;
        }
        if (Input.GetKeyDown(dashKey) && dashCoolDownTimer <= 0)
        {
            Vector2 dashDirection = getDashDirection();
            //Nur dashen, wenn der Spieler eine Richtun angibt
            if (dashDirection != Vector2.zero)
            {
                dash = dashDirection;
                dashCoolDownTimer = dashCoolDown;
            }
        }
    }

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

    private float getPlayerHorizontalAxis()
    {
        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            return 0f;
        } else if (Input.GetKey(leftKey)) {
            return -1f;
        } else if (Input.GetKey(rightKey)) {
            return 1f;
        }
        return 0f;
    }

    private Vector2 getDashDirection()
    {
        Vector2 dashDirection = Vector2.zero;

        if (Input.GetKey(leftKey))
        {
            dashDirection.x -= 1;
        }
        if (Input.GetKey(rightKey))
        {
            dashDirection.x += 1;
        }
        if (Input.GetKey(jumpKey))
        {
            dashDirection.y += 1;
        }
        if (Input.GetKey(crouchKey))
        {
            dashDirection.y -= 1;
        }
        return dashDirection;
    }
}
