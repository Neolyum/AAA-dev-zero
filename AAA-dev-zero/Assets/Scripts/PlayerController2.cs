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
    public PlayerController2 Instance;

    public Transform firePoint;
    public Transform bullet;
    public GameObject gameController;
    [SerializeField] private GameObject explosion;
    private bool isReady = false;
    private bool updateDashCoolDownText = false;
    
    public Animator anim;


    [SerializeField] private GameObject dashTrail;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
        controller = GetComponent<CharacterController2D>();
        device = GetComponent<PlayerInput>().devices[0].name;
        gameController.GetComponent<Controller.GameController>().addPlayer(gameObject);
        Instance = this;
        readyText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCoolDownTimer > 0) dashCoolDownTimer -= Time.deltaTime;
        if (shootingCoolDownTimer > 0) shootingCoolDownTimer -= Time.deltaTime;
        anim.SetFloat("shootCoolDown", shootingCoolDownTimer);
        horizontalMove = moveDirection.x * movementSpeed;
        if (Mathf.Abs(moveDirection.x) < 0.2) horizontalMove = 0; //Verhindern von minimalen Movement bei controllern.
        if (Mathf.Abs(moveDirection.y) < 0.2) moveDirection.y = 0;
        if (updateDashCoolDownText) updateCoolDown();
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
        if (!anim.GetBool("isJumping")) SoundsLib.Instance.play(transform.position, enums.Sounds.jump, 0.3f);
        jump = true;
        anim.SetBool("isJumping", true);
        anim.SetTrigger("jump");
    }

    public void OnLand()
    {
        Destroy(GameObject.Find("One shot audio"));
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
        anim.SetBool("isCrouching", true);
    }

    private void OnCrouchUp()
    {
        crouch = false;
        anim.SetBool("isCrouching", false);
    }

    private Vector3 getVectorToMousePosition(bool onlyForward = false)
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

                anim.SetTrigger("shoot");
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
            anim.SetTrigger("shoot");
            shootingCoolDownTimer = shootingCoolDown;
            var bulletObj = Instantiate(bullet, firePoint.position, firePoint.rotation) as Transform;
            if (moveDirection == Vector2.zero)
            {
                bulletObj.GetComponent<Rigidbody2D>().velocity = firePoint.right * shootingSpeed;
            }
            else
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

    public bool isPlayerReady()
    {
        return isReady;
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
            SoundsLib.Instance.play(transform.position, enums.Sounds.dash, 0.3f);
            Debug.Log(dashTrail);
            GameObject d = Instantiate(dashTrail, transform.position, Quaternion.identity);
            Destroy(d, 0.11f);
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

    private void OnBecameInvisible()
    {
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 5);
        SoundsLib.Instance.play(transform.position, enums.Sounds.explosion);
        Destroy(gameObject);
    }
    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public float getSpeed()
    {
        return movementSpeed;
    }

    public void setDashCooldown(float time)
    {
        dashCoolDown = time;
    }

    public float getDashCooldown()
    {
        return dashCoolDown;
    }

    public float getDashCooldownTimer()
    {
        return dashCoolDownTimer;
    }

    private void updateCoolDown() //DashCoolDown-Anzeige mit #
    {
        string outs = "";
        for (int i = 0; i < dashCoolDownTimer * 3; i++)
        {
            outs += "#";
        }
        readyText.text = outs;
    }



    public void disableReadyText(Color color)
    {
        readyText.text = "";
        readyText.color = color;
        updateDashCoolDownText = true;
    }
}
