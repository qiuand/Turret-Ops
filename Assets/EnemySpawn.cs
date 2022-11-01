using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    RectTransform rect;
    float spawnCooldown=0.5f;
    float spawnTimer=0;
    float minXSpeed=-2;
    float maxXSpeed=-5;
    float minYSpeed=-2;
    float maxYSpeed=2;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        print(rect.rect.xMin+""+ rect.rect.xMax);
        if (spawnTimer <= 0)
        {
            GameObject enemyInstance= Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
            enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
            spawnTimer = spawnCooldown;
        }
        spawnTimer -= Time.deltaTime;
    }
}
