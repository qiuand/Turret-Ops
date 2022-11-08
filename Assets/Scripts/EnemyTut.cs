using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTut : MonoBehaviour
{
    Rigidbody2D rb;
    public ParticleSystem fire;
    public Animator enemyAnim;
    AudioSource source;
    public AudioClip explode;
    float activeTimer;
    float activeTime=0.6f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" && gameObject.tag=="TutEnemy" || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "TutEnemy2"))
        {   
            source.PlayOneShot(explode);
            rb.velocity = new Vector2(0, 0);
            GetComponent<Collider2D>().enabled = false;
            rb.isKinematic = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
