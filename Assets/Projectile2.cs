using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public GameObject tutShip;
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
        /*        print(3);
                if (collision.gameObject.tag == "Enemy2")
                {
                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.tag == "TutEnemy2")
                {
                    collision.gameObject.SetActive(false);
                }*/

        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Projectile2")
        {
            Destroy(gameObject);
        }
    }
}
