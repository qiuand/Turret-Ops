using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chamelelon : MonoBehaviour
{
    public float value = 100f;

    public GameObject square;
    public GameObject circle;
    public Sprite redGuy;
    public Sprite greenGuy;
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
    float changeCooldownTime=5.0f;
    float changeDuration=5.0f;
    float speed=2;
    public GameObject bomb;
    float health = 10;
    float maxHealth = 10;
    bool positionFound = false;
    float moveCooldown;
    float moveDuration=3.0f;
    float bombCooldown;
    float bombDuration = 6.0f;
    public AudioClip damage;
    public AudioClip dink;
    public SpriteRenderer render;
    float sinCenterY;
    float frequency = 1;
    float amplitude = 0.3f;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;
        /*        render = GetComponent<SpriteRenderer>();*/
        GetComponent<SpriteRenderer>().sprite = greenGuy;
        healthText = GameObject.FindGameObjectWithTag("Boss Health").GetComponent<Image>();
        moveCooldown = moveDuration;
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyAnim.SetBool("Chameleon", true);
    }

    // Update is called once per frame
    void Update()
    {
/*        Vector2 position = transform.position;
        float sin = Mathf.Sin(transform.position.x) * amplitude;
        position.y = sinCenterY + sin;
        transform.position = position;*/
        /*        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite = greenGuy;*/
        healthText.fillAmount = health / maxHealth;
        bombCooldown -= Time.deltaTime;
        if (bombCooldown <= 0)
        {
            bombCooldown = bombDuration;
            Instantiate(bomb, transform.position, Quaternion.identity);
            if (gameObject.tag == "Enemy")
            {
                bomb.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.3f, 1f);
                bomb.gameObject.tag = "Shot";
            }
            else{
                bomb.gameObject.tag = "Shot2";
                bomb.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.3f, 0.15f);
            }
        }
        moveCooldown -= Time.deltaTime;
/*        if (moveCooldown <= 0)
        {
            positionFound = false;
            moveCooldown = moveDuration;
        }
        if (positionFound == false)
        {
            newPos = new Vector3(Random.Range(RoamRange.roamRange.rect.xMin, RoamRange.roamRange.rect.xMax), Random.Range(RoamRange.roamRange.rect.yMin, RoamRange.roamRange.rect.yMax), 0) + RoamRange.roamRange.transform.position;
            positionFound = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);*/
/*        if (newPos == rb.transform.position)
        {
            positionFound = false;
        }*/
        changeCooldownTime -= Time.deltaTime;
        if (changeCooldownTime <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                square.SetActive(true);
                circle.SetActive(false);
                render.sprite = redGuy;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.3f, 0.15f);
                gameObject.tag = "Enemy2";
            }
            else
            {
                square.SetActive(false);
                circle.SetActive(true);
                render.sprite = greenGuy;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.07f, 0.4f, 0.8f);
                gameObject.tag = "Enemy";
            }
            changeDuration = Random.Range(changeMin, changeMax);
            changeCooldownTime = changeDuration;
        }
        render.sprite = redGuy;
        /*        GetComponent<SpriteRenderer>().sprite = greenGuy;*/
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
        else if (collision.gameObject.tag=="Projectile" || collision.gameObject.tag == "Projectile2")
        {
            source.PlayOneShot(dink);
        }

    }
    private void Destroy()
    {
        square.SetActive(false);
        circle.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        GameObject.FindGameObjectWithTag("Scrippy").GetComponent<EnemySpawn>().bossDestroyed = true;
        source.PlayOneShot(explode);
        source.PlayOneShot(explode);
        source.PlayOneShot(explode);
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = false;
        fire.enableEmission = false;
        enemyAnim.SetBool("Destroyed", true);
        Turret.score += value * EnemySpawn.scoreMultiply;
        Destroy(gameObject, 2f);
    }
}
