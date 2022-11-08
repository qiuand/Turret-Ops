using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    public GameObject mechanicView;
    public GameObject gunnerView;
    public ParticleSystem minimapLaserGreen;
    public ParticleSystem minimapLaserRed;
    public bool electricOverride = false;
    float smashProjHeat = 40;
    public GameObject smashProj;
    public GameObject smashProj2;
    float smashProjectileSpeed = 1.0f;
    public bool smasher = false;
    public bool redShield = false;
    public bool autoRepair = false;
    public AudioClip overcharge;
    public AudioClip cooldownSound;
    public string installedUpgrade = "No";
    bool recharged = true;
    float rechargeDuration = 7.0f;
    float rechargeTime = 7.0f;
    float pierceDurationCool=5.0f;
    float pierceCooldownTime=5.0f;
    float abilityCooldown=0f;
    public bool pierceUpgrade = false;
    public GameObject projectile3;
    public string installedGun;
    public float chainGunCool = 0.1f;
    public float chainGunHeat = 15f;
    public bool chainGun = false;
    public GameObject greenLaserHB;
    public bool basicGun = true;
    public ParticleSystem laser;
    public bool laserUpgrade = false;
    public GameObject shotgunPos1;
    public GameObject shotgunPos2;
    public bool shotgun = false;
    public AudioClip select;
    public AudioClip repair;
    public AudioClip fix;
    public AudioClip overheat;
    public AudioClip deathExplode;
    public AudioClip malfunction;
    public AudioClip shootPlasma;
    public AudioClip shootGun;
    public AudioClip explode;
    public AudioSource source;
    public bool inTut = false;
    public int healthGain = 20;
    public Image overheatBar;
    public Image healthBar;
    public Image overheatBar2;
    public Image healthBar2;
    Vector3 barrelColour = new Vector3(0.6033731f, 0.8584906f, 0.8451912f);
    Vector3 damagedColour = new Vector3(1f, 0f, 0f);
    public float heat;
    float heatBuildUp;
    public float maxHeat = 100;
    float heatCoolDown = 100;
    public GameObject barrelEnd;
    float moveSpeed;
    float rotation;
    public GameObject projectile;
    public int projectileSpeed = 30;
    float shootCooldown;
    float cooldown = 0f;
    bool canRepair = true;
    public GameObject projecile3;
    public int thing;
    float repairAmountPerSwing = 20;
    public bool overheated = false;
    float repairCooldown = 1f;
    float repairTimer = 0;
    public GameObject barrel;
    public float originalShootCooldown = 0.2f;
    public float originalHeatBuildup = 20;
    public float health;
    public float maxHealth = 100f;
    public int damageTaken = 0;
    int maxDamBeforeMalfunction = 2;
    bool malfunctioning = false;
    string malfunctionType = "None";
    string[] malfunctionList = new string[] { "Cockpit", "Left wing", "Right wing", "Barrel" };
    public GameObject hullGUI;
    public GameObject rWingGUI;
    public GameObject lWingGUI;
    public GameObject cameraGUI;
    public GameObject barrelGUI;
    string inputDisplay = "";
    string codeDisplay = "None";
    int hullHits = 0;
    int hullHitsReq = 5;
    bool barrelChanged = false;
    public GameObject blackout;
    List<string> playerInput = new List<string>();
    public int maxInput;
    string[] leftWingCode = new string[] { "A", "D", "S", "W" };
    string[] rightWingCode = new string[] { "W", "S", "D", "A" };
    string[] cameraCode = new string[] { "S", "A", "D", "W" };
    string[] requiredCode = new string[] { };
    public GameObject rCodeText;
    public GameObject pCodeText;
    public GameObject mTypeText;
    bool correctNo = true;
    int brokenHeatBuild = 50;
    bool OldbarrelOut;
    public GameObject projectile2;
    bool NewbarrelIn;
    int installedBarrel = 1;
    int startingBarrel;
    int originalMoveSpeed = 70;
    bool barrelInserted = true;
    bool released = false;
    public GameObject turretSprite;
    bool detectedBarrel = false;
    bool needReload = false;
    bool magRelease = true;
    bool detectedMag = false;
    public int startingMag;
    bool lWing = false;
    bool rWing = false;
    bool cameraDamage = false;
    bool hullDamage = false;
    bool barrelHeated = false;
    bool centralDamaged = false;
    public int insertedChip;
    public GameObject actionStatus;
    public int reducedMoveSpeed = 10;
    float damageTick = 3f;
    float damageTimer = 0f;
    public GameObject barrelIcon;
    public int healthDamage = 5;
    string storedType = "No Upgrade";
    public int[] malfunctionArray;
    public int swungAt=0;
    int swungMax;
    public int hits = 5;
    float score=10;
    public GameObject yes;
    public AudioClip magazine;
    public GameObject lWingSelect;
    public GameObject rWingSelect;
    public GameObject barrelSelect;
    public GameObject hullSelect;
    bool leftMotionDamage=false;
    bool rightMotionDamage=false;
    float turnPenalty = 0.40f;
    public GameObject camText;
    public GameObject lWingText;
    public GameObject rWingText;
    public GameObject hullText;
    public GameObject barrelText;
    Vector3 defaultBarrelColour=new Vector3(1f,1f,1f);
    Vector3 currentBarrelColour = new Vector3();
    Vector3 blueBarrelColour = new Vector3(0.5f, 0, 0);
    Vector3 greenBarrelColour=new Vector3(0f, 0.5f, 0f);
    public GameObject scoreText;
    public GameObject scoreText2;
    public float scoreMultiplier = 10;
    float autoRepairMax=9;
    int repairAmount = 8;
    int decreaseHealthPart = 1;
    int decreaseHealthTime=1;
    int decreaseTimer=0;
    float debuffTimer=3.0f;
    float debuffTime=3.0f;
    bool canMalfunction;
    float shotgunRadius = 45f;
    public Animator muzzle;
    public GameObject smoke;
    public ParticleSystem smokeSystem;
    public GameObject tutLayer;
    public GameObject tutLayer2;
    float gameScore = 0;
    float gameScoreMultiplier=3;
    public GameObject gameScoreText;
    public GameObject gameScoreText2;
    public ParticleSystem lWingFire;
    public ParticleSystem rWingFire;
    public Animator ship;
    float deathTimer=0.0f;
    float deathDelay = 2.0f;
    bool exploded = false;
    public Animator shipExplode;
    float laserHeat = 15f;
    public AudioClip laserBlast;
    float laserSoundCoolTime=0f;
    float laserSoundDuration;
    float shottyHeat = 7f;
    public GameObject laser2HB;
    public ParticleSystem laser2;
    bool pierceActive = false;
    public GameObject powerupInfo;
    public string powerupInstalled;
    public GameObject powerupCoolText;
    bool upgradeActive = false;
    bool redShieldActive = false;
    public GameObject redShieldObject;
    // Start is called before the first frame update
    void Start()
    {
        gunnerView.SetActive(false);
        mechanicView.SetActive(false);
        redShieldObject.SetActive(false);
        installedGun = "Blaster";
        laser2.gameObject.SetActive(false);
        laser2HB.gameObject.SetActive(false);
        laser.gameObject.SetActive(false);
        laserSoundDuration = laserBlast.length;
        laser.enableEmission = false;
        minimapLaserGreen.gameObject.SetActive(false);
        minimapLaserRed.gameObject.SetActive(false);
        minimapLaserGreen.enableEmission = false;
        minimapLaserRed.enableEmission = false;
        health = maxHealth;
        currentBarrelColour = defaultBarrelColour;
        lWingSelect.SetActive(false);
        rWingSelect.SetActive(false);
        barrelSelect.SetActive(false);
        hullSelect.SetActive(false);
        blackout.SetActive(false);
        heatBuildUp = originalHeatBuildup;
        shootCooldown = originalShootCooldown;
        moveSpeed = originalMoveSpeed;
        malfunctionArray = new int[4] { 0, 0, 0, 0};
        swungMax = malfunctionArray.Length - 1;

        powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    // Update is called once per framwwwwswwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwsse
    void Update()
    {
        if (electricOverride == true)
        {
            gunnerView.SetActive(true);
            mechanicView.SetActive(true);
        }
        else
        {
            gunnerView.SetActive(false);
            mechanicView.SetActive(false);
        }
        if (electricOverride == true)
        {
            heat = 0;
        }
        powerupInfo.GetComponent<TMPro.TextMeshProUGUI>().text = installedUpgrade+" installed";
        /*        pierceActive = true;
                pierceUpgrade = true;*/
        if (Input.GetKeyDown("q")&& recharged==true && installedUpgrade!=null && installedUpgrade!= "Auto-repair Module") {
            StartCoroutine(PlayOvercharge());
            switch (installedUpgrade)
            {
                case "HEAT Round Module":
                    pierceActive = true;
                    break;
                case "Red Shield Module":
                    redShieldActive = true;
                    break;
            }
            /*source.PlayOneShot(overcharge);*/
            upgradeActive = true;
        }
        if (pierceActive)
        {
            print("yes");
        }
        if (pierceUpgrade)
        {
            print("nose");
        }
        if (redShieldActive == true)
        {
            redShieldObject.SetActive(true);
        }
        else
        {
            redShieldObject.SetActive(false);
        }
        if (upgradeActive == true)
        {
            powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = installedUpgrade+" Active: " + System.Math.Round(pierceCooldownTime, 2) + " seconds remaining";
            pierceCooldownTime -= Time.deltaTime;
            if (pierceCooldownTime <= 0)
            {
                source.PlayOneShot(cooldownSound) ;
                redShieldObject.SetActive(false);
                redShieldActive = false;
                pierceActive = false;
                recharged = false;
                rechargeTime = rechargeDuration;
                pierceCooldownTime = pierceDurationCool;
            }
        }
        rechargeTime -= Time.deltaTime;
        if (recharged == false)
        {
            upgradeActive = false;
            powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = System.Math.Round(rechargeTime, 2) + " seconds to recharge";
            rechargeTime -= Time.deltaTime;
            if (rechargeTime <= 0)
            {
                rechargeTime = rechargeDuration;
                recharged = true;
                powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Ability fully recharged";
            }
        }
        if (laserUpgrade == true)
            {
            minimapLaserGreen.gameObject.SetActive(true);
            minimapLaserRed.gameObject.SetActive(true);
            laser2.gameObject.SetActive(true);
                laser.gameObject.SetActive(true);
                basicGun = false;
            }
            else
            {
                laser.enableEmission = false;
            minimapLaserGreen.enableEmission = false;
            }
            laserSoundCoolTime -= Time.deltaTime;
            Cursor.visible = true;

            Cursor.visible = false;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathDelay && exploded == true)
            {
                laserUpgrade = false;
                shotgun = false;
                basicGun = true;
                Upgrades.canUpgrade = false;
                Upgrades.upgradesRolled = true;
                SceneManager.LoadScene("GameOver");
            }
            if (health <= 0)
            {
                /*            SceneManager.LoadScene("GameOver");*/
                print("ducky: " + deathTimer + health);
                if (exploded == false)
                {
                    source.PlayOneShot(deathExplode);
                    lWingFire.enableEmission = true;
                    rWingFire.enableEmission = true;
                    heat = 999;
                    overheated = true;
                    malfunctionArray[0] = hits;
                    malfunctionArray[1] = hits;
                    malfunctionArray[2] = hits;
                    malfunctionArray[3] = hits;

                    smokeSystem.enableEmission = true;
                    autoRepairMax = 999999;
                    deathTimer = 0;
                    source.PlayOneShot(explode);
                    exploded = true;
                    shipExplode.SetBool("Exploded", true);
                    ship.SetBool("Exploded", true);
                }


                print(1222);
            }
            if (Input.GetKeyDown("left"))
            {
                startingMag = 1;
                source.PlayOneShot(magazine);
            }
            if (Input.GetKeyDown("right"))
            {
                startingMag = 2;
                source.PlayOneShot(magazine);
            }
            if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
            {
                source.PlayOneShot(magazine);
            }
            if (Input.GetKeyDown("escape"))
            {
                SceneManager.LoadScene("Main");
            }
            if (inTut == false)
            {
                tutLayer.SetActive(false);
                tutLayer2.SetActive(false);
                gameScore += Time.deltaTime * gameScoreMultiplier;
                int gameScoreInt = Mathf.FloorToInt(gameScore);
                gameScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score:  " + gameScoreInt;
                gameScoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Score:  " + gameScoreInt;
            }
            else
            {
                tutLayer.SetActive(true);
                tutLayer2.SetActive(true);
            }
            inTut = gun.inTutorial;
            print(score);
            int scoreInt = Mathf.FloorToInt(score);
            score -= Time.deltaTime * scoreMultiplier;
        if (autoRepair == true)
        {
            if (health >= 100)
            {
                scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair on standby:  " + score;
                powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair on standby: " + score;
            }
            else
            {
                scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair Cooldown:  " + score;
                powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair Cooldown: " + score;
            }
            if (score <= 0)
            {
                health += repairAmount;
                score = 10;
            }
        }
            highLight();
            if (Input.GetKeyDown("g"))
            {
                HammerSwing();
            }
            if (Input.GetKeyDown("w")) {
                source.PlayOneShot(select);
                swungAt += 1;
            }
            if (Input.GetKeyDown("s")) {
                swungAt -= 1;
                source.PlayOneShot(select);
            }
            if (swungAt > swungMax)
            {
                source.PlayOneShot(select);
                swungAt = 0;
            }
            if (swungAt < 0)
            {
                source.PlayOneShot(select);
                swungAt = swungMax;
            }
            processMalfunction();
            /*        if (cameraDamage == true)
                    {
                        blackout.SetActive(true);
                        moveSpeed = reducedMoveSpeed;

                    }
                    else
                    {
                        blackout.SetActive(false);
                        moveSpeed = originalMoveSpeed;
                    }*/
            if (detectedBarrel == false)
            {
                if (Input.GetMouseButton(0))
                {
                    startingBarrel = 1;
                    detectedBarrel = true;
                }
                else if (Input.GetMouseButton(1))
                {
                    startingBarrel = 2;
                    detectedBarrel = true;
                }
                else
                {
                    barrelInserted = false;
                }
            }
            /*        if (detectedMag == false)
                    {
                        if (Input.GetAxis("Mouse X") > 0)gg
                        {
                            startingMag = 1;
                            detectedMag = true;
                        }
                        if (Input.GetMouseButton(1))
                        {
                            startingMag = 2;
                            detectedMag = true;
                        }
                    }*/
            if (health < 0)
            {
                health = 0;
            }
            if (damageTaken >= maxDamBeforeMalfunction/* && centralDamaged == false*/)
            {
                damageTaken = 0;
                randomMalfunction();
                source.PlayOneShot(malfunction);
                /*            malfunctioning = true;
                            if (malfunctionType == "Barrel")
                            {
                                storedType = malfunctionList[Random.Range(0, malfunctionList.Length)];
                            }
                            else
                            {
                                malfunctionType = malfunctionList[Random.Range(0, malfunctionList.Length)];
                            }
                            RunMalfunctions(malfunctionType, damagedColour);*/
            }
            /*        if (malfunctioning && hullDamage == true)
                    {
                        if (damageTimer <= 0)
                        {
                            health -= healthDamage * 2;
                            damageTimer = damageTick;
                        }
                        mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Critical Hull Damage!";
                        if (Input.GetKeyDown("g") && canRepair)
                        {
                            hullHits++;
                            health += repairAmountPerSwing;
                            canRepair = false;
                            repairTimer = repairCooldown;
                        }
                        if (repairTimer > 0)
                        {
                            repairTimer -= Time.deltaTime;
                        }
                        if (repairTimer <= 0)
                        {
                            canRepair = true;
                        }
                        if (hullHits >= hullHitsReq)
                        {
                            repairTimer = 0;
                            malfunctioning = false;
                            hullDamage = false;
                            mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hull Restored";
                            RunMalfunctions(malfunctionType, barrelColour);
                            centralDamaged = false;
                            if (storedType != "None")
                            {
                                storedType = "None";
                            }
                            malfunctioning = false;
                        }
                    }*/
            /*        if (malfunctioning == true && barrelHeated == true)
                    {
                        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                        {
                            released = true;
                            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Barrel removed";
                        }
                        if (released == true)
                        {
                            turretSprite.GetComponent<SpriteRenderer>().enabled = false;
                            barrelIcon.GetComponent<SpriteRenderer>().enabled = false;
                            if (startingBarrel == 1)
                            {
                                if (Input.GetMouseButtonDown(1))
                                {
                                    actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Barrel changed";
                                    overheated = false;
                                    heat = 0;
                                    startingBarrel = 2;
                                    RunMalfunctions(malfunctionType, barrelColour);
                                    released = false;
                                    barrelHeated = false;
                                    turretSprite.GetComponent<SpriteRenderer>().enabled = true;
                                    barrelIcon.GetComponent<SpriteRenderer>().enabled = true;
                                    if (storedType != "None")
                                    {
                                        malfunctionType = storedType;
                                        malfunctioning = true;
                                    }
                                    else
                                    {
                                        malfunctioning = false;
                                    }
                                    return;
                                }
                            }
                            else if (startingBarrel == 2)
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    malfunctioning = false;
                                    overheated = false;
                                    heat = 0;
                                    startingBarrel = 1;
                                    RunMalfunctions(malfunctionType, barrelColour);
                                    released = false;
                                    turretSprite.GetComponent<SpriteRenderer>().enabled = true;
                                    if (storedType != "None")
                                    {
                                        malfunctionType = storedType;
                                        malfunctioning = true;
                                    }
                                    else
                                    {
                                        malfunctioning = false;
                                    }
                                    return;
                                }
                            }
                        }

                    }*/
            /*        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
                    {
                        currentBarrelColour = defaultBarrelColour;
                        actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Magwell Empty";
                        detectedMag = false;
                    }
                    if (detectedMag == false)
                    {
            */
            if (Input.GetKey("right"))
            {
                currentBarrelColour = blueBarrelColour;
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Plasma " + installedGun + " Loaded";
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0, 0);
                startingMag = 2;
                detectedMag = true;
            }

            else if (Input.GetKey("left"))
            {
                currentBarrelColour = greenBarrelColour;
                /*                    barrel.GetComponent<SpriteRenderer>().color = new Color(0.4009f, 1f, 0.4507f);*/
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = ".50 CAL " + installedGun + " Loaded";
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 1f, 0);
                startingMag = 1;
                detectedMag = true;
            }
            else
            {
                currentBarrelColour = defaultBarrelColour;
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Empty " + installedGun;
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f);
                detectedMag = false;
            }


            /*        if (malfunctioning && malfunctionType != "Barrel" && malfunctionType != "Hull")
                    {
                        if (damageTimer <= 0)
                        {
                            health -= healthDamage;
                            damageTimer = damageTick;
                        }
                        damageTimer -= Time.deltaTime;
                        mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = malfunctionType;
                        pCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = inputDisplay;
                        rCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = requiredCode[0] + requiredCode[1] + requiredCode[2] + requiredCode[3];
                        if ((Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") || Input.GetKeyDown("w")) && playerInput.Count < maxInput)
                        {
                            if (Input.GetKeyDown("a"))
                            {
                                playerInput.Add("A");
                                inputDisplay += "A";
                            }
                            else if (Input.GetKeyDown("s"))
                            {
                                playerInput.Add("S");
                                inputDisplay += "S";
                            }
                            else if (Input.GetKeyDown("d"))
                            {
                                playerInput.Add("D");
                                inputDisplay += "D";
                            }
                            else if (Input.GetKeyDown("w"))
                            {
                                playerInput.Add("W");
                                inputDisplay += "W";
                            }
                        }
                        if (playerInput.Count >= maxInput)
                        {
                            for (int i = 0; i < maxInput; i++)
                            {
                                if (playerInput[i] != requiredCode[i])
                                {
                                    print(1);
                                    correctNo = false;
                                    break;
                                }
                                else
                                {
                                    correctNo = true;
                                }
                            }
                            if (correctNo == false)
                            {
                                damagePlayer();
                                playerInput.Clear();
                                inputDisplay = "";
                            }
                            if (correctNo)
                            {
                                mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Code: N/A";
                                pCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Input: N/A";
                                rCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Normal";
                                malfunctioning = false;
                                playerInput = new List<string>() { };
                                RunMalfunctions(malfunctionType, barrelColour);
                                inputDisplay = "";
                                centralDamaged = false;
                                correctNo = true;
                                storedType = "None";
                                malfunctioning = false;
                            }
                        }
                    }*/
            overheatBar.fillAmount = heat / maxHeat;
            healthBar.fillAmount = health / maxHealth;
            overheatBar2.fillAmount = heat / maxHeat;
            healthBar2.fillAmount = health / maxHealth;
            barrel.GetComponent<SpriteRenderer>().color = new Color(currentBarrelColour.x + heat / 100, currentBarrelColour.y - heat / 400, currentBarrelColour.z);
            if (currentBarrelColour == defaultBarrelColour)
            {
                barrel.GetComponent<SpriteRenderer>().color = new Color(currentBarrelColour.x, currentBarrelColour.y - heat / 100, currentBarrelColour.z - heat / 100);
            }
            /*statusText.GetComponent<TMPro.TextMeshProUGUI>().color = barrel.GetComponent<SpriteRenderer>().color;*/
            if (heat >= maxHeat)
            {
                heat = maxHeat;
                if (overheated == false)
                {
                    source.PlayOneShot(overheat);
                    overheated = true;
                minimapLaserGreen.gameObject.SetActive(false);
                laser.enableEmission = false;
                    malfunctionArray[3] = hits;
                }
                /*            barrelHeated = true;
                            if (malfunctionType != "None" && malfunctionType != "Barrel")
                            {
                                malfunctioning = true;
                                storedType = malfunctionType;
                            }
                            malfunctionType = "Barrel";
                            malfunctioning = true;
                            RunMalfunctions(malfunctionType, Damagedcolour);*/
            }
            if (Input.GetKey("space") && overheated == false && detectedMag == true)
            {
                if (chainGun == true)
                {
                    shootCooldown = chainGunCool;
                    heatBuildUp = chainGunHeat;
                }
                else
                {
                    heatBuildUp = originalHeatBuildup;
                    shootCooldown = originalShootCooldown;
                }
                muzzle.Play("Muzzle");
                muzzle.SetBool("Firing", true);
                if (cooldown <= 0)
                {
                    if (startingMag == 1)
                    {
                    if (smasher == true)
                    {
                        source.PlayOneShot(laserBlast);
                        heat += smashProjHeat;
                        GameObject smashProjectile = Instantiate(smashProj2, barrelEnd.transform.position, transform.rotation);
                        smashProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * smashProjectileSpeed;
                    }
                        if (shotgun == true)
                        {
                            GameObject shotgunBullet = Instantiate(projectile, shotgunPos1.transform.position, shotgunPos1.transform.rotation);
                            shotgunBullet.GetComponent<Rigidbody2D>().velocity = shotgunPos1.transform.right * projectileSpeed;
                            GameObject shotgunBullet2 = Instantiate(projectile, shotgunPos2.transform.position, shotgunPos2.transform.rotation);
                            shotgunBullet2.GetComponent<Rigidbody2D>().velocity = shotgunPos2.transform.right * projectileSpeed;
                            heat += shottyHeat;
                        }
                        if (laserUpgrade == true)
                        {
                            greenLaserHB.SetActive(true);
                            heat += laserHeat;
                                minimapLaserGreen.enableEmission=true;
                            laser.enableEmission = true;
                            if (laserSoundCoolTime < -0)
                            {
                                source.PlayOneShot(laserBlast);
                                laserSoundCoolTime = laserSoundDuration;
                            }
                        }
                        if (basicGun == true)
                        {
                            source.PlayOneShot(shootGun, 1.0f);
                            GameObject bullet = Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
                            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                        }
                        if (pierceUpgrade == true && pierceActive == true)
                        {
                            source.PlayOneShot(shootGun, 1.0f);
                            GameObject bullet = Instantiate(projectile3, barrelEnd.transform.position, transform.rotation);
                            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                        }

                    }
                    else
                    {
                    if (smasher == true)
                    {
                        source.PlayOneShot(laserBlast);
                        heat += smashProjHeat;
                        GameObject smashProjectile = Instantiate(smashProj, barrelEnd.transform.position, transform.rotation);
                        smashProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * smashProjectileSpeed;
                    }
                    if (shotgun == true)
                        {
                            GameObject shotgunBullet = Instantiate(projectile2, shotgunPos1.transform.position, shotgunPos1.transform.rotation);
                            shotgunBullet.GetComponent<Rigidbody2D>().velocity = shotgunPos1.transform.right * projectileSpeed;
                            GameObject shotgunBullet2 = Instantiate(projectile2, shotgunPos2.transform.position, transform.rotation);
                            shotgunBullet2.GetComponent<Rigidbody2D>().velocity = shotgunPos2.transform.right * projectileSpeed;
                            heat += shottyHeat;
                        }
                        if (laserUpgrade == true)
                        {
                            heat += laserHeat;
                            laser2HB.SetActive(true);
                        minimapLaserRed.enableEmission = true;
                            laser2.enableEmission = true;
                            if (laserSoundCoolTime < -0)
                            {
                                source.PlayOneShot(laserBlast);
                                laserSoundCoolTime = laserSoundDuration;
                            }
                        }
                        if (pierceUpgrade == true && pierceActive == true)
                        {
                            source.PlayOneShot(shootGun, 1.0f);
                            GameObject bullet = Instantiate(projectile3, barrelEnd.transform.position, transform.rotation);
                            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                        }
                        if (basicGun == true)
                        {
                            source.PlayOneShot(shootPlasma, 1.0f);
                            GameObject bullet = Instantiate(projectile2, barrelEnd.transform.position, transform.rotation);
                            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                        }
                    }
                    cooldown = shootCooldown;
                    heat += heatBuildUp;
                }
            }
            else
            {
                greenLaserHB.SetActive(false);
                laser.enableEmission = false;
                laser2HB.SetActive(false);
                laser2.enableEmission = false;
            minimapLaserRed.enableEmission = false;
            minimapLaserGreen.enableEmission = false;
            }
            if (cooldown > 0)
            {
                muzzle.SetBool("Firing", false);
            }


            else if (overheated == false)
            {
                if (heat < 0)
                {
                    heat = 0;
                }
                if (heat > 0)
                {
                    heat -= Time.deltaTime * heatCoolDown;
                }
            }
            /*        if (overheated)
                    {
                        statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Critical Overheat � Repairs Needed!";

                    }
                    else
                    {
                        statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Normal";
                    }*/

            if (malfunctioning == true)
            {
                shootCooldown = 1f;
                heatBuildUp = brokenHeatBuild;
            }
            else
            {
                shootCooldown = originalShootCooldown;
                heatBuildUp = originalHeatBuildup;
            }
            cooldown -= Time.deltaTime;
            rotation = Input.GetAxis("Vertical");
            if (rotation > 0 && leftMotionDamage)
            {
                transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed * turnPenalty);
            }
            else if (rotation < 0 && rightMotionDamage)
            {
                transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed * turnPenalty);
            }
            else
            {
                print("bugs");
                transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed);
            }
            /*        transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed);*/
            if (debuffTimer <= 0) {
                canMalfunction = true;
            }
            debuffTimer -= Time.deltaTime;
        }

        /*    private void RunMalfunctions(string malfunctionType, Vector3 colour)
            {
                centralDamaged = true;
                switch (malfunctionType)
                {
                    case "Cockpit":
                        cameraDamage = true;
                        requiredCode = cameraCode;
                        cameraGUI.GetComponent<SpriteRenderer>().color = new Color(colour.x, colour.y, colour.z);
                        break;
                    case "Left wing":
                        lWing = true;
                        requiredCode = leftWingCode;
                        lWingGUI.GetComponent<SpriteRenderer>().color = new Color(colour.x, colour.y, colour.z);
                        break;
                    case "Right wing":
                        rWingGUI.GetComponent<SpriteRenderer>().color = new Color(colour.x, colour.y, colour.z);
                        requiredCode = rightWingCode;
                        break;
                    case "Hull":
                        hullDamage = true;
                        hullGUI.GetComponent<SpriteRenderer>().color = new Color(colour.x, colour.y, colour.z);
                        health -= 1;
                        break;
                    case "Barrel":
                        barrelGUI.GetComponent<SpriteRenderer>().color = new Color(colour.x, colour.y, colour.z);
                        break;
                }
            }*/
    private void Camera()
    {
        blackout.SetActive(true);
    }
    private void damagePlayer()
    {
        health--;
    }
    private void randomMalfunction()
    {
        int random = Random.Range(0, malfunctionArray.Length-1);
        malfunctionArray[random] = hits;
    }
    private void processMalfunction()
    {
        if (malfunctionArray[0]<= 0)
        {
            rightMotionDamage = false;
            rWingGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            malfunctionArray[0] = 0;
            rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Right Wing Systems Normal";
            rWingText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1, 1);
            rWingFire.enableEmission = false;
        }
        else
        {
            rWingMalfunction();
            rWingGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
        }
        if (malfunctionArray[1] == 0)
        {
            lWingText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1, 1);
            leftMotionDamage = false;
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            malfunctionArray[1] = 0;
            lWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Left Wing Systems Normal";
            lWingFire.enableEmission = false;
        }
        else
        {
            lWingMalfunction();
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
        }
        if (malfunctionArray[2] == 0)
        {
            camText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1, 1);
            blackout.SetActive(false);
            hullGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            camText.GetComponent<TMPro.TextMeshProUGUI>().text="Status: Cam Systems Functional";
            hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Cockpit Systems Normal";
            camText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f,1f,1f);
            hullText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f);
        }
        else{
            print("jes");
            camText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0f, 0f);
            hullGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
            hullMalfunction();
            camText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Critical Cam System Damage!";
        }
