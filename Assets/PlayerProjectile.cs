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
    private void OnCollisionEnter2D(Collision2D collision)
    {
/*        if (collision.gameObject.tag == "TutEnemy")
        {
            print("yippee");
            collision.gameObject.SetActive(false);
        }*/
        Destroy(gameObject);
    }
}
