using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy2;
    public GameObject enemy;
    RectTransform rect;
    float spawnCooldown=0.5f;
    float spawnTimer=0;
    float minXSpeed=-2;
    float maxXSpeed=-5;
    float minYSpeed=-2;
    float maxYSpeed=2;
    int random;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
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
        }
        spawnTimer -= Time.deltaTime;
    }
}
