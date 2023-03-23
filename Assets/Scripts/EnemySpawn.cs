using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    int prevType;
    float originScoreMultiply=1;
    public float catchTimer;
    float catchDuration = 3f;
    public static float scoreMultiply = 1;
    float bossAppearThres = 50;
    public static float bossTime=75f;
    public GameObject standardUI;
    public static float savedMaxSpeed;
    public GameObject waveText2;
    public static float empowerMultiplier = 1.1f;
    public GameObject boss;
    public Camera cam;
    public static int maxWave = 10;
    AudioSource source;
    public AudioClip ding;
    bool endOfTut = false;
    public float waveDuration = 6.5f;
    public static int waveCount = 1;
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
    public float waveTiming = 0f;
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
    public GameObject blueBombDuo;
    public GameObject mixed;
    public GameObject mixed2;
    public GameObject mixedShoot;
    public GameObject shieldWave;
    public GameObject homingOrangeSmall;
    public GameObject homingBlueSmall;
    public GameObject blueBombBig;
    public GameObject orangeBombBig;

    public GameObject waveAlert;

    public int bossWave = 10;
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
    float difficultyMultiply;
    public GameObject healthUI;

/*    int[] waveCheckpointList = new int[3] { 3,6,9 };*/
    // Start is called before the first frame update
    void Start()
    {
        switch (gun.index)
        {
            case 0:
                difficultyMultiply = 0.50f;
                waveTiming = 9f;
                break;
            case 1:
                difficultyMultiply = 1.0f;
                scoreMultiply = originScoreMultiply * 1.25f;
                waveTiming = 6f;
                break;
            case 2:
                difficultyMultiply = 1.35f;
                scoreMultiply = originScoreMultiply * 1.5f;
                waveTiming = 5f;
                break;
            default:
                difficultyMultiply = 1.0f;
                scoreMultiply = originScoreMultiply * 1.25f;
                waveTiming = 6.5f;
                break;

        }
        maxspeed = baseMaxSpeed;
        boss.SetActive(false);
        waveGraceTimer = waveGraceDuration;
        source = GetComponent<AudioSource>();
        breakCounter = breakCount;
        waveTime = waveDuration;
        positionArray = new GameObject[] { greenWave, blueWave, triangle, square, mixed, mixed2, shootWave, shootWave2, mixedShoot, homingOrangeSmall, homingBlueSmall, homingGreen, homingRed, rotate, bomber, blueBombDuo, orangeBombBig, blueBombBig, radial, greenShield, shieldWave, chameleon};
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        catchTimer -= Time.deltaTime;
        maxspeed = baseMaxSpeed+baseMaxSpeed* (waveCount*speedWaveMultiplier*difficultyMultiply);
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
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>Final Wave!</color><br><size=35> " + System.Math.Round(waveTime, 0) + " Seconds Left</size>";
                waveText2.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>Final Wave!</color><br><size=35> " + System.Math.Round(waveTime, 0) + " Seconds Left</size>";
            }
            else
            {
                standardUI.SetActive(true);
                waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>WAVE " + waveCount + " of "+maxWave+"</color><br><size=35>" + System.Math.Round(waveTime, 0) + " Seconds Left</size>"/* + waveTimer + " Break: " + breakCounter*/;
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
            if (waveCount != bossWave)
            {
                stopSpawn = true;
            }
            if (waveCount == bossWave && waveTime <= bossAppearThres && bossDestroyed)
            {
                beginNextWave = true;
                waveCount = 11;
                turret.GetComponent<Turret>().GetHighScore();
                SceneManager.LoadScene("Won");
            }
            if ((!GameObject.FindGameObjectWithTag("Enemy") && (!GameObject.FindGameObjectWithTag("Enemy2"))))
            {
                cam.GetComponent<CamZoom>().zoomIn = true;
/*                tankAnimate.GetComponent<Animator>().Play("Upgrade");*/
/*                tankAnimate.GetComponent<Animator>().SetBool("Upgrade", false);*/
                turret.GetComponent<Turret>().health = turret.GetComponent<Turret>().maxHealth;
                for(int i=0; i< turret.GetComponent<Turret>().malfunctionArray.Length; i++)
                {
                    turret.GetComponent<Turret>().malfunctionArray[i] = 0;
                }
                source.PlayOneShot(ding);                
                waveTime = 9999;
                waveCompleted = true;
                beginNextWave = false;
                catchTimer = catchDuration;
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
                healthUI.SetActive(false);
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
                if (waveCount == bossWave && bossActive==false && waveTime<=bossAppearThres)
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
                            createEnemies(0, 4);
                            break;
                        case 2:
                            createEnemies(1, 7);
                            break;
                        case 3:
                            createEnemies(2, 9);
                            break;
                        case 4:
                            createEnemies(3, 11);
                            break;
                        case 5:
                            createEnemies(4,14);
                            break;
                        case 6:
                            createEnemies(4, 16);
                            break;
                        case 7:
                            createEnemies(4, 17);
                            break;
                        case 8:
                            createEnemies(4, 21);
                            break;
                        case 9:
                            createEnemies(4, 21);
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
                            createEnemies(4, 21);
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
    private void createEnemies(int waveRestrictMin, int waveRestrict)
    {
        int type = Random.Range(waveRestrictMin, waveRestrict);
        while (type == prevType)
        {
            type= Random.Range(waveRestrictMin, waveRestrict);
        }
        prevType = type;
        genspeed = maxspeed;
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