using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    AudioSource src;
    public GameObject bomb;
    float bombTimer;
    float bombDuration=6.0f;
    // Start is called before the first frame update
    void Start()
    {
        bombTimer = bombDuration;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
/*        transform.position = new Vector2(9, 9);*/
/*        Instantiate(bomb, transform.position, Quaternion.identity);*/
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
            src.Play();
            Instantiate(bomb, transform.position, Quaternion.identity);
            bombTimer = /*bombDuration*/999;
        }
    }
}
