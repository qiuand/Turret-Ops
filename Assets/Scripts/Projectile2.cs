using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public bool ricochet;
    public GameObject tutShip;
    // Start is called before the first frame update
    void Start()
    {

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
            gameObject.transform.rotation = Quaternion.Inverse(transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                /*                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z+90);
                                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);*/
                Vector3 enemyScalex = collision.gameObject.transform.localScale *= EnemySpawn.empowerMultiplier;
                collision.gameObject.transform.localScale = new Vector3(enemyScalex.x, enemyScalex.y, collision.gameObject.transform.localScale.z);
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
