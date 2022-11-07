using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
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
    int projectileSpeed = 30;
    float shootCooldown;
    float cooldown = 0f;
    bool canRepair = true;
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
    string storedType = "None";
    public int[] malfunctionArray;
    int swungAt=0;
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
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per framwwwwswwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwsse
    void Update()
    {
        Cursor.visible=true;
/*
            Cursor.visible = false;*/
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        deathTimer += Time.deltaTime;
        if (deathTimer >= deathDelay&&exploded==true)
        {
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
            int gameScoreInt =Mathf.FloorToInt(gameScore);
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
        if (score <= 0)
        {
            health += repairAmount;
            score = 10;
        }
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair Cooldown:  " + score;
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair Cooldown: " + score;
        highLight();
        if (Input.GetKeyDown("g"))
        {
            HammerSwing();
        }
        if (Input.GetKeyDown("w")){
            source.PlayOneShot(select);
            swungAt += 1;
        }
        if (Input.GetKeyDown("s")){
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
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Plasma Mag Loaded";
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0, 0);
            startingMag = 2;
                detectedMag = true;
            }

                else if (Input.GetKey("left"))
                {
                    currentBarrelColour = greenBarrelColour;
/*                    barrel.GetComponent<SpriteRenderer>().color = new Color(0.4009f, 1f, 0.4507f);*/
                    actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = ".50 CAL Mag Loaded";
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color=new Color(0,1f,0);
            startingMag = 1;
                    detectedMag = true;
                }
        else
        {
            currentBarrelColour = defaultBarrelColour;
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Magwell Empty";
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f,1f,1f);
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
        barrel.GetComponent<SpriteRenderer>().color = new Color(currentBarrelColour.x + heat / 100, currentBarrelColour.y - heat/400, currentBarrelColour.z);
        if (currentBarrelColour == defaultBarrelColour)
        {
            barrel.GetComponent<SpriteRenderer>().color = new Color(currentBarrelColour.x, currentBarrelColour.y-heat/100, currentBarrelColour.z - heat / 100);
        }
        /*statusText.GetComponent<TMPro.TextMeshProUGUI>().color = barrel.GetComponent<SpriteRenderer>().color;*/
        if (heat >= maxHeat)
        {
            heat = maxHeat;
            if (overheated == false)
            {
                source.PlayOneShot(overheat);
                overheated = true;
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
            muzzle.Play("Muzzle");
            muzzle.SetBool("Firing", true);
            if (cooldown <= 0)
            {
                if (startingMag == 1)
                {
                    source.PlayOneShot(shootGun,1.0f);
                    GameObject bullet = Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                }
                else
                {
                    source.PlayOneShot(shootPlasma,1.0f);
                    GameObject bullet = Instantiate(projectile2, barrelEnd.transform.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                }
                cooldown = shootCooldown;
                heat += heatBuildUp;
            }
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
            statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Critical Overheat — Repairs Needed!";

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
        if(rotation>0 && leftMotionDamage)
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed*turnPenalty);
        }
        else if(rotation<0 && rightMotionDamage)
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed*turnPenalty);
        }
        else
        {
            print("bugs");
            transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed);
        }
/*        transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed);*/
        if (debuffTimer <= 0){
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
            rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
            rWingFire.enableEmission = false;
        }
        else
        {
            rWingMalfunction();
            rWingGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
        }
        if (malfunctionArray[1] == 0)
        {
            leftMotionDamage = false;
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            malfunctionArray[1] = 0;
            lWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
            lWingFire.enableEmission = false;
        }
        else
        {
            lWingMalfunction();
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
        }
        if (malfunctionArray[2] == 0)
        {
            blackout.SetActive(false);
            hullGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            camText.GetComponent<TMPro.TextMeshProUGUI>().text="Status: Cam Systems Functional";
            hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
            camText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f,1f,1f);
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
            malfunctionArray[3] = 0;
            barrelGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            if (overheated == true)
            {
                overheated = false;
                heat = maxHeat - 1;
            }
            smokeSystem.enableEmission = false;
            barrelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
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
        rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Right Wing damaged: -50% leftwards rotation speed. Hit "+malfunctionArray[0]+" times";
        rightMotionDamage = true;
    }
    private void lWingMalfunction()
    {
        lWingFire.enableEmission = true;
        lWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Left Wing damaged: -50% rightwards rotation speed. Hit " + malfunctionArray[1] + " times";
        leftMotionDamage = true;
    }
    private void hullMalfunction()
    {
        print("yes");
        hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Cockpit integrity compromised: Turret camera unavailabe. Hit " + malfunctionArray[2] + " times";
        /*        switchColours(hullText, damagedColour);*/
/*        hullText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);*/
        blackout.SetActive(true);
    }
    private void camMalfunction()
    {        
    }
    private void barrelMalfunction()
    {
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
}