/*        if (malfunctionArray[3] == 0)
        {
            malfunctionArray[3] = 0;
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
        }
        else{
            cameraGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
            camMalfunction();
        }*/
        if (malfunctionArray[3] == 0)
        {
            barrelText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1, 1);
            malfunctionArray[3] = 0;
            barrelGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            if (overheated == true)
            {
                overheated = false;
                heat = maxHeat - 1;
            }
            smokeSystem.enableEmission = false;
            barrelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Turret Systems Normal";
        }
        else
        {
            barrelGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
            barrelMalfunction();
        }
    }
    private void damageColour() {
    }
    private void returnColour() {
    }
    private void HammerSwing()
    {
        if (malfunctionArray[swungAt] > 0)
        {
            if (malfunctionArray[swungAt] ==1){
                debuffTimer = 5.0f;
                source.PlayOneShot(fix);
            }
            source.PlayOneShot(repair);
            malfunctionArray[swungAt] -= 1;
        }

    }
    private void highLight()
    {
        switch (swungAt)
        {
            case (0):
                rWingSelect.SetActive(true);
                lWingSelect.SetActive(false);
                hullSelect.SetActive(false);
                barrelSelect.SetActive(false);
                break;
            case (1):
                lWingSelect.SetActive(true);
                rWingSelect.SetActive(false);
                hullSelect.SetActive(false);
                barrelSelect.SetActive(false);
                break;
            case (2):
                hullSelect.SetActive(true);
                rWingSelect.SetActive(false);
                lWingSelect.SetActive(false);
                barrelSelect.SetActive(false);
                break;
            case (3):
                barrelSelect.SetActive(true);
                rWingSelect.SetActive(false);
                lWingSelect.SetActive(false);
                hullSelect.SetActive(false);
                break;
        }
    }
    private void rWingMalfunction()
    {
        rWingFire.enableEmission = true;
        rWingText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0, 0);
        rightMotionDamage = true;
        rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Right Wing damaged: -50% leftwards rotation speed. Hit "+malfunctionArray[0]+" times";
        rightMotionDamage = true;
    }
    private void lWingMalfunction()
    {

        lWingText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f,0,0);
        lWingFire.enableEmission = true;
        lWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Left Wing damaged: -50% rightwards rotation speed. Hit " + malfunctionArray[1] + " times";
        leftMotionDamage = true;
    }
    private void hullMalfunction()
    {
        hullText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0, 0);
        print("yes");
        hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Cockpit integrity compromised: Turret camera unavailabe. Hit " + malfunctionArray[2] + " times";
        /*        switchColours(hullText, damagedColour);*/
        /*        hullText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);*/
        if (exploded != true)
        {
            blackout.SetActive(true);
        }
        else
        {
            blackout.SetActive(false);
        }
    }
    private void camMalfunction()
    {        
    }
    private void barrelMalfunction()
    {
        barrelText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0, 0);
        smokeSystem.enableEmission = true;
        smoke.gameObject.transform.eulerAngles = new Vector3(0, 0,90);
        barrelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Melted Barrel: Unable to fire. Hit " + malfunctionArray[3] + " times";
    }
    private void switchColours(GameObject obj, Vector3 change)
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(change.x,change.y,change.z);
    }
    private void damageComponent()
    {
        health -= decreaseHealthPart;
    }
    IEnumerator PlayOvercharge()
    {
        source.PlayOneShot(overcharge);
        yield return new WaitForSeconds(2.0f);
    }
}