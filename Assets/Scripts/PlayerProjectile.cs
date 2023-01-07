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


        if (ricochet)
        {
            gameObject.transform.rotation = Quaternion.Inverse(transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2" /*&& ((gameObject.tag=="Projectile" && collision.gameObject.tag=="Enemy2")||(gameObject.tag=="Projectile2" && collision.gameObject.tag=="Enemy"))*/)
        {
            if (collision.gameObject.tag == "Enemy2")
            {
                /*                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z+90);
                                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);*/
                Vector3 enemyScalex = collision.gameObject.transform.localScale *= EnemySpawn.empowerMultiplier;
                collision.gameObject.transform.localScale = new Vector3(enemyScalex.x, enemyScalex.y, collision.gameObject.transform.localScale.z);
            }
            /*            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z);*/
/*            else {
                Destroy(gameObject);
            }*/
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
