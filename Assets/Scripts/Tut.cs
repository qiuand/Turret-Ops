using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class Tut : MonoBehaviour
{
    public AudioClip bang;
    public GameObject standardUI;
    public GameObject healthUI;
    public VideoClip spin, load, swap, hammer, black, colour, activate;
    public GameObject mechVid, gunVid;
    public GameObject gunTutVid;
    public GameObject mechTutVid;

    bool canToggle = true;
    public GameObject spawner;
    public AudioClip ding;
    Vector3 originalPos = new Vector3(-0.51f, 2.27f,0);
    public GameObject mechText;
    public GameObject turretText;
    string[] gunTut=new string[8];
    string[] mechTut= new string[8];
    public GameObject turret;
    int selection = 0;
    public GameObject tutEnemyBlue;
    public GameObject tutEnemyGreen;
    public bool inTut = false;
    bool locked = false;
    bool malfSet=false;
    public AudioSource source;
    public AudioClip malfunction;
    float waitTime = 1.25f;
    bool gunnerAgree = false;
    bool mechanicAgree = false;
    int agreeNum = 0;
    public GameObject agreeText;
    public GameObject agreeText2;
    float waitTimer;
    float waitDuration = 1.0f;
    bool continued = true;
    bool magChanged = true;
    bool soundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        mechVid.SetActive(false);
        mechTutVid.SetActive(false);
        gunTutVid.SetActive(false);

        waitTimer = waitDuration;
        tutEnemyBlue.SetActive(false);
            tutEnemyGreen.SetActive(false);
        /*        gunTut[0] = "Welcome to COSMIC CREW! You and your partner have a very important mission: keep your ship afloat at all costs to defeat the advancing fleet!<br>Fire to continue";*/
        gunTut[0] = "<b>1/4<br>Welcome to COSMIC CREW!</b><br><br>Your mission is to destroy the advancing fleet!<br><br><color=green>Press <color=red>Fire ●</color> to Continue";/*<br><color=#006CFF>2: Maintain the ship</color><br><br><color=green>Press fire to continue";*/
        gunTut[1] = "<b>2/4<br>You're the Gunner!</b> Your partner is the <color=#006CFF>Mechanic.</color><br><br><color=green>Press <color=red>Fire ●</color> to Continue";
        /*gunTut[2] = "You are in charge of shooting down enemies.<br><br><color=green>Press fire to continue";*/
        gunTut[2] = "Rotate and fire to destroy that enemy!</b>";
        /*gunTut[4] = "Malfunctions occur when the ship is hit! This can cause all sorts of problems for your turret. Your ship automatically repairs a bit of damage after some time, but critical malfunctions must be fixed by your friend!";*/
        gunTut[3] = "<b>3/4<br><color=red>Malfunctions</color> are bad!</b> Ask the <color=#006CFF>Mechanic</color> to fix them!<br><br>Waiting for <color=#006CFF>Mechanic...</color>";
        /*        gunTut[5] = "<b>Shooting causes <color=red>overheating!</color></b> Let your <color=#006CFF>friend</color> fix that, too!<br><br>Waiting for <color=#006CFF>Mechanic...</color>";
        */
        gunTut[4] = "<b>Waiting for more repairs...</b>";
        gunTut[5] = "<b>4/4<br></b><color=#1266E6>Blue (○) bullets</color> for <color=#1266E6>blue (○) enemies!</color><br><color=#CC4C26>Orange (☐) bullets</color> for <color=#CC4C26>orange (☐) enemies!<br></color>But the <color=#006CFF>Mechanic</color> can't see colours; Help them out!<br><b>Now destroy that enemy!</b>";
        /*        gunTut[4] = "<color=#1266E6>blue (○) bullets</color> for <color=#1266E6>blue enemies</color>, and <color=#CC4C26>orange (☐) bullets</color> for <color=#CC4C26>orange enemies!</color>Of course, you wouldn't know who's who; ask the <color=yellow>Gunner</color> which to load!";*/
        gunTut[6] = "Good Luck, Cosmic Gunner!<br><br><color=green><br>Press <color=red>Fire ●</color> to Continue";
        gunTut[7] = "";

        mechTut[0] = "<b>1/4<br>Welcome to COSMIC CREW!</b><br><br>Your mission is to destroy the advancing fleet!<br><br><color=green>Press <color=red>Select ●</color> to Continue";
        mechTut[1]= "<b>2/4<br>You're the Mechanic!</b> Your partner is the <color=yellow>Gunner.</color><br><br><color=green>Press <color=red>Select ●</color> to Continue";
/*        mechTut[2] = "You are in charge of maintaining the ship.<br><br><color=green>Press select to continue";
*/        mechTut[2] = "<b>3/4</b><br>Note that your view is in black and white.<br>Waiting for <color=yellow>Gunner...</color>";
        mechTut[3] = "<b><color=red>Getting hit is bad!</color></b> Repair damage and apply upgrades with your hammer!";
        mechTut[4] = "<b>Your ship comes with a Repair Kit!</b><br>Press <color=red>Select ●</color> to repair all malfunctions instantly, and restore a bit of health. Use it wisely, though - it has a long cooldown!";
        /*        mechTut[5] = "Reckless shooting can cause overheating. Repair that, too!";
        */
        mechTut[5] = "<b>4/4<br></b><color=#1266E6>Blue (○) bullets</color> for <color=#1266E6>blue (○) enemies</color>, and <color=#CC4C26>orange (☐) bullets</color> for <color=#CC4C26>orange (☐) enemies!</color> Of course, you can't see colours!<br><b>ask the <color=yellow>Gunner</color> which to load!</b>";
        mechTut[6]= "<b>Good luck, Cosmic Mechanic!<b><br><br><color=green>Press <color=red>Select ●</color> to Continue";
        mechTut[7] = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.inTutorial != false)
        {
            healthUI.SetActive(false);
            standardUI.SetActive(false);
        agreeText.GetComponent<TMPro.TextMeshProUGUI>().text= "Waiting for players: "+agreeNum+" / "+2;
        agreeText2.GetComponent<TMPro.TextMeshProUGUI>().text = agreeText.GetComponent<TMPro.TextMeshProUGUI>().text;

        if (mechanicAgree && gunnerAgree)
        {
            agreeNum=2;
        }
        if (mechanicAgree || gunnerAgree)
        {
            agreeNum=1;
        }
        inTut = gun.inTutorial;
            if (inTut == true)
            {
                if (Input.GetKeyDown("space") && selection < 2)
                {
                    if (gunnerAgree == false)
                    {
                        source.PlayOneShot(ding);
                        gunnerAgree = true;
                    }
                }
                if (Input.GetKeyDown("g") && selection < 2)
                {
                    if (mechanicAgree == false)
                    {
                        source.PlayOneShot(ding);
                        mechanicAgree = true;
                    }
                }
                if (gunnerAgree == true && mechanicAgree == true && selection < 3/*gunTut.Length - 2*/)
                {
                    StartCoroutine(Wait());
                    /*                selection += 1;*/
                    gunnerAgree = false;
                    mechanicAgree = false;
                }

                if (selection == 2)
                {
                    agreeText.SetActive(false);
                    agreeText2.SetActive(false);
                    if (turret.GetComponent<Turret>().startingMag == 0)
                    {
                        magChanged = false;
                        turretText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>The gun is empty!</color><br>Ask the <color=#006CFF>Mechanic</color> to slap one in!";
                        mechText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>The gun is empty!</color><br>Slap one in to let the <color=yellow>Gunner</color> to destroy that enemy!";
                        gunTutVid.SetActive(true);
                        gunTutVid.GetComponent<VideoPlayer>().clip = swap;
                    }
                    if (malfSet == false && turret.GetComponent<Turret>().startingMag != 0)
                    {
                        malfSet = true;
                        if (turret.GetComponent<Turret>().startingMag == 1)
                        {
                            gunTutVid.GetComponent<VideoPlayer>().clip = spin;
                            mechTutVid.GetComponent<VideoPlayer>().clip = black;
                            magChanged = true;
                            tutEnemyBlue.SetActive(false);
                            tutEnemyGreen.SetActive(true);
                        }
                        else
                        {
                            gunTutVid.GetComponent<VideoPlayer>().clip = spin;
                            mechTutVid.GetComponent<VideoPlayer>().clip = black;
                            magChanged = true;
                            tutEnemyGreen.SetActive(false);
                            tutEnemyBlue.SetActive(true);
                        }
                    }

                    /*            if (tutEnemyGreen.activeSelf==false && turret.GetComponent<Turret>().startingMag==1)
                                {
                                    proceed();
                                }*/
                    if (tutEnemyBlue.activeSelf == false && tutEnemyGreen.activeSelf == false && continued==true && magChanged)
                    {
                        continued = false;
                        source.PlayOneShot(ding);
                        StartCoroutine(Wait());
                    }
                }
                if (selection == 3)
                {
                    gunTutVid.SetActive(true);
                    gunTutVid.GetComponent<VideoPlayer>().clip = hammer;
                    if (malfSet == false)
                    {
                        turret.GetComponent<Turret>().malfunctionArray[0] = turret.GetComponent<Turret>().hits/*turret.GetComponent<Turret>().hits*/;
                        malfSet = true;
                        source.PlayOneShot(malfunction);
                    }
                    if (turret.GetComponent<Turret>().malfunctionArray[0] == 0 && continued)
                    {
                        continued = false;
                        source.PlayOneShot(ding);
                        StartCoroutine(Wait());
                    }
                }
                if (selection == 4)
                {
                    if (malfSet == false)
                    {
                        malfSet = true;
                        source.PlayOneShot(bang);
                        source.PlayOneShot(bang);
                        for (int i = 0; i < turret.GetComponent<Turret>().malfunctionArray.Length; i++)
                        {
                            source.PlayOneShot(malfunction);
                            turret.GetComponent<Turret>().malfunctionArray[i] = 9999999;
                            turret.GetComponent<Turret>().heat = turret.GetComponent<Turret>().maxHeat;
/*                            turret.GetComponent<Turret>().barrelMalfunction();*/
                        }
                    }
                    if (Input.GetKeyDown("g"))
                    {
                        turret.GetComponent<Turret>().heat = 0;
                        turret.GetComponent<Turret>().ActivatePowerups(true);
                        source.PlayOneShot(ding);
                        StartCoroutine(Wait());
                    }
                }
                if (selection == 15)
                {
                    if (malfSet == false)
                    {
                        turret.GetComponent<Turret>().malfunctionArray[3] = turret.GetComponent<Turret>().hits;
                        turret.GetComponent<Turret>().heat = turret.GetComponent<Turret>().maxHeat * 1000;
                        malfSet = true;
                    }
                    if (turret.GetComponent<Turret>().malfunctionArray[3] == 0 && continued)
                    {
                        continued = false;
                        source.PlayOneShot(ding);
                        StartCoroutine(Wait());
                    }
                }
                if (selection == 5)
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
                    if (tutEnemyBlue.activeSelf == false && tutEnemyGreen.activeSelf == false && continued)
                    {
                        continued = false;
                        source.PlayOneShot(ding);
                        StartCoroutine(Wait());
                    }
                }
                if (selection == 6)
                {
                    agreeText.SetActive(true);
                    agreeText2.SetActive(true);
                    if (Input.GetKeyDown("space") && gunnerAgree==false)
                    {
                        source.PlayOneShot(ding);
                        gunnerAgree = true;
                    }
                    if (Input.GetKeyDown("g") && mechanicAgree == false)
                    {
                        source.PlayOneShot(ding);
                        mechanicAgree = true;
                    }
                    if (/*Input.GetKeyDown("g")*/mechanicAgree && gunnerAgree && continued)
                    {
                        continued = false;
                        StartCoroutine(Wait());
                        gun.inTutorial = false;
                        standardUI.SetActive(true); ;
                        healthUI.SetActive(true);

                    }
                }
                SwitchText(turretText, gunTut);
                SwitchText(mechText, mechTut);
                EnemySpawn.beginNextWave = true;
                spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
            }
        }
    }
    private void SwitchText(GameObject thing, string[] array)
    {
        if (magChanged == true)
        {
            thing.GetComponent<TMPro.TextMeshProUGUI>().text = array[selection];
        }
    }
    private void spawnTutEnemy()
    {

    }
    private void proceed()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        if (canToggle == true)
        {
            canToggle = false;
            agreeNum = 2;
            agreeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 1, 0);
            agreeText2.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 1, 0);
            yield return new WaitForSeconds(waitTime);
            selection++;
            malfSet = false;
            continued = true;
            agreeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            agreeText2.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            agreeNum = 0;
            canToggle = true;
            SetVids();
        }
    }
    private void setActiveAgain(GameObject shipTut)
    {
        shipTut.GetComponent<EnemyTut>().fire.enableEmission = true;
        shipTut.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    public void SetVids()
    {
        switch (selection)
        {
            case 2:
                mechVid.SetActive(true);
                mechTutVid.SetActive(true);
                gunTutVid.SetActive(true);
                gunTutVid.GetComponent<VideoPlayer>().clip = spin;
                mechTutVid.GetComponent<VideoPlayer>().clip = swap;
                break;
            case 3:
/*                gunVid.SetActive(false);*/
                gunTutVid.SetActive(false);
                mechTutVid.GetComponent<VideoPlayer>().clip = hammer;
                break;
            case 4:
                /*                gunVid.SetActive(false);*/
                mechTutVid.GetComponent<VideoPlayer>().clip = activate;
                break;
            case 5:
                gunVid.SetActive(true);
                gunTutVid.SetActive(true);
                gunTutVid.GetComponent<VideoPlayer>().clip = black;
                mechTutVid.GetComponent<VideoPlayer>().clip = colour;
                break;
            case 6:
                gunVid.SetActive(false);
                mechVid.SetActive(false);
                break;
        }
    }
}
