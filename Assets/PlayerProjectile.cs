using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifetime = 4f;
    public GameObject tutShip;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
/*        if (collision.gameObject.tag == "TutEnemy")
        {
            print("yippee");
            collision.gameObject.SetActive(false);
        }*/
if(collision.gameObject.tag!="Projectile"&& collision.gameObject.tag != "Projectile2")
        {
            Destroy(gameObject);
        }
    }
}
