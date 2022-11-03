using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Turret : MonoBehaviour
{
    public int healthGain = 20;
    public Image overheatBar;
    public Image healthBar;
    Vector3 barrelColour = new Vector3(0.6033731f, 0.8584906f, 0.8451912f);
    Vector3 damagedColour = new Vector3(0.8679245f, 0.4380563f, 0.4554211f);
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
    bool overheated = false;
    float repairCooldown = 1f;
    float repairTimer = 0;
    public GameObject barrel;
    public float originalShootCooldown = 0.2f;
    public float originalHeatBuildup = 20;
    public float health = 100f;
    public float maxHealth = 100f;
    public int damageTaken = 0;
    int maxDamBeforeMalfunction = 1;
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
    int originalMoveSpeed = 20;
    bool barrelInserted = true;
    bool released = false;
    public GameObject turretSprite;
    bool detectedBarrel = false;
    bool needReload = false;
    bool magRelease = true;
    bool detectedMag = false;
    int startingMag;
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
    int[] malfunctionArray;
    int swungAt=0;
    int swungMax;
    int hits = 5;
    float score=0;
    public GameObject lWingSelect;
    public GameObject rWingSelect;
    public GameObject barrelSelect;
    public GameObject hullSelect;
    bool leftMotionDamage=false;
    bool rightMotionDamage=false;
    public float turnPenalty = 0.5f;
    public GameObject camText;
    public GameObject lWingText;
    public GameObject rWingText;
    public GameObject hullText;
    public GameObject barrelText;
    public Vector3 defaultBarrelColour=new Vector3(1f,1f,1f);
    public Vector3 currentBarrelColour = new Vector3();
    public Vector3 blueBarrelColour = new Vector3(0.2783f, 0.5641f, 1f);
    public Vector3 greenBarrelColour=new Vector3(0.4009f, 1f, 0.4507f);
    public GameObject scoreText;
    public GameObject scoreText2;
    public float scoreMultiplier = 10;
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        print(score);
        int scoreInt = Mathf.FloorToInt(score);
        score += Time.deltaTime * scoreMultiplier;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text="Score: "+scoreInt;
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + scoreInt;
        if (health < 0)
        {
            Destroy(gameObject);
        }
        print(defaultBarrelColour);
        highLight();
        if (Input.GetKeyDown("g"))
        {
            HammerSwing();
        }
        if (Input.GetKeyDown("w")){
            swungAt += 1;
        }
        if (Input.GetKeyDown("s")){
            swungAt -= 1;
                }
        if (swungAt > swungMax)
        {
            swungAt = 0;
        }
        if (swungAt < 0)
        {
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
        if (detectedMag == false)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                startingMag = 1;
                detectedMag = true;
            }
            else if (Input.GetMouseButton(1))
            {
                startingMag = 2;
                detectedMag = true;
            }
        }
        if (health < 0)
        {
            health = 0;
        }
        if (damageTaken >= maxDamBeforeMalfunction/* && centralDamaged == false*/)
        {
            damageTaken = 0;
            randomMalfunction();
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
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            print("default");
            barrel.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Magwell Empty";
            magRelease = true;
        }
        if (magRelease == true)
        {

            if (Input.GetKeyDown("right"))
            {
                print("1ds");
                barrel.GetComponent<SpriteRenderer>().color = new Color(0.2783f, 0.5641f, 1f);
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Plasma Mag Loaded";
                startingMag = 2;
                magRelease = false;
            }
            {
                if (Input.GetKeyDown("left"))
                {
                    print("2ds");
                    barrel.GetComponent<SpriteRenderer>().color = new Color(0.4009f, 1f, 0.4507f);
                    actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = ".50 CAL Mag Loaded";
                    startingMag = 1;
                    magRelease = false;
                }
            }
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
/*        barrel.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x + heat / 200, barrelColour.y - heat / 400, barrelColour.z - heat / 400);*/
        /*statusText.GetComponent<TMPro.TextMeshProUGUI>().color = barrel.GetComponent<SpriteRenderer>().color;*/
        if (heat >= maxHeat)
        {
            heat = maxHeat;
            if (overheated == false)
            {
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

        if (Input.GetKey("space") && overheated == false && magRelease == false)
        {
            if (cooldown <= 0)
            {
                if (startingMag == 1)
                {
                    GameObject bullet = Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                }
                else
                {
                    GameObject bullet = Instantiate(projectile2, barrelEnd.transform.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                }
                cooldown = shootCooldown;
                heat += heatBuildUp;
            }
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
            transform.Rotate(0, 0, rotation * Time.deltaTime * moveSpeed);
        }
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
        print(malfunctionArray[3]);
        if (malfunctionArray[0]<= 0)
        {
            rightMotionDamage = false;
            rWingGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            malfunctionArray[0] = 0;
            rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
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
        }
        else
        {
            lWingMalfunction();
            lWingGUI.GetComponent<SpriteRenderer>().color = new Color(damagedColour.x, damagedColour.y, damagedColour.z);
        }
        if (malfunctionArray[2] == 0)
        {
            malfunctionArray[2] = 0;
            blackout.SetActive(false);
            hullGUI.GetComponent<SpriteRenderer>().color = new Color(barrelColour.x, barrelColour.y, barrelColour.z);
            camText.GetComponent<TMPro.TextMeshProUGUI>().text="Status: Cam Systems Functional";
            hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Systems Normal";
        }
        else{
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
        rWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Right Wing damaged: -50% leftwards rotation speed. Hit "+malfunctionArray[0]+" times";
        rightMotionDamage = true;
    }
    private void lWingMalfunction()
    {
        lWingText.GetComponent<TMPro.TextMeshProUGUI>().text = "Left Wing damaged: -50% rightwards rotation speed. Hit " + malfunctionArray[1] + " times";
        leftMotionDamage = true;
    }
    private void hullMalfunction()
    {
        hullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Cockpit integrity compromised: Turret camera unavailabe. Hit " + malfunctionArray[2] + " times";
        blackout.SetActive(true);
    }
    private void camMalfunction()
    {        
    }
    private void barrelMalfunction()
    {
        barrelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Melted Barrel: Unable to fire. Hit " + malfunctionArray[3] + " times";
    }
    private void switchColours(GameObject obj, Vector3 change)
    {
        print(change.x);
        obj.GetComponent<SpriteRenderer>().color = new Color(change.x,change.y,change.z);
    }
}