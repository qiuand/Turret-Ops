using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamelelon : MonoBehaviour
{
    float scoreUpgradeValue = 20;
    public Animator enemyAnim;
    Rigidbody2D rb;
    public AudioClip explode;
    public AudioSource source;
    public ParticleSystem fire;
    float chameleonHealth;
    float changeMin = 3.0f;
    float changeMax = 5.0f;
    float changeCooldownTime;
    float changeDuration;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeCooldownTime -= Time.deltaTime;
        if (changeCooldownTime <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0f, 0f);
                gameObject.tag = "Enemy2";
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1f, 0f);
                gameObject.tag = "Enemy";
            }
            changeDuration = Random.Range(changeMin, changeMax);
            changeCooldownTime = changeDuration;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bounds" || (collision.gameObject.tag == "Projectile" && gameObject.tag == "Enemy") || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "Enemy2") || collision.gameObject.tag == "Projectile3")
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
            Destroy(gameObject, 0.65f);
        }

    }
}
