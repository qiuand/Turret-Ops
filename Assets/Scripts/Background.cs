using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed=1;
    [SerializeField]
    Renderer bgRender;
    float maxSpeed = 1.25f;
    float speedMultiplier = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.inTutorial != false && EnemySpawn.beginNextWave==true && speed<=speed*maxSpeed)
        {
            speed += Time.deltaTime * speedMultiplier;
        }
        bgRender.material.mainTextureOffset += new Vector2(0, speed*Time.deltaTime);
    }
}
