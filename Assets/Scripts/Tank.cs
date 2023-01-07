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
    float reactiveArmourDamageMultiplier = 0.5f;
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Shot" || collision.gameObject.tag == "Shot2")
        {
            shaker.GetComponent<Shake>().startShake = true;
            shaker2.GetComponent<Shake>().startShake = true;
            if (((Turret.GetComponent<Turret>().reactiveArmour == true && (collision.gameObject.tag == "Enemy" && Turret.GetComponent<Turret>().startingMag==1)) ||( (Turret.GetComponent<Turret>().reactiveArmour == true && (collision.gameObject.tag == "Enemy2" && Turret.GetComponent<Turret>().startingMag == 2))))&& Turret.GetComponent<Turret>().detectedMag==true)
            {
                Turret.GetComponent<Turret>().health -= 20 * reactiveArmourDamageMultiplier;
            }
            else
            {
                Turret.GetComponent<Turret>().health -= 20;
                Turret.GetComponent<Turret>().damageTaken += 1;
            }

        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Shot")
        {
            shaker.GetComponent<Shake>().startShake = true;
            shaker2.GetComponent<Shake>().startShake = true;
            if (Turret.GetComponent<Turret>().reactiveArmour)
            {
                Turret.GetComponent<Turret>().health -= 20 * reactiveArmourDamageMultiplier;
            }
            else
            {
                Turret.GetComponent<Turret>().health -= 20;
                Turret.GetComponent<Turret>().damageTaken += 1;
            }
            src.PlayOneShot(explode);
            Destroy(other.gameObject);
        }

    }

}
