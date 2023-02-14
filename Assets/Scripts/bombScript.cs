using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    AudioSource source;
    public AudioClip explode;
    Animator enemyAnim;
    public ParticleSystem fire;
    float speed = 1;
    Rigidbody2D rb;
    public Transform player;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.position;
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("yes");
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bounds" || (collision.gameObject.tag == "Projectile" && gameObject.tag == "Shot") || (collision.gameObject.tag == "Projectile2" && gameObject.tag == "Shot2") || collision.gameObject.tag == "Projectile3")
        {
            if (collision.gameObject.tag != "Bounds" && collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2" && collision.gameObject.tag != "Projectile3")
            {
                source.PlayOneShot(explode);
            }
            rb.velocity = new Vector2(0, 0);
/*            rb.isKinematic = false;*/
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            Destroy(gameObject, 0.5f);
        }
    }
    }
