using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tut : MonoBehaviour
{
    Vector3 originalPos = new Vector3(-0.6f, 1.94f, -1.61f);
    public GameObject mechText;
    public GameObject turretText;
    string[] gunTut=new string[9];
    string[] mechTut= new string[9];
    public GameObject turret;
    int selection = 0;
    public GameObject tutEnemyBlue;
    public GameObject tutEnemyGreen;
    public bool inTut = false;
    bool locked = false;
    bool malfSet=false;
    public AudioSource source;
    public AudioClip malfunction;
    float waitTime = 1f;
    bool gunnerAgree = false;
    bool mechanicAgree = false;
    // Start is called before the first frame update
    void Start()
    {
        tutEnemyBlue.SetActive(false);
        tutEnemyGreen.SetActive(false);
        /*        gunTut[0] = "Welcome to COSMIC CREW! You and your partner have a very important mission: keep your ship afloat at all costs to defeat the advancing fleet!<br>Fire to continue";*/
        gunTut[0] = "<b>Welcome to COSMIC CREW!<b><br>Your mission is simple:<br>1: Destroy the advancing fleet<br>2: Maintain the ship";
        gunTut[1] = "You're the Gunner! Your partner is the <color=#006CFF>Mechanic.</color><br>Fire to continue";
        gunTut[2] = "You are in charge of shooting down enemies.<br>Fire to continue";
        gunTut[3] = "Lever: Rotate turret<br>Fire: Shoot<br>Destroy that enemy!";
        /*gunTut[4] = "Malfunctions occur when the ship is hit! This can cause all sorts of problems for your turret. Your ship automatically repairs a bit of damage after some time, but critical malfunctions must be fixed by your friend!";*/
        gunTut[4] = "Malfunctions are bad! Ask the mechanic to fix them!";
        gunTut[5] = "Shooting causes overheating! Let your friend fix that, too!";
        /*        gunTut[6] = "You can only damage green enemies with green bullets, and blue enemies with blue bullets! The mechanic can't tell what colour enemies are, so tell him which ammo to load!";*/
        gunTut[6] = "<color=green>Green bullets</color> for <color=green>green enemies</color>, and <color=red>red bullets</color> for <color=red>red enemies!</color> You need to tell the mechanic which you need — they can't see colors!";
        gunTut[7] = "That's all you need to know! Good luck, Cosmic Gunner!";
        gunTut[8] = "";

        mechTut[0] = "<b>Welcome to COSMIC CREW!</b><br>Your mission is simple:<br>1: Destroy the advancing fleet<br>2: Maintain the ship";
        mechTut[1]= "You're the Mechanic! Your partner is the <color=yellow>Gunner.</color>";
        mechTut[2] = "You are in charge of maintaining the ship.";
        mechTut[3] = "Note that your gunport cannot see colours.";
        mechTut[4] = "Getting hit is bad! Repair the damage with your hammer now!";
        mechTut[5] = "Reckless shooting can cause overheating. Repair that, too!";
        mechTut[6] = "<color=green>Green bullets</color> for <color=green>green enemies</color>, and <color=red>red bullets</color> for <color=red>red enemies!</color>Of course, you wouldn't know who's who; ask the Gunner which to load!";
        mechTut[7]= "Good luck, Cosmic Mechanic!<b> Use the hammer to continue";
        mechTut[8] = "";
    }

    // Update is called once per frame
    void Update()
    {
        inTut = gun.inTutorial;
        if (inTut == true)
        {
            if (Input.GetKeyDown("space") && selection < 3)
            {
                gunnerAgree = true;
            }
            if (Input.GetKeyDown("g") && selection < 3)
            {
                mechanicAgree = true;
            }
            if (gunnerAgree==true && mechanicAgree==true && selection < gunTut.Length - 2)
            {
                selection += 1;
                gunnerAgree = false;
                mechanicAgree = false;
            }

            if (selection == 3)
            {
                if (malfSet == false)
                {
                    malfSet = true;
                    if (turret.GetComponent<Turret>().startingMag == 1)
                    {
                        tutEnemyBlue.SetActive(false);
                        tutEnemyGreen.SetActive(true);
                    }
                    else
                    {
                        tutEnemyGreen.SetActive(false);
                        tutEnemyBlue.SetActive(true);
                    }
                }

                /*            if (tutEnemyGreen.activeSelf==false && turret.GetComponent<Turret>().startingMag==1)
                            {
                                print("yessir");
                                proceed();
                            }*/
                if (tutEnemyBlue.activeSelf == false && tutEnemyGreen.activeSelf == false)
                {
                    malfSet = false;
                    proceed();
                }


                print("thing");
            }
            if (selection == 4)
            {
                if (malfSet == false)
                {
                    turret.GetComponent<Turret>().malfunctionArray[0] = turret.GetComponent<Turret>().hits;
                    malfSet = true;
                    source.PlayOneShot(malfunction);
                }
                if (turret.GetComponent<Turret>().malfunctionArray[0] == 0)
                {
                    malfSet = false;
                    proceed();
                }

            }
            if (selection == 5)
            {
                if (malfSet == false)
                {
                    turret.GetComponent<Turret>().malfunctionArray[3] = turret.GetComponent<Turret>().hits;
                    turret.GetComponent<Turret>().heat = turret.GetComponent<Turret>().maxHeat * 1000;
                    malfSet = true;
                }
                if (turret.GetComponent<Turret>().malfunctionArray[3] == 0)
                {
                    malfSet = false;
                    proceed();
                }
            }
            if (selection == 6)
            {
                if (malfSet == false)
                {
                    tutEnemyGreen.transform.position = originalPos;
                    tutEnemyGreen.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    tutEnemyBlue.transform.position = originalPos;
                    tutEnemyBlue.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    tutEnemyBlue.GetComponent<Animator>().SetBool("Destroyed", false);
                    tutEnemyGreen.GetComponent<Animator>().SetBool("Destroyed", false);
                    tutEnemyGreen.GetComponent<Rigidbody2D>().isKinematic = true;
                    tutEnemyBlue.GetComponent<Rigidbody2D>().isKinematic = true;
                    malfSet = true;
                    if (turret.GetComponent<Turret>().startingMag == 1)
                    {
                        tutEnemyGreen.SetActive(false);
                        tutEnemyBlue.SetActive(true);
                    }
                    else
                    {
                        tutEnemyBlue.SetActive(false);
                        tutEnemyGreen.SetActive(true);
                    }
                }
                if (tutEnemyBlue.activeSelf == false && tutEnemyGreen.activeSelf == false)
                {
                    malfSet = false;
                    proceed();
                }
            }
            if (selection == 7)
            {
                if (Input.GetKeyDown("g"))
                {
                    proceed();
                    gun.inTutorial = false;
                }
            }
            SwitchText(turretText, gunTut);
            SwitchText(mechText, mechTut);
            EnemySpawn.beginNextWave = true;
        }
    }
    private void SwitchText(GameObject thing, string[] array)
    {
        thing.GetComponent<TMPro.TextMeshProUGUI>().text = array[selection];
    }
    private void spawnTutEnemy()
    {

    }
    private void proceed()
    {
        selection++;
/*        StartCoroutine(Wait());*/
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        selection++;
    }
}
