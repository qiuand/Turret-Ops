using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chamelelon : MonoBehaviour
{
    public Text text;
    public Image healthText;
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
    float speed=2;
    public GameObject bomb;
    float health = 5;
    float maxHealth = 5;
    bool positionFound = false;
    float moveCooldown;
    float moveDuration=3.0f;
    float bombCooldown;
    float bombDuration = 6.0f;
    public AudioClip damage;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("Boss Health").GetComponent<Image>();
        moveCooldown = moveDuration;
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.fillAmount = health / maxHealth;
        bombCooldown -= Time.deltaTime;
        if (bombCooldown <= 0)
        {
            bombCooldown = bombDuration;
            Instantiate(bomb, transform.position, Quaternion.identity);
            if (gameObject.tag == "Enemy")
            {
                bomb.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1f, 0f);
                bomb.gameObject.tag = "Shot";
            }
            else{
                bomb.gameObject.tag = "Shot2";
                bomb.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0f, 0f);
            }
        }
        moveCooldown -= Time.deltaTime;
        if (moveCooldown <= 0)
        {
            positionFound = false;
            moveCooldown = moveDuration;
        }
        if (positionFound == false)
        {
            newPos = new Vector3(Random.Range(RoamRange.roamRange.rect.xMin, RoamRange.roamRange.rect.xMax), Random.Range(RoamRange.roamRange.rect.yMin, RoamRange.roamRange.rect.yMax), 0) + RoamRange.roamRange.transform.position;
            positionFound = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
/*        if (newPos == rb.transform.position)
        {
            positionFound = false;
        }*/
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
                source.PlayOneShot(damage);
            }
            health--;
            if (health <= 0)
            {
                Destroy();
            }
/*            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = false;
            fire.enableEmission = false;
            enemyAnim.SetBool("Destroyed", true);
            Destroy(gameObject, 0.65f);*/
        }

    }
    private void Destroy()
    {
        source.PlayOneShot(explode);
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = false;
        fire.enableEmission = false;
        enemyAnim.SetBool("Destroyed", true);
        Destroy(gameObject, 0.65f);
    }
}
