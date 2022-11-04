using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tut : MonoBehaviour
{
    Vector3 originalPos = new Vector3(5.98f, 0.75f, -3.59f);
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
    // Start is called before the first frame update
    void Start()
    {
        tutEnemyBlue.SetActive(false);
        tutEnemyGreen.SetActive(false);
        gunTut[0] = "Welcome to COSMIC CREW! You and your partner have a very important mission: keep your ship afloat at all costs to defeat the advancing fleet!<br>Fire to continue";
        gunTut[1] = "You're the Gunner! Your partner is the Mechanic.<br>Fire to continue";
        gunTut[2] = "You are in charge of shooting down enemies.<br>Fire to continue";
        gunTut[3] = "Lever: Rotate turret<br>Fire: Shoot<br>Destroy that enemy!";
        gunTut[4] = "Malfunctions occur when the ship is hit! This can cause all sorts of problems for your turret. Your ship automatically repairs a bit of damage after some time, but critical malfunctions must be fixed by your friend!";
        gunTut[5] = "Shooting too much cuases critical overheats! Let your friend the Mechanic fix those, too!";
        gunTut[6] = "You can only damage green enemies with green bullets, and blue enemies with blue bullets! The mechanic can't tell what colour enemies are, so tell him which ammo to load!";
        gunTut[7] = "That's all you need to know to be a certified space gunner! Good luck, Cosmic Crew!";
        gunTut[8] = "";

        mechTut[0]= "Welcome to COSMIC CREW! You and your partner have a very important mission: keep your ship afloat at all costs to defeat the advancing fleet!"; ;
        mechTut[1]= "You're the Mechanic! Your partner is the Gunner.";
        mechTut[2] = "You are in charge of maintaining the ship.";
        mechTut[3] = "Your mini map gives you a view of what the Gunner is up to.";
        mechTut[4] = "Malfunctions occur when the ship is hit! Your ship automatically repairs a bit of damage after some time, but the critical malfunctions can cause all sorts of problems for your friend's turret. Use the button on the left to select the damaged part, and hit the repair pad with the hammer to fix it!";
        mechTut[5] = "Reckless shooting can cause barrel overheating. Repair the barrel now!";
        mechTut[6] = "The gunner needs blue ammo to damage blue ships, and green ammo for green ships. You can't see enemy colours, so listen closely to what ammo they need!";
        mechTut[7]= "Good luck, Cosmic Mechanic!!<b> Use the hammer to continue";
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
                print("af");
                if (selection < gunTut.Length - 2)
                {
                    selection += 1;
                }
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
                    tutEnemyGreen.transform.position = new Vector3(5.98f, 0.75f, -3.59f);
                    tutEnemyGreen.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    tutEnemyBlue.transform.position = new Vector3(5.98f, 0.75f, -3.59f);
                    tutEnemyBlue.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
    }
}
