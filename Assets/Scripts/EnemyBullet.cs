using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip explode;
    Rigidbody2D rb;
    public ParticleSystem fire;
    public Animator enemyAnim;
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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bounds" || (collision.gameObject.tag == "Projectile" && gameObject.tag == "Shot") || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "Shot2")|| collision.gameObject.tag=="Projectile3")
        {
            if (collision.gameObject.tag != "Bounds" && collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2" && collision.gameObject.tag != "Projectile3")
            {
                source.PlayOneShot(explode);
            }
            rb.velocity = new Vector2(0, 0);
            /*            rb.isKinematic = false;*/
            GetComponent<Collider2D>().enabled = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            Destroy(gameObject, 0.5f);
        }

    }
}
