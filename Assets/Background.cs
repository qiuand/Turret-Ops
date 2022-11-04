using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed=1;
    [SerializeField]
    Renderer bgRender;
    float speedMultiplier = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.inTutorial != false)
        {
            speed += Time.deltaTime * speedMultiplier;
        }
        bgRender.material.mainTextureOffset += new Vector2(speed*Time.deltaTime, 0);
    }
}
