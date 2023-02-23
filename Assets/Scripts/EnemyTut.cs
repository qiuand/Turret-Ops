using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTut : MonoBehaviour
{
    public AudioClip dink;
    Rigidbody2D rb;
    public ParticleSystem fire;
    public Animator enemyAnim;
    AudioSource source;
    public AudioClip explode;
    float activeTimer;
    float activeTime=0.65f;
    public bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        GetComponent<Collider2D>().enabled = true;
        fire.enableEmission = true;
        enemyAnim.SetBool("Exploded", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyed)
        {
            fire.enableEmission = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" && gameObject.tag=="TutEnemy" || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "TutEnemy2"))
        {
            destroyed = true;
            source.PlayOneShot(explode);
            rb.velocity = new Vector2(0, 0);
            GetComponent<Collider2D>().enabled = false;
            rb.isKinematic = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Exploded", true);
            StartCoroutine(Wait());
        }
        else if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Projectile2")
        {
            source.PlayOneShot(dink);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
