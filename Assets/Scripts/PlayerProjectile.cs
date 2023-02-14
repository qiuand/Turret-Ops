using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public bool ricochet=false;
    public float lifetime = 4f;
    public GameObject tutShip;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*        if (collision.gameObject.tag == "TutEnemy")
                {
                    collision.gameObject.SetActive(false);
                }*/


        if (ricochet && collision.gameObject.tag!="Bounds")
        {
            /*            Destroy(gameObject, 0.5f);
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                        var speed = rb.velocity;
                        print(speed);
                        transform.eulerAngles = Vector3.Reflect(speed, collision.contacts[0].normal);*/
            gameObject.transform.rotation = Quaternion.Inverse(transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2" /*&& ((gameObject.tag=="Projectile" && collision.gameObject.tag=="Enemy2")||(gameObject.tag=="Projectile2" && collision.gameObject.tag=="Enemy"))*/)
        {
            if (collision.gameObject.tag == "Enemy2")
            {
                Destroy(gameObject);
/*                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                var speed = rb.velocity;
                print(speed);
                transform.eulerAngles = Vector3.Reflect(speed, collision.contacts[0].normal);*/

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
