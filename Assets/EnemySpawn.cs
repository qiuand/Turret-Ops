using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    bool endOfTut = false;
    public float waveDuration = 5f;
    public int waveCount = 1;
    public float waveTime;
    public float waveTimeIncrement=5f;
    public GameObject enemy2;
    public GameObject enemy;
    RectTransform rect;
    float spawnCooldown = 0.75f;
    float spawnTimer = 0;
    float minXSpeed = -0.01f;
    float maxXSpeed = -0.02f;
    float minYSpeed = -2;
    float maxYSpeed = 2;
    int random;
    bool isEnemy2 = false;
    public float waveTimer = 0f;
    public float waveTiming = 3f;
    public int enemyNum = 3;
    float minSpeed = -1;
    float maxSpeed = -2;
    public float minRotate = 3;
    public float maxRotate = -3;
    float genSpeed;
    public GameObject turret;
    public float enemySpeedMultiplier = 0.1f;
    GameObject[] positionArray;
    public GameObject greenWave;
    public GameObject blueWave;
    public GameObject shootWave;
    public GameObject shootWave2;
    bool waveCompleted = false;
    public GameObject waveText;
    public static bool beginNextWave = true;
    float breakCounter = 3.0f;
    float breakCount=10.0f;
    float gracePeriod = 3.0f;
    public GameObject upgradeManager;
    // Start is called before the first frame update
    void Start()
    {
        breakCounter = breakCount;
        waveTime = waveDuration;
        positionArray = new GameObject[] { greenWave, blueWave, shootWave, shootWave2 };
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        /*        if (waveCompleted == true)
                {
                    breakCounter -= Time.deltaTime;
                }
                else
                {
                    waveTime -= Time.deltaTime;
                }
                if (breakCounter <= 0)
                {
                    waveCompleted = false;
                    beginNextWave = true;
                    breakCounter = breakCount;
                    waveTime += waveTimeIncrement;
                }*/
        if (gun.inTutorial == false)
        {
            waveTime -= Time.deltaTime;
        }
        waveTimer -= Time.deltaTime;
        if (waveTime > 0)
        {
            waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + waveCount + ": " + System.Math.Round(waveTime, 2) + " Seconds Remaining: " + waveTimer + " Break: " + breakCounter;
        }
        else
        {
            waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Continue";
        }

        if (waveTime < 0)
        {
            waveTime = 999;
            waveCompleted = true;
            beginNextWave = false;

            Upgrades.upgradesRolled = false;
            if (Upgrades.upgradesRolled == false)
            {
                upgradeManager.GetComponent<Upgrades>().RollUpgrades();
                Upgrades.upgradesRolled = true;
            }

        }
/*        if (*//*Input.GetKeyDown("g") &&*//* beginNextWave == false && waveTime<0-gracePeriod)
        {
            waveCount++;
            beginNextWave = true;
            waveDuration += waveTimeIncrement;
            waveTime = waveDuration;
            waveTimer = waveTiming;
        }*/
        if (gun.inTutorial == false && beginNextWave == true)
        {
            maxSpeed -= Time.deltaTime * enemySpeedMultiplier;
        }
        if (turret.GetComponent<Turret>().inTut == false)
        {
            if (waveTimer <= 0 && beginNextWave == true)
            {
                switch (waveCount)
                {
                    case 1:
                        createEnemies(1);
                        break;
                    case 2:
                        createEnemies(2);
                        break;
                    case 3:
                        createEnemies(3);
                        break;
                    case 4:
                        createEnemies(4);
                        break;
                    default:
                        createEnemies(4);
                        break;
                }
                /*                for (int i = 0; i < enemyNum; i++)
                                {
                                    if (type == 0)
                                    {
                                        {
                                            GameObject enemyInstance = Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(minRotate, maxRotate))));
                                            enemyInstance.GetComponent<Rigidbody2D>().velocity = transform.right * (genSpeed);
                                        }
                                    }
                                    else
                                    {
                                        GameObject enemyInstance = Instantiate(enemy2, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                                        enemyInstance.GetComponent<Rigidbody2D>().velocity = transform.right * (genSpeed);
                                    }
                                }*/
            }
            spawnTimer -= Time.deltaTime;
        }
    }
    private void createEnemies(int waveRestrict)
    {
        int type = Random.Range(0, waveRestrict);
        genSpeed = Random.Range(minSpeed, maxSpeed);
        GameObject waveControl = Instantiate(positionArray[type], new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(minRotate, maxRotate))));
        waveControl.GetComponent<Rigidbody2D>().velocity = transform.up * (genSpeed);
        waveTimer = waveTiming;
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
}