using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController2 : MonoBehaviour
{
    private CharacterController2D controller;
    private TextMeshPro readyText;


    [SerializeField] private float movementSpeed = 40;
    [SerializeField] private float dashCoolDown = 3f;
    [SerializeField] private float shootingSpeed = 20f;
    [SerializeField] private float shootingCoolDown = 0.5f;
    private float horizontalMove = 0f;
    private Vector2 moveDirection = Vector2.zero;
    private bool jump = false;
    private bool crouch = false;
    private Vector2 dash = Vector2.zero;
    private float dashCoolDownTimer = 0f;
    private float shootingCoolDownTimer = 0f;
    private string device;
    private bool isReady = false;
    
    public Transform firePoint;
    public Transform bullet;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        device = GetComponent<PlayerInput>().devices[0].name;
        readyText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCoolDownTimer > 0) dashCoolDownTimer -= Time.deltaTime;
        if (shootingCoolDownTimer > 0) shootingCoolDownTimer -= Time.deltaTime;
        horizontalMove = moveDirection.x * movementSpeed;
        if (Mathf.Abs(moveDirection.x) < 0.2) horizontalMove = 0; //Verhindern von minimalen Movement bei controllern.
        if (Mathf.Abs(moveDirection.y) < 0.2) moveDirection.y = 0;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (controller.isGrounded())
        {
            anim.SetTrigger("stopDash");
        }
        mouseShooting();
    }

    private void LateUpdate()
    {
        readyText.transform.rotation = Quaternion.identity;
    }

    private void OnJump()
    {
        jump = true;
        anim.SetBool("isJumping", true);
    }

    public void OnLand()
    {
        anim.SetBool("isJumping", false);
        anim.SetTrigger("stopDash");
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

    private Vector3 getVectorToMousePosition(bool onlyForward=false)
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        if (shootDirection.x > transform.position.x && !controller.isFacingRight() && onlyForward) return Vector3.zero;
        if (shootDirection.x < transform.position.x && controller.isFacingRight() && onlyForward) return Vector3.zero;
        shootDirection = (shootDirection - firePoint.position);
        shootDirection.z = 0f;
        return shootDirection;
    }

    private void mouseShooting()
    {
        if (Input.GetMouseButton(0) && device == "Keyboard")
        {            
            if (shootingCoolDownTimer <= 0)
            {
                Vector3 shootDirection = getVectorToMousePosition(onlyForward: true);
                if (shootDirection == Vector3.zero) return;

                shootingCoolDownTimer = shootingCoolDown;
                var bulletObj = Instantiate(bullet, firePoint.position, firePoint.rotation) as Transform;
                foreach (Collider2D c in GetComponents<Collider2D>())
                {
                    Physics2D.IgnoreCollision(c, bulletObj.GetComponent<Collider2D>());
                }
                bulletObj.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * shootingSpeed;
            }
        }
    }

    private void OnShoot()
    {
        if (shootingCoolDownTimer <= 0)
        {
            shootingCoolDownTimer = shootingCoolDown;
            var bulletObj = Instantiate(bullet, firePoint.position, firePoint.rotation) as Transform;
            if (moveDirection == Vector2.zero)
            {
                bulletObj.GetComponent<Rigidbody2D>().velocity = firePoint.right * shootingSpeed;
            } else
            {
                bulletObj.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * shootingSpeed;
            }
            
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                Physics2D.IgnoreCollision(c, bulletObj.GetComponent<Collider2D>());
            }
        }
        
    }

    private void OnReady() {
        isReady = !isReady;
        if (isReady)
        {
            readyText.text = "Ready";
            readyText.color = new Color(0f, 1f, 0f);
        } else
        {
            readyText.text = "Not ready";
            readyText.color = new Color(1f, 0f, 0f);
        }
        

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = Vector2.zero;
    }

    private void tryToDash()
    {
        if (dashCoolDownTimer <= 0)
        {
            if (device == "Keyboard")
            {
                Vector3 dashDirection = getVectorToMousePosition();
                dash = new Vector2(dashDirection.normalized.x, dashDirection.normalized.y);
                dashCoolDownTimer = dashCoolDown;
                anim.SetBool("isDashing", true);
            } else if (moveDirection != Vector2.zero)
            {
                dash = moveDirection;
                dashCoolDownTimer = dashCoolDown;
                anim.SetBool("isDashing", true);
            }
        }
    }
}
