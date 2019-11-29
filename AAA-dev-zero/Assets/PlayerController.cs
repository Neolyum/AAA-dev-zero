using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController2D controller;

    [SerializeField] private float movementSpeed = 40;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private Vector2 dash = Vector2.zero;

    public KeyCode leftKey, rightKey, jumpKey, crouchKey, dashKey;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        managePlayerInput();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = Vector2.zero;
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
        if (Input.GetKeyDown(dashKey))
        {
            dash = getDashDirection();
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
        Debug.Log(dashDirection);
        return dashDirection;
    }
}
