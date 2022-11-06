using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootEnemy : MonoBehaviour
{
    public GameObject projectile;
    public GameObject muzzle;
    float shootCooldown = 1f;
    float shootDuration;
    float velocity = 3;
    // Start is called before the first frame update
    void Start()
    {
        shootDuration = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        shootDuration -= Time.deltaTime;
        if (shootDuration <= 0)
        {
            GameObject thingy = Instantiate(projectile, muzzle.transform.position, Quaternion.identity);
            thingy.GetComponent<Rigidbody2D>().velocity = -transform.up * velocity;
            shootDuration = shootCooldown;
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Bounds" || other.gameObject.tag == "Projectile") && gameObject.tag=="Shot")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bounds" || other.gameObject.tag == "Projectile2" && gameObject.tag == "Shot2")
        {
            Destroy(gameObject);
        }
    }
}
