using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bomb;
    float bombTimer;
    float bombDuration=6.0f;
    // Start is called before the first frame update
    void Start()
    {
        bombTimer = bombDuration;
    }

    // Update is called once per frame
    void Update()
    {
/*        Instantiate(bomb, transform.position, Quaternion.identity);*/
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            bombTimer = /*bombDuration*/999;
        }
    }
}
