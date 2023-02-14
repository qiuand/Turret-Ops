using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    Rigidbody2D rb;
    public bool ricochet;
    public GameObject tutShip;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*     
                if (collision.gameObject.tag == "Enemy2")
                {
                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.tag == "TutEnemy2")
                {
                    collision.gameObject.SetActive(false);
                }*/
        if (ricochet)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            var speed = rb.velocity;
            print(speed);
            /*            transform.eulerAngles = Vector3.Reflect(speed, collision.contacts[0].normal);*/
            gameObject.transform.rotation = Quaternion.Inverse(transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
/*                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                var speed = rb.velocity;
                print(speed);
                transform.eulerAngles = Vector3.Reflect(speed, collision.contacts[0].normal);*/
                /*var speed = rb.velocity;
                var direction = Vector3.Reflect(speed, collision.contacts[0].normal);
                rb.velocity = direction;*/
                /*                rb.AddForce(new Vector2(500, 500), ForceMode2D.Impulse);*/
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
