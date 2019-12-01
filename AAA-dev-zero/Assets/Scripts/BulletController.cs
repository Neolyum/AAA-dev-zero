using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float recoil = 25f;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.right * shootingSpeed;
        SoundsLib.Instance.play(transform.position, enums.Sounds.shoot);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity * recoil, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
}
