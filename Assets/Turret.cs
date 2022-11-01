using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public GameObject statusText;
    public Image overheatBar;
    public Image healthBar;
    Vector3 barrelColour = new Vector3(0.6033731f, 0.8584906f, 0.8451912f);
    public float heat;
    float heatBuildUp = 700;
    public float maxHeat = 100;
    float heatCoolDown = 100;
    public GameObject barrelEnd;
    float moveSpeed = 20; 
    float rotation;
    public GameObject projectile;
    int projectileSpeed = 30;
    float shootCooldown = 0.5f;
    float cooldown=0f;
    bool canRepair;
    float repairAmountPerSwing = 20;
    bool overheated = false;
    float repairCooldown = 1f;
    float repairTimer = 0;
    public GameObject barrel;
    public float health=1000f;
    public float maxHealth=1000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        overheatBar.fillAmount = heat / maxHeat;
        healthBar.fillAmount = health / maxHealth;
        barrel.GetComponent<SpriteRenderer>().color=new Color(barrelColour.x+heat/200, barrelColour.y-heat/400,barrelColour.z - heat / 400);
        statusText.GetComponent<TMPro.TextMeshProUGUI>().color = barrel.GetComponent<SpriteRenderer>().color;
        if (heat >= maxHeat)
        {
            heat = maxHeat;
            overheated = true;
        }
        if (Input.GetKey("space") && overheated == false)
        {
            health -= 1;
            if (cooldown <= 0)
            {
                GameObject bullet= Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
                cooldown = shootCooldown;
                heat += Time.deltaTime*heatBuildUp;
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
            statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Critical Overheat — Repairs Needed!";
            if (Input.GetKeyDown("r") && canRepair)
            {
                heat -= repairAmountPerSwing;
                canRepair = false;
                repairTimer = repairCooldown;
            }
            if (repairTimer>0)
            {
                repairTimer -= Time.deltaTime;
            }
            if (repairTimer <= 0)
            {
                canRepair = true;
            }
            if (heat <= 0)
            {
                repairTimer = 0;
                overheated = false;
            }
        }
        else
        {
            statusText.GetComponent<TMPro.TextMeshProUGUI>().text = "Status: Normal";
        }
        cooldown -= Time.deltaTime;
        rotation = Input.GetAxis("Vertical");
        transform.Rotate(0,0, rotation * Time.deltaTime*moveSpeed);
    }
}
