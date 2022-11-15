using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinMotion : MonoBehaviour
{
    float sinCenterX;
    float frequency = 1;
    float amplitude = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        sinCenterX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        Vector2 position = transform.position;
        float sin = Mathf.Sin(transform.position.y)*amplitude;
        position.x = sinCenterX + sin;
        transform.position = position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bounds" || transform.childCount==0)
        {
            Destroy(gameObject);
        }
/*        Destroy(gameObject);*/
    }
}
