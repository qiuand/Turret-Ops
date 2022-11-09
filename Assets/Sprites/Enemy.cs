using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float scoreUpgradeValue = 20;
    public Animator enemyAnim;
    Rigidbody2D rb;
    public AudioClip explode;
    public AudioSource source;
    public ParticleSystem fire;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bounds"||(collision.gameObject.tag=="Projectile"&&gameObject.tag=="Enemy")||( collision.gameObject.tag=="Projectile2"&&gameObject.tag=="Enemy2")||collision.gameObject.tag=="Projectile3")
        {
            if (collision.gameObject.tag != "Bounds")
            {
                Turret.scoreToUpgrade += scoreUpgradeValue;
                source.PlayOneShot(explode);
            }
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            Destroy(gameObject,0.3f);
        }
        
    }
/*    private void OnParticleTrigger(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bounds" || (collision.gameObject.tag == "Projectile" && gameObject.tag == "Enemy") || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "Enemy2"))
        {
            if (collision.gameObject.tag != "Bounds")
            {
                source.PlayOneShot(explode);
            }
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            Destroy(gameObject, 0.3f);
        }
    }*/
}
