using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public static float bossTime=35f;
    public GameObject standardUI;
    public static float savedMaxSpeed;
    public GameObject waveText2;
    public static float empowerMultiplier = 1.1f;
    public GameObject boss;
    public Camera cam;
    public static int maxWave = 7;
    AudioSource source;
    public AudioClip ding;
    bool endOfTut = false;
    public float waveDuration = 6.5f;
    public static int waveCount = 7;
    public float waveTime;
    public float waveTimeIncrement=5f;
    public GameObject enemy2;
    public GameObject enemy;
    RectTransform rect;
    float spawnCooldown = 0.75f;
    float spawnTimer = 0;
    float minXspeed = -0.01f;
    float maxXspeed = -0.02f;
    float minYspeed = -2;
    float maxYspeed = 2;
    int random;
    bool isEnemy2 = false;
    public float waveTimer = 0f;
    public float waveTiming = 3f;
    public int enemyNum = 3;
    public float baseMaxSpeed = -1.45f;
    public float speedWaveMultiplier = 0.01f;
    public float minspeed = -1.0f;
    public float maxspeed = -1.45f;
    public float minRotate = 3;
    public float maxRotate = -3;
    float genspeed;
    public GameObject turret;
    float enemyspeedMultiplier = 0.005f;
    GameObject[] positionArray;
    public GameObject greenWave;
    public GameObject blueWave;
    public GameObject shootWave;
    public GameObject shootWave2;
    public GameObject triangle;
    public GameObject square;
    public GameObject homingGreen;
    public GameObject homingRed;
    public GameObject bomber;
    public GameObject radial;
    public GameObject greenShield;
    public GameObject rotate;
    int bossWave = 7;
    public GameObject chameleon;
    bool waveCompleted = false;
    public GameObject waveText;
    public static bool beginNextWave = true;
    float breakCounter = 3.0f;
    float breakCount=10.0f;
    float gracePeriod = 3.0f;
    public GameObject upgradeManager;
    public bool upgradeTrigger = true;
    public bool upgradeWaveChance = true;
    public bool stopSpawn = false;
    public bool waveGrace = false;
    float waveGraceTimer;
    float waveGraceDuration = 3.0f;
    public GameObject tankAnimate;
    public bool bossActive = false;
    public bool bossDestroyed = false;
/*    int[] waveCheckpointList = new int[3] { 3,6,9 };*/
    // Start is called before the first frame update
    void Start()
    {
        maxspeed = baseMaxSpeed;
        boss.SetActive(false);
        waveGraceTimer = waveGraceDuration;
        source = GetComponent<AudioSource>();
        breakCounter = breakCount;
        waveTime = waveDuration;
        positionArray = new GameObject[] { greenWave, blueWave, triangle, square, shootWave, shootWave2, homingGreen, homingRed, bomber, radial, greenShield, rotate, chameleon};
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        maxspeed = baseMaxSpeed+baseMaxSpeed* (waveCount*speedWaveMultiplier);
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
            waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Waiting for wave end...";
            if (waveCompleted && beginNextWave==false && waveCount!=bossWave)
            {
                standardUI.SetActive(false);
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave Completed!";

            }
            else if (waveCount == bossWave)
            {
                standardUI.SetActive(true);
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Wave!";
                waveText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Wave!";
            }
            else
            {
                standardUI.SetActive(true);
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>WAVE " + waveCount + "/"+maxWave+": " + System.Math.Round(waveTime, 0) + " Seconds Left"/* + waveTimer + " Break: " + breakCounter*/;
                waveText2.GetComponent<TMPro.TextMeshProUGUI>().text = waveText.GetComponent<TMPro.TextMeshProUGUI>().text;
            }

        }
        /*        else
                {
                    waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Continue";
                }*/
        if ((!GameObject.FindGameObjectWithTag("Enemy") && (!GameObject.FindGameObjectWithTag("Enemy2"))))
        {
/*            if (waveCount == bossWave)
            {
                SceneManager.LoadScene("Won");
            }*/
        }
            if (waveTime < 0)
        {
            stopSpawn = true;
            if ((!GameObject.FindGameObjectWithTag("Enemy") && (!GameObject.FindGameObjectWithTag("Enemy2"))))
            {
                if (waveCount == bossWave)
                {
                    SceneManager.LoadScene("Won");
                }
                cam.GetComponent<CamZoom>().zoomIn = true;
/*                tankAnimate.GetComponent<Animator>().Play("Upgrade");*/
/*                tankAnimate.GetComponent<Animator>().SetBool("Upgrade", false);*/
                turret.GetComponent<Turret>().health = turret.GetComponent<Turret>().maxHealth;
                for(int i=0; i< turret.GetComponent<Turret>().malfunctionArray.Length; i++)
                {
                    turret.GetComponent<Turret>().malfunctionArray[i] = 0;
                }
                source.PlayOneShot(ding);                waveTime = 9999;
                waveCompleted = true;
                beginNextWave = false;
                upgradeTrigger = true;
                stopSpawn = false;
                waveGrace = true;
            }
/*            if (Turret.scoreToUpgrade >= turret.GetComponent<Turret>().scoreToUpgradeRequired)
            {
                upgradeTrigger = true;
                upgradeWaveChance = false;
            }
            else
            {

                    upgradeManager.GetComponent<Upgrades>().Skip();
            }*/
        }
