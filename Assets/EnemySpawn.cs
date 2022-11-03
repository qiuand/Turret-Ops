using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy2;
    public GameObject enemy;
    RectTransform rect;
    float spawnCooldown=0.75f;
    float spawnTimer=0;
    float minXSpeed=-2;
    float maxXSpeed=-4;
    float minYSpeed=-2;
    float maxYSpeed=2;
    int random;
    bool isEnemy2 = false;
    float waveTimer = 0;
    float waveTiming = 3.25f;
    int enemyNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            int type = Random.Range(0, 2);
            for (int i = 0; i < enemyNum; i++)
            {
                if (type == 0)
                {
                    {
                        GameObject enemyInstance = Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                        enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
                    }
                }
                else
                {
                    GameObject enemyInstance = Instantiate(enemy2, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                    enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
                }
                waveTimer = waveTiming;


            }

        }
/*        if (spawnTimer <= 0)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                GameObject enemyInstance = Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
            }
            else
            {
                GameObject enemyInstance = Instantiate(enemy2, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
            }
            spawnTimer = spawnCooldown;
        }*/
        spawnTimer -= Time.deltaTime;
    }
}
