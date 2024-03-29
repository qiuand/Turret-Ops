using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Turret : MonoBehaviour
{
    public GameObject statusText;
    public Image overheatBar;
    public Image healthBar;
    Vector3 barrelColour = new Vector3(0.6033731f, 0.8584906f, 0.8451912f);
    Vector3 Damagedcolour = new Vector3(0.8679245f, 0.4380563f, 0.4554211f);
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
    float cooldown=0f;
    bool canRepair = true;
    float repairAmountPerSwing = 20;
    bool overheated = false;
    float repairCooldown = 1f;
    float repairTimer = 0;
    public GameObject barrel;
    public float originalShootCooldown = 0.2f;
    public float originalHeatBuildup = 20;
    public float health=100f;
    public float maxHealth=100f;
    public int damageTaken = 0;
    int maxDamBeforeMalfunction=1;
    bool malfunctioning = false;
    string malfunctionType="None";
    string[] malfunctionList = new string[] { "Cockpit", "Left wing", "Right wing", "Hull"};
    public GameObject hullGUI;
    public GameObject rWingGUI;
    public GameObject lWingGUI;
    public GameObject cameraGUI;
    public GameObject barrelGUI;
    string inputDisplay="";
    string codeDisplay="None";
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
    bool detectedBarrel=false;
    bool needReload = false;
    bool magRelease = false;
    bool detectedMag=false;
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
    float damageTick=3f;
    float damageTimer=0f;
    public GameObject barrelIcon;
    public int healthDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        blackout.SetActive(false);
        heatBuildUp = originalHeatBuildup;
        shootCooldown = originalShootCooldown;
        moveSpeed = originalMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (cameraDamage == true)
        {
            blackout.SetActive(true);
            moveSpeed = reducedMoveSpeed;
           
        }
        else
        {
            blackout.SetActive(false);
            moveSpeed = originalMoveSpeed;
        }
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
            if (Input.GetAxis("Mouse X")>0)
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

        if (damageTaken >= maxDamBeforeMalfunction && centralDamaged==false)
        {
            damageTaken = 0;
            malfunctioning = true;
            malfunctionType = malfunctionList[Random.Range(0, malfunctionList.Length)];
            RunMalfunctions(malfunctionType, Damagedcolour);
        }
        if(malfunctioning && hullDamage==true)
            {
            if (damageTimer <= 0)
            {
                health-=healthDamage*2;
                damageTimer = damageTick;
            }
            mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Critical Hull Damage!";
                if (Input.GetKeyDown("g") && canRepair)
                {
                    hullHits++;
                    health+=repairAmountPerSwing;
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
                if (hullHits>=hullHitsReq)
                {
                    repairTimer = 0;
                    malfunctioning = false;
                hullDamage = false;
                mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hull Restored";
                RunMalfunctions(malfunctionType, barrelColour);
                centralDamaged = false;
                }
            }
        if (malfunctioning==true && barrelHeated==true)
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
                        malfunctioning = false;
                        overheated = false;
                        heat = 0;
                        startingBarrel = 2;
                        RunMalfunctions(malfunctionType, barrelColour);
                        released = false;
                        barrelHeated=false;
                        turretSprite.GetComponent<SpriteRenderer>().enabled = true;
                        barrelIcon.GetComponent<SpriteRenderer>().enabled = true;
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
                        return;
                    }
                }
            }

        }
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Magwell empty";
            magRelease = true;
        }
        if (magRelease == true)
        {

                if (Input.GetKeyDown("right"))
                {
                actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = "Plasma mag loaded";
                startingMag = 2;
                    magRelease = false;
                }
            {
                if (Input.GetKeyDown("left"))
                {
                    actionStatus.GetComponent<TMPro.TextMeshProUGUI>().text = ".50 CAL mag loaded";
                    startingMag = 1;
                    magRelease = false;
                }
            }
        }
        if (malfunctioning && malfunctionType!="Barrel" && malfunctionType != "Hull")
        {
            if (damageTimer <= 0)
            {
                health-=healthDamage;
                damageTimer = damageTick;
            }
            damageTimer -= Time.deltaTime;
            mTypeText.GetComponent<TMPro.TextMeshProUGUI>().text = malfunctionType;
            pCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = inputDisplay;
            rCodeText.GetComponent<TMPro.TextMeshProUGUI>().text = requiredCode[0] + requiredCode[1] + requiredCode[2] + requiredCode[3];
            if ((Input.GetKeyDown("a")|| Input.GetKeyDown("s")|| Input.GetKeyDown("d")|| Input.GetKeyDown("w")) && playerInput.Count<maxInput)
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
                for(int i=0; i<maxInput; i++)
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
                if(correctNo==false)
                        {
                            damagePlayer();
                            playerInput.Clear();
                            inputDisplay = "";
                        }
                if(correctNo)
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
                }
            }
        }
        overheatBar.fillAmount = heat / maxHeat;
        healthBar.fillAmount = health / maxHealth;
        barrel.GetComponent<SpriteRenderer>().color=new Color(barrelColour.x+heat/200, barrelColour.y-heat/400,barrelColour.z - heat / 400);
        statusText.GetComponent<TMPro.TextMeshProUGUI>().color = barrel.GetComponent<SpriteRenderer>().color;
        if (heat >= maxHeat)
        {
            heat = maxHeat;
            overheated = true;
            barrelHeated = true;
            malfunctionType = "Barrel";
            malfunctioning = true;
            RunMalfunctions(malfunctionType, Damagedcolour);
        }

        if (Input.GetKey("space") && overheated == false &&  magRelease==false)
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
        else if(overheated==false)
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
        if (overheated)
        {
            statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Critical Overheat � Repairs Needed!";
           
        }
        else
        {
            statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Normal";
        }

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
        transform.Rotate(0,0, rotation * Time.deltaTime*moveSpeed);
    }

    private void RunMalfunctions(string malfunctionType, Vector3 colour)
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
    }
    private void Camera()
    {
        blackout.SetActive(true);
    }
    private void damagePlayer()
    {
        health--;
    }
}
