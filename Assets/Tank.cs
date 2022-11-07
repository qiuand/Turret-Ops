using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public AudioSource src;
    public AudioClip explode;
    public GameObject Turret;
    public GameObject shaker;
    public GameObject shaker2;
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
        print("dagnabbit");
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Shot" || collision.gameObject.tag == "Shot2")
        {
            shaker.GetComponent<Shake>().startShake=true;
            shaker2.GetComponent<Shake>().startShake = true;
            Turret.GetComponent<Turret>().health -= 20;
            Turret.GetComponent<Turret>().damageTaken += 1;

        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Shot")
        {

            Turret.GetComponent<Turret>().health -= 20;
            Turret.GetComponent<Turret>().damageTaken += 1;
            src.PlayOneShot(explode);
            Destroy(other.gameObject);
        }
        print("DML");

    }

}