/*        if (waveGrace)
        {
            waveGraceTimer -= Time.deltaTime;
            if (waveGraceTimer < -0)
            {
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave "+waveCount+" beginning in"+System.Math.Round(waveGraceTimer, 2)+" seconds...";
                beginNextWave = true;
                waveCompleted = true;
                waveGrace = false;
            }
        }*/
        if (beginNextWave == false && waveCompleted==true && /*Upgrades.upgradesRolled==true*/upgradeTrigger)
            {
                Upgrades.upgradesRolled = false;
                if (Upgrades.upgradesRolled == false)
                {
                    upgradeManager.GetComponent<Upgrades>().RollUpgrades();
                    Upgrades.upgradesRolled = true;
                    upgradeTrigger = false;
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
/*            maxspeed -= (Time.deltaTime * enemyspeedMultiplier);*/
        }
        if (bossDestroyed == true && waveCount == bossWave && (!GameObject.FindGameObjectWithTag("Enemy") && (!GameObject.FindGameObjectWithTag("Enemy2"))))
        {
            boss.SetActive(false);
            SceneManager.LoadScene("Won");
            /*            bossActive = false;*/
            waveCompleted = true;
            beginNextWave = false;
        }
        if (turret.GetComponent<Turret>().inTut == false && stopSpawn==false)
        {
            if (waveTimer <= 0 && beginNextWave == true)
            {
                if (waveCount == bossWave && bossActive==false)
                {
                    boss.SetActive(true);
                    GameObject bossControl = Instantiate(positionArray[positionArray.Length-1], new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(minRotate, maxRotate))));
                    bossActive = true;

                }
                else
                {
                    switch (waveCount)
                    {
                        case 1:
                            createEnemies(2);
                            break;
                        case 2:
                            createEnemies(4);
                            break;
                        case 3:
                            createEnemies(6);
                            break;
                        case 4:
                            createEnemies(8);
                            break;
                        case 5:
                            createEnemies(10);
                            break;
                        case 6:
                            createEnemies(12);
                            break;
                        case 7:
                            createEnemies(12);
                            break;
/*                        case 8:
                            createEnemies(9);
                            break;
                        case 9:
                            createEnemies(9);
                            break;
                        case 10:
                            createEnemies(10);
                            break;
                        case 11:
                            createEnemies(11);
                            break;
                        case 12:
                            createEnemies(12);
                            break;*/
/*                        case 7:
                            createEnemies(11);
                            break;*/
/*                        case 8:
                            break;*/
                        default:
                            createEnemies(11);
                            break;
                    }
                }
                /*                for (int i = 0; i < enemyNum; i++)
                                {
                                    if (type == 0)
                                    {
                                        {
                                            GameObject enemyInstance = Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(minRotate, maxRotate))));
                                            enemyInstance.GetComponent<Rigidbody2D>().velocity = transform.right * (genspeed);
                                        }
                                    }
                                    else
                                    {
                                        GameObject enemyInstance = Instantiate(enemy2, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                                        enemyInstance.GetComponent<Rigidbody2D>().velocity = transform.right * (genspeed);
                                    }
                                }*/
            }
            spawnTimer -= Time.deltaTime;
        }
    }
    private void createEnemies(int waveRestrict)
    {
        int type = Random.Range(0, waveRestrict);
        genspeed = Random.Range(minspeed, maxspeed);
        GameObject waveControl = Instantiate(positionArray[type], new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(minRotate, maxRotate))));
        if (positionArray[type]!=homingRed && positionArray[type] != homingGreen)
        {
            waveControl.GetComponent<Rigidbody2D>().velocity = transform.up * (genspeed);
        }
        waveTimer = waveTiming;
    }
    /*        if (spawnTimer <= 0)
            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    GameObject enemyInstance = Instantiate(enemy, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                    enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXspeed, maxXspeed), Random.Range(minYspeed, maxYspeed));
                }
                else
                {
                    GameObject enemyInstance = Instantiate(enemy2, new Vector3(Random.Range(rect.rect.xMin, rect.rect.xMax), Random.Range(rect.rect.yMin, rect.rect.yMax)) + rect.transform.position, Quaternion.identity);
                    enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(minXspeed, maxXspeed), Random.Range(minYspeed, maxYspeed));
                }
                spawnTimer = spawnCooldown;
            }*/
}