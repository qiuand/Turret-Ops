﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class Upgrades : MonoBehaviour
{

    float longDuration, medDuration, shortDuration;
    float longCooldown, medCooldown, shortCooldown;

    public GameObject uppy1Src, uppy2Src;
    public GameObject gunPlayer1, gunPlayer2, mechPlayer1, mechPlayer2;
    public VideoClip dualVid, triVid, laserVid, ricoVid, partiVid, railVid, chainVid;
    public VideoClip thermalVid, overVid, orangeVid, blueVid, heatVid, airVid;

    public GameObject waveFlash;
    public GameObject waveFlash2;

    public VideoClip black;

    public GameObject gun;
    public GameObject body;

    float cooldownTime;
    float activationTime;

    public GameObject mechGun;
    public GameObject mechBody;

    public GameObject upgradeLayer2;

    public Sprite dual;
    public Sprite triple;
    public Sprite fast;
    public Sprite railgun;
    public Sprite ricochet;
    public Sprite chaingun;
    public Sprite laserSprite;
    public Sprite particleSprite;
    public Sprite overgunSprite;
    public GameObject minimapGun;
    public GameObject minimapHull;
    public Sprite small, repair, redPower, greenPower, pierce, Thermals, enhanced, heavy, airstrike, tactialOverride;
    public AudioClip select;
    public AudioClip bing;
    public GameObject hullThing;
    public AudioClip donk;
    public GameObject tankAnimate;
    public GameObject abortText2;
    public GameObject upgradeLayer;
    AudioSource source;
    public AudioClip ding;
    public static bool canUpgrade=false;
    List<string> upgradeList = new List<string> { "Improved Bearings", "Dual shot", "Chain Gun", "Dual shot", "Shotgun", "Ricochet Shot", "Laser", "Particle Smasher"/*, "Reactive Armour"*/, /*"Overcharge",*/ "Railgun Overcharge" };
    List<string> powerupList = new List<string> { /*"Small Frame",*/ "Repair", "Orange Shield", "Blue Shield", "Blue Shield", "Heat Rounds",  "Thermal Imaging", "Enhanced Materials", "Heavy Armour"/*, "Electric Override"*/,  "Tactical Airstrike", };
    public GameObject upgrade1;
    public GameObject upgrade2;
    int displayChoice;
    int displayChoice2;
    bool upgradeChosen = false;
    string chosenUpgrade;
    public GameObject ship;
    public static bool upgradesRolled = true;
    int upgradeIndex;
    public GameObject spawner;
    float abortTimer;
    float abortDuration = 10.0f;
    public GameObject ticking;
    public GameObject abortText;
    public bool pendingUpgrade;
    int upgradeNumSelected;
    public GameObject mechanicScreenUppy1;
    public GameObject mechanicScreenUppy2;
    public GameObject mechanicScreenUppyLayer;
    public Camera cam;
    public bool installing = false;
    public GameObject turretSprite;
    float upgradeTimeShow;
    float upgradeTimeDuration=1.0f;
    public string replacedThingGun = "";
    public string replacedThingBody = "";
    // Start is called before the first frame update
    public GameObject turret;

    void Start()
    {
        longDuration = 20f;
        medDuration = 10f;
        shortDuration = 5f;

        longCooldown = 45f;
        medCooldown = 30f;
        shortCooldown = 15f;

        cooldownTime = turret.GetComponent<Turret>().rechargeDuration;
        activationTime = turret.GetComponent<Turret>().pierceDurationCool;
        upgradeTimeShow = upgradeTimeDuration;
        abortTimer = abortDuration;
        mechanicScreenUppyLayer.SetActive(false);
        upgradeLayer.SetActive(false);
        source = GetComponent<AudioSource>();
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawn.waveCount == spawner.GetComponent<EnemySpawn>().bossWave)
        {
            waveFlash.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Wave!";
        }
        else
        {
            waveFlash.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + EnemySpawn.waveCount + "!";
        }
        waveFlash2.GetComponent<TMPro.TextMeshProUGUI>().text = waveFlash.GetComponent<TMPro.TextMeshProUGUI>().text;
        print("Upgrade: "+displayChoice + " " + upgradeList.Count);
        print("Powerup:"+displayChoice2+" "+powerupList.Count);
/*        upgradeTimeShow -= Time.deltaTime;
        if (upgradeTimeShow <= 0)
        {
            upgradeIndex = Random.Range(0, 9);
            chosenUpgrade = upgradeList[Random.Range(2, 3)];
            InstallPowerups();
            upgradeIndex = Random.Range(0, 9);
            chosenUpgrade = powerupList[Random.Range(2, 3)];
            InstallUpgrades();
            upgradeTimeShow = upgradeTimeDuration;
        }*/
        if (Turret.scoreToUpgrade >= ship.GetComponent<Turret>().scoreToUpgradeRequired)
        {
            if (canUpgrade)
            {
                if (Input.GetKeyDown("2") && pendingUpgrade == false)
                {
                    source.PlayOneShot(select);
                    upgradeNumSelected = 2;
                    chosenUpgrade = upgradeList[displayChoice2];
                    /*                upgradeChosen = true;*/
                    upgradeIndex = displayChoice2;
                    /*                InstallPowerups();*/
                    /*                canUpgrade = false;*/
                    pendingUpgrade = true;
                }
                if (Input.GetKeyDown("1") && pendingUpgrade == false)
                {
                    source.PlayOneShot(select);
                    upgradeNumSelected = 1;
                    chosenUpgrade = upgradeList[displayChoice];
                    upgradeIndex = displayChoice;
                    pendingUpgrade = true;

                }
                if (Input.GetKeyDown("3") && pendingUpgrade==false)
                {
                    print("yeya");
                    source.PlayOneShot(select);
                    upgradeNumSelected = 15;

                    pendingUpgrade = true;

                }
                if (pendingUpgrade)
                {
                    if (abortTimer > 0 && pendingUpgrade)
                    {
                        ticking.SetActive(true);
                        if (installing == false)
                        {
/*                            abortTimer -= Time.deltaTime;*/
                        }
                        abortText.GetComponent<TMPro.TextMeshProUGUI>().text = "Waiting for Mechanic approval.<br>auto-abort in " + System.Math.Round(abortTimer, 1) + " seconds";
                        if (upgradeNumSelected==1)
                        {
                            abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Gunner chose " + upgradeList[displayChoice] + ".<br>Press <color=red>● Select</color> to approve<br>auto-abort in " + System.Math.Round(abortTimer, 1) + " seconds";
                            abortTimer -= Time.deltaTime;
/*                            abortTimer -= Time.deltaTime;
*/                        }
                        else if (upgradeNumSelected == 2)
                        {
                            abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Gunner chose " + powerupList[displayChoice2] + ".<br>Press <color=red>● Select</color> to approve<br>auto-abort in " + System.Math.Round(abortTimer, 1) + " seconds";
                            abortTimer -= Time.deltaTime;
                        }
                        else
                        {
                            print("yosh");
                            abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Gunner chose nothing. Press <color=red>● Select</color> to approve<br>auto-abort in " + System.Math.Round(abortTimer, 1) + " seconds";
                            abortTimer -= Time.deltaTime;
                        }

                        if (Abort())
                        {
                            ticking.SetActive(false);
                            abortText.GetComponent<TMPro.TextMeshProUGUI>().text = "Success! Waiting for installation...";
                            abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Success! Hit plates to install upgrades.";
                            upgradeChosen = true;
                            if (upgradeNumSelected == 1)
                            {
                                source.PlayOneShot(select);
                                canUpgrade = false;
                                installing = true;
                                ship.GetComponent<Turret>().malfunctionArray[3] = ship.GetComponent<Turret>().uppieHits;
/*                                InstallUpgrades();*/
                                pendingUpgrade = false;
                            }
                            else if (upgradeNumSelected == 2)
                            {
                                source.PlayOneShot(select);
                                installing = true;
                                ship.GetComponent<Turret>().malfunctionArray[2] = ship.GetComponent<Turret>().uppieHits;
                                pendingUpgrade = false;
/*                                if (ship.GetComponent<Turret>().malfunctionArray[3] <= 0)
                                {
                                    InstallPowerups();
                                    pendingUpgrade = false;
                                }*/
                            }
                            else
                            {
                                source.PlayOneShot(select);
                                ticking.SetActive(false);
                                Skip();
                                pendingUpgrade = false;
                            }
                        }
                        else { }
                    }
                    if (abortTimer <= 0)
                    {
                        ticking.SetActive(false);
                        abortTimer = abortDuration;
                        pendingUpgrade = false;
                        abortText.GetComponent<TMPro.TextMeshProUGUI>().text = "Upgrade failed. Please choose again!";
                        abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Upgrade failed. Please choose again!";
                        source.PlayOneShot(donk);
                    }
                }
/*                if (Input.GetKeyDown("3"))
                {
                    upgradeNumSelected = 3;
                    pendingUpgrade = true;
                    *//*                Skip();*//*
                }*/
            }
        }
        if (installing == true)
        {
            if (upgradeNumSelected == 1)
            {
                if (ship.GetComponent<Turret>().malfunctionArray[3] <= 0)
                {
                    InstallUpgrades();
                    pendingUpgrade = false;
                    source.PlayOneShot(bing);
                }
            }
            else
            {
                if (ship.GetComponent<Turret>().malfunctionArray[2] <= 0)
                {
                    InstallPowerups();
                    pendingUpgrade = false;
                    source.PlayOneShot(bing);
                }
            }
        }
    }
    public void Skip()
    {
        turret.GetComponent<Turret>().rechargeTime = 0;
        powerupList.RemoveAt(displayChoice2);
        upgradeList.RemoveAt(displayChoice);
        cam.GetComponent<CamZoom>().zoomIn=false;
        abortTimer = abortDuration;
        upgradeLayer.SetActive(false);
        upgradeLayer2.SetActive(false);
        source.PlayOneShot(ding);
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        EnemySpawn.waveCount++;
        tankAnimate.GetComponent<Animator>().Play("UpgradeReverse");
        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        if (EnemySpawn.waveCount == EnemySpawn.maxWave)
        {
            spawner.GetComponent<EnemySpawn>().waveTime = EnemySpawn.bossTime;
        }
        else
        {
            spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        }
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
        spawner.GetComponent<EnemySpawn>().healthUI.SetActive(true);
        waveFlash.GetComponent<Animator>().Play("WaveFlash");
        waveFlash2.GetComponent<Animator>().Play("WaveFlash");
    }
    public void RollUpgrades()
    {
        abortText.GetComponent<TMPro.TextMeshProUGUI>().text = "You deserve an <color=green>upgrade!</color> Choose one:";
        abortText2.GetComponent<TMPro.TextMeshProUGUI>().text = "You deserve an <color=green>upgrade!</color> Discuss with the Gunner and pick one!";
        if (upgradeList.Count>0 && powerupList.Count > 0)
        {
            mechanicScreenUppyLayer.SetActive(true);
            upgradeLayer2.SetActive(true);
            upgradeLayer.SetActive(true);
            canUpgrade = true;
            if(/*EnemySpawn.waveCount<upgradeList.Count*/true)
            {
                if (EnemySpawn.waveCount < 3)
                {
                    displayChoice = Random.Range(0, upgradeList.Count / 3);
                    displayChoice2 = Random.Range(0, powerupList.Count / 3);
                }
                else
                {
                    displayChoice = Random.Range(0, upgradeList.Count);
                    displayChoice2 = Random.Range(0, powerupList.Count);
                }
                /*displayChoice = *//*Random.Range(0, *//*EnemySpawn.waveCount - 1*//*upgradeList.Count-1*//*);*//*;
                displayChoice2 = *//*Random.Range(0, *//*EnemySpawn.waveCount - 1 *//*powerupList.Count-1*//*);*//*;*/
                /*        while (displayChoice2 == displayChoice)
                        {
                            displayChoice2 = Random.Range(0, upgradeList.Length);
                        }*/
            }
            displayUpgrades(upgrade1);
            displayPowerups(upgrade2);
            upgradesRolled = true;
        }
        else
        {
            Skip();
        }
    }
    public void displayPowerups(GameObject textField)
    {
        replacedThingBody = ship.GetComponent<Turret>().installedUpgrade;
        mechPlayer2.SetActive(true);
        uppy2Src.SetActive(true);
        switch (powerupList[displayChoice2])
        {
            case "Heat Rounds":
                gunPlayer2.GetComponent<VideoPlayer>().clip = heatVid;
                body.GetComponent<Image>().sprite = pierce;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>HEAT Rounds (Mechanic)</b><br><color=green>+Mechanic activates with<br><color=red>● Select:</color><br>Your bullets destroy any colour ship for " + medDuration+" seconds.</color><color=red><br>-"+longCooldown+" second cooldown<br>-Replaces "+ replacedThingBody +"</color>";
                break;
            case "Repair":
                mechPlayer2.SetActive(false);
                uppy2Src.SetActive(false);
                body.GetComponent<Image>().sprite = repair;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Auto-repair (Mechanic)</b><br><color=green>+Your ship auto-repairs a bit of health every 5 seconds<br><color=red>-Replaces " + replacedThingBody + "</color>";
                break;
            case "Orange Shield":
                gunPlayer2.GetComponent<VideoPlayer>().clip = orangeVid;
                body.GetComponent<Image>().sprite = redPower;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Orange Shield (Mechanic)</b><br><color=green>+Mechanic activates with<br><color=red>● Select:</color><br>Block all <color=#CC4C26>orange (○) fire</color> for " + shortDuration + " seconds.<color=red><br>-" + shortCooldown + " second cooldown<br>-Replaces " + replacedThingBody +"</color>";
                break;
            case "Blue Shield":
                gunPlayer2.GetComponent<VideoPlayer>().clip = blueVid;
                body.GetComponent<Image>().sprite = greenPower;
                body.GetComponent<Image>().sprite = greenPower;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Blue Shield (Mechanic)</b><br><color=green>+Mechanic activates with<br><color=red>● Select:</color><br>Block all <color=#1266E6>blue (☐) fire</color> for " + shortDuration + " seconds.<br><color=red>-"+ shortCooldown + " second cooldown<br>-Replaces " + replacedThingBody + "</color>";
                break;

            case "Electric Override":
                gunPlayer2.GetComponent<VideoPlayer>().clip = overVid;
                body.GetComponent<Image>().sprite = tactialOverride;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Optimized Config (Mechanic)</b><br><color=green>++Greatly reduces heat buildup<br><color=red>--The Mechanic can see colour, but the Gunner cannot.<br>-Replaces " + replacedThingBody + "</color>";
                break;

            case "Enhanced Materials":
                mechPlayer2.SetActive(false);
                uppy2Src.SetActive(false);
                body.GetComponent<Image>().sprite = enhanced;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Enhanced Materials (Mechanic)</b><br><color=green>+Less hits to repair ship<color=red><br>-Replaces " + replacedThingBody + "</color>";
                break;
            case "Thermal Imaging":
                gunPlayer2.GetComponent<VideoPlayer>().clip = thermalVid;
                body.GetComponent<Image>().sprite = Thermals;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Thermal Imaging (Mechanic)</b><br><color=green>+Mechanic activates with<br><color=red>● Select:</color><br>The Mechanic can see colour for " + activationTime + " seconds.<color=red>-" + cooldownTime + " second cooldown<br>-Replaces " + replacedThingBody + "</color>";
                break;
            case "Heavy Armour":
                mechPlayer2.SetActive(false);
                uppy2Src.SetActive(false);
                body.GetComponent<Image>().sprite = heavy;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Heavy Armour (Mechanic)</b><br><color=green>++Greatly increased health<br><color=red>--Extremely slow turn speed<br>-Replaces " + replacedThingBody + "</color>";
                break;
            case "Double Duty":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Double Duty Sniper Tank (Mechanic)<br><color=green>++Massively increased spotting distance<br>++Greatly increased health<br>+Increased rotation speed<br><color=red>----Enemies start coming at you from the rear<br>-replaces " + replacedThingBody + "</color>";
                break;
            case "Small Frame":
                body.GetComponent<Image>().sprite = small;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Small Frame (Mechanic)</b><br><color=green>+Smaller ship size<br><color=red>-Replaces " + replacedThingBody + "</color>";
                break;
            case "Tactical Airstrike":
                gunPlayer2.GetComponent<VideoPlayer>().clip = airVid;
                body.GetComponent<Image>().sprite = airstrike;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Tactical Airstrike (Gunner)</b><br><color=green>++Mechanic activates with<br><color=red>● Select:</color><br>Destroy all visible enemies.<color=red><br>-" + longCooldown + " second cooldown<br>-Replaces " + replacedThingBody + "</color>";
                break;

        }
        mechBody.GetComponent<Image>().sprite = body.GetComponent<Image>().sprite;
        mechanicScreenUppy2.GetComponent<TMPro.TextMeshProUGUI>().text = textField.GetComponent<TMPro.TextMeshProUGUI>().text;
    }
    public void displayUpgrades(GameObject textField)
    {
        mechPlayer1.SetActive(true);
        uppy1Src.SetActive(true);
        replacedThingGun = ship.GetComponent<Turret>().installedGun;
        switch (upgradeList[displayChoice])
        {
            case "Laser":
                gunPlayer1.GetComponent<VideoPlayer>().clip = laserVid;
                gun.GetComponent<Image>().sprite = laserSprite;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Laser Cannon (Gunner)</b> <br><color=green>+Laser Fire</color><br><color=red>-High heat<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Shotgun":
                gunPlayer1.GetComponent<VideoPlayer>().clip = triVid;
                gun.GetComponent<Image>().sprite = triple;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Energy Refractor</b> (Gunner)<br><color=green>+Triple shot<br><color=red>-High heat<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Chain Gun":
                gunPlayer1.GetComponent<VideoPlayer>().clip = chainVid;
                gun.GetComponent<Image>().sprite = chaingun;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Chain Gun (Gunner)</b><br><color=green>+Faster firing<br>+Less heat<br><color=red>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Particle Smasher":
                gunPlayer1.GetComponent<VideoPlayer>().clip = partiVid;
                gun.GetComponent<Image>().sprite = particleSprite;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Particle Smasher (Gunner)</b><br><color=green>++Shoots giant energy balls<br><color=red>--Very high heat<br>-Slow projectile speed<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Improved Bearings":
                mechPlayer1.SetActive(false);
                uppy1Src.SetActive(false);
                gun.GetComponent<Image>().sprite = fast;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Shorty Gun (Gunner)</b><br><color=green>+Faster turret turn speed<color=red><br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Railgun Overcharge":
                gunPlayer1.GetComponent<VideoPlayer>().clip = railVid;
                gun.GetComponent<Image>().sprite = railgun;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Railgun</b> (Gunner)<br><color=green>++Bullets penetrate and destroy every colour ship<color=red><br>--Every shot causes an instant overheat<br>Replaces " + replacedThingGun + "</color>";
                break;
            case "Ricochet Shot":
                gunPlayer1.GetComponent<VideoPlayer>().clip = ricoVid;
                gun.GetComponent<Image>().sprite = ricochet;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Ricochet Shot (Gunner)</b><br><color=green>+Bullets ricochet<br><color=red>-Medium heat<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Reactive Armour":
                gun.GetComponent<Image>().sprite = heavy;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Reactive Armour (Gunner)<br><color=green>++Hold fire to greatly reduce damage from incoming fire of selected colour<br><color=red>--Removes gun<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Dual shot":
                gunPlayer1.GetComponent<VideoPlayer>().clip = dualVid;
                gun.GetComponent<Image>().sprite = dual;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Double Shot (Gunner)</b><br><color=green>+Double shot fire<br><color=red>-Medium heat<br>-Replaces " + replacedThingGun + "</color>";
                break;
            case "Overcharge":
                gun.GetComponent<Image>().sprite = overgunSprite;
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Rerouted Overcharger (Gunner)<br><color=green>++Rapid-fire armour Heat Rounds machine gun<br><color=red>--Each shot has a chance of causing malfunctions<br>-Replaces " + replacedThingGun + "</color>";
                break;
        }
        mechGun.GetComponent<Image>().sprite = gun.GetComponent<Image>().sprite;
        mechanicScreenUppy1.GetComponent<TMPro.TextMeshProUGUI>().text = textField.GetComponent<TMPro.TextMeshProUGUI>().text;
    }
    public void InstallPowerups()
    {
        abortTimer = abortDuration;
        mechanicScreenUppyLayer.SetActive(false);
        upgradeLayer.SetActive(false);
        source.PlayOneShot(ding);
        switch (powerupList[upgradeIndex])
        {
            case "Heat Rounds":
                ship.GetComponent<Turret>().installedUpgrade = "HEAT Round Module";
                hullThing.GetComponent<SpriteRenderer>().sprite = pierce;
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press <color=red>Select ●</color> to activate universal bullets!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().pierceUpgrade = true;

                ship.GetComponent<Turret>().pierceDurationCool = medDuration;
                ship.GetComponent<Turret>().rechargeDuration = longCooldown;

                ship.GetComponent<Turret>().pierceCooldownTime = ship.GetComponent<Turret>().pierceDurationCool;

                break;
            case "Repair":
                hullThing.GetComponent<SpriteRenderer>().sprite = repair;
                ship.GetComponent<Turret>().installedUpgrade = "Auto-repair Module";
                turret.GetComponent<Turret>().powerupStatus.GetComponent<SpriteRenderer>().color = Color.white;
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair ready!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().autoRepair = true;

                break;
            case "Orange Shield":
                hullThing.GetComponent<SpriteRenderer>().sprite = redPower;
                ship.GetComponent<Turret>().installedUpgrade = "Orange Shield Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press <color=red>Select ●</color> to block <color=#CC4C26>orange (○) enemies!</color>";
                setPowerupsFalse();
                ship.GetComponent<Turret>().redShield = true;

                ship.GetComponent<Turret>().pierceDurationCool = shortDuration;
                ship.GetComponent<Turret>().rechargeDuration = shortCooldown;

                ship.GetComponent<Turret>().pierceCooldownTime = ship.GetComponent<Turret>().pierceDurationCool;
                break;
            case "Electric Override":
                hullThing.GetComponent<SpriteRenderer>().sprite = tactialOverride;
                ship.GetComponent<Turret>().installedUpgrade = "Optimized Config";
                turret.GetComponent<Turret>().powerupStatus.GetComponent<SpriteRenderer>().color = Color.white;
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "The Mechanic sees colour, but the Mechanic cannot!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().electricOverride = true;

                ship.GetComponent<Turret>().pierceCooldownTime = ship.GetComponent<Turret>().pierceDurationCool;
                break;
            case "Blue Shield":
                hullThing.GetComponent<SpriteRenderer>().sprite = greenPower;
                setPowerupsFalse();
                ship.GetComponent<Turret>().installedUpgrade = "Blue Shield Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press <color=red>Select ●</color> to block <color=#1266E6>blue (☐) enemies!</color>";
                ship.GetComponent<Turret>().greenShield = true;

                ship.GetComponent<Turret>().pierceDurationCool = shortDuration;
                ship.GetComponent<Turret>().rechargeDuration = shortCooldown;

                ship.GetComponent<Turret>().pierceCooldownTime = ship.GetComponent<Turret>().pierceDurationCool;

                break;
            case "Enhanced Materials":
                hullThing.GetComponent<SpriteRenderer>().sprite = enhanced;
                turret.GetComponent<Turret>().powerupStatus.GetComponent<SpriteRenderer>().color = Color.white;
                ship.GetComponent<Turret>().installedUpgrade = "Enhanced Repair";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Reduces repair time!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().enhancedMaterials = true;
                break;
            case "Thermal Imaging":
                hullThing.GetComponent<SpriteRenderer>().sprite = Thermals;
                ship.GetComponent<Turret>().installedUpgrade = "Thermal Imaging";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press <color=red>Select ●</color> to see colours!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().thermalImaging = true;

                ship.GetComponent<Turret>().pierceDurationCool = longDuration;
                ship.GetComponent<Turret>().rechargeDuration = medDuration;

                ship.GetComponent<Turret>().pierceCooldownTime = ship.GetComponent<Turret>().pierceDurationCool;
                break;
            case "Tactical Airstrike":
                hullThing.GetComponent<SpriteRenderer>().sprite = airstrike;
                ship.GetComponent<Turret>().installedUpgrade = "Tactical Airstrike";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press <color=red>Select ●</color> to destroy all ships!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().tacticalStrike = true;

                ship.GetComponent<Turret>().rechargeDuration = longCooldown;
                break;
            case "Heavy Armour":
                ship.GetComponent<Turret>().maxHealth = ship.GetComponent<Turret>().heavyArmourHealth;
                ship.GetComponent<Turret>().health = ship.GetComponent<Turret>().heavyArmourHealth;
                hullThing.GetComponent<SpriteRenderer>().sprite = heavy;
                turret.GetComponent<Turret>().powerupStatus.GetComponent<SpriteRenderer>().color = Color.white;
                ship.GetComponent<Turret>().installedUpgrade = "Heavy Armour";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Lots of health, but slow gun speed!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().heavyArmour = true;
                break;
            case "Double Duty":
                ship.GetComponent<Turret>().installedUpgrade = "Double Duty Sniper Tank";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().doubleDuty = true;
                break;
            case "Small Frame":
                hullThing.GetComponent<SpriteRenderer>().sprite = small;
                ship.GetComponent<Turret>().installedUpgrade = "Small Frame";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Smaller ship!";
                setPowerupsFalse();
                ship.GetComponent<Turret>().small = true;
                break;
        }
        ship.GetComponent<Turret>().originalMessagePower = ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text;
        /*        setScoreUpgradeReset();
                cam.GetComponent<CamZoom>().zoomIn = false;
                *//*        powerupList.Remove(powerupList[upgradeIndex]);*//*
                upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                EnemySpawn.beginNextWave = true;
                spawner.GetComponent<EnemySpawn>().waveCount++;
                ship.GetComponent<Turret>().health = ship.GetComponent<Turret>().maxHealth;
                spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
                spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
                spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
        *//*        tankAnimate.GetComponent<Animator>().Play("UpgradeReverse");*//*
                installing = false;*/
        powerupList.RemoveAt(displayChoice2);
        upgradeList.RemoveAt(displayChoice);
        levelSkip();
        minimapHull.GetComponent<SpriteRenderer>().sprite = hullThing.GetComponent<SpriteRenderer>().sprite;
        turret.GetComponent<Turret>().rechargeTime = 0;
    }
    public void levelSkip()
    {
        cam.GetComponent<CamZoom>().zoomIn = false;
        /*        powerupList.Remove(powerupList[upgradeIndex]);*/
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        EnemySpawn.waveCount++;
        ship.GetComponent<Turret>().health = ship.GetComponent<Turret>().maxHealth;
        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        if (EnemySpawn.waveCount == EnemySpawn.maxWave)
        {
            spawner.GetComponent<EnemySpawn>().waveTime = EnemySpawn.bossTime;
        }
        else
        {
            spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        }
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
        /*        tankAnimate.GetComponent<Animator>().Play("UpgradeReverse");*/
        installing = false;
        minimapHull.GetComponent<SpriteRenderer>().sprite = hullThing.GetComponent<SpriteRenderer>().sprite;
        spawner.GetComponent<EnemySpawn>().healthUI.SetActive(true);
        waveFlash.GetComponent<Animator>().Play("WaveFlash");
        waveFlash2.GetComponent<Animator>().Play("WaveFlash");
    }
    public void InstallUpgrades()
    {
        abortTimer = abortDuration;
        mechanicScreenUppyLayer.SetActive(false);
        upgradeLayer.SetActive(false);
        source.PlayOneShot(ding);
        switch (upgradeList[upgradeIndex])
        {
            case "Laser":
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.22f, 0);
                turretSprite.GetComponent<SpriteRenderer>().sprite = laserSprite;
                ship.GetComponent<Turret>().installedGun = "Laser";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().laserUpgrade = true;

                break;
            case "Shotgun":
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                turretSprite.GetComponent<SpriteRenderer>().sprite = triple;
                ship.GetComponent<Turret>().installedGun = "Shotgun";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().shotgun = true;
                ship.GetComponent<Turret>().basicGun = true;

                break;
            case "Chain Gun":
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                turretSprite.GetComponent<SpriteRenderer>().sprite = chaingun;
                ship.GetComponent<Turret>().installedGun = "Chain Gun";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().basicGun =true;
                ship.GetComponent<Turret>().chainGun = true;
                break;
            case "Particle Smasher":
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.16f, 0);
                turretSprite.GetComponent<SpriteRenderer>().sprite = particleSprite;
                ship.GetComponent<Turret>().installedGun = "Spitter Launcher";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().smasher = true;
                break;
            case "Improved Bearings":
                turretSprite.GetComponent<SpriteRenderer>().sprite = fast;
                ship.GetComponent<Turret>().installedGun = "Fast Blaster";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().speed = true;
                ship.GetComponent<Turret>().basicGun = true;
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.22f, 0);
                break;
            case "Railgun Overcharge":
                turretSprite.GetComponent<SpriteRenderer>().sprite = railgun;
                ship.GetComponent<Turret>().installedGun = "Railgun";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().singleShot = true;
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                break;
            case "Ricochet Shot":
                turretSprite.GetComponent<SpriteRenderer>().sprite = ricochet;
                ship.GetComponent<Turret>().installedGun = "Ricochet Shot";
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.28f, 0);
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().ricochet = true;
                ship.GetComponent<Turret>().basicGun = true;
                break;
            case "Reactive Armour":
                ship.GetComponent<Turret>().installedGun = "Reactive Armour";
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().reactiveArmour = true;
                break;
            case "Dual shot":
                turretSprite.GetComponent<SpriteRenderer>().sprite = dual;
                ship.GetComponent<Turret>().installedGun = "Dual shot";
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().dualShot = true;
                ship.GetComponent<Turret>().basicGun = false;
                ship.GetComponent<Turret>().shotgun = true;
                break;
            case "Overcharge":
                ship.GetComponent<Turret>().barrelEnd.transform.localPosition = new Vector2(1.62f, 0);
                turretSprite.GetComponent<SpriteRenderer>().sprite = overgunSprite;
                ship.GetComponent<Turret>().installedGun = "Overcharger";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().overChargeGun = true;
                break;
        }
        minimapGun.GetComponent<SpriteRenderer>().sprite = turretSprite.GetComponent<SpriteRenderer>().sprite;
        /*        setScoreUpgradeReset();
                cam.GetComponent<CamZoom>().zoomIn = false;
                *//*        upgradeList.Remove(upgradeList[upgradeIndex]);*//*
                upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                EnemySpawn.beginNextWave = true;
                tankAnimate.GetComponent<Animator>().Play("UpgradeReverse");
                spawner.GetComponent<EnemySpawn>().waveCount++;
                ship.GetComponent<Turret>().health = ship.GetComponent<Turret>().maxHealth;
                spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
                spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
                spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;*/
        upgradeList.RemoveAt(displayChoice);
        powerupList.RemoveAt(displayChoice2);
        levelSkip();
        installing = false;
        turret.GetComponent<Turret>().rechargeTime = 0;
    }
    private void setGunUpgradesFalse()
    {
        ship.GetComponent<Turret>().laserUpgrade = false;
        ship.GetComponent<Turret>().shotgun = false;
        ship.GetComponent<Turret>().basicGun = false;
        ship.GetComponent<Turret>().chainGun = false;
        ship.GetComponent<Turret>().smasher = false;
        ship.GetComponent<Turret>().speed = false;
        ship.GetComponent<Turret>().singleShot = false;
        ship.GetComponent<Turret>().ricochet = false;
        ship.GetComponent<Turret>().reactiveArmour = false;
        ship.GetComponent<Turret>().dualShot = false;
        ship.GetComponent<Turret>().overChargeGun = false;
    }

    private void setPowerupsFalse()
    {
        ship.GetComponent<Turret>().autoRepair = false;
        ship.GetComponent<Turret>().pierceUpgrade = false;
        ship.GetComponent<Turret>().redShield = false;
        ship.GetComponent<Turret>().electricOverride = false;
        ship.GetComponent<Turret>().greenShield = false;
        ship.GetComponent<Turret>().enhancedMaterials = false;
        ship.GetComponent<Turret>().thermalImaging = false;
        ship.GetComponent<Turret>().heavyArmour = false;
        ship.GetComponent<Turret>().doubleDuty = false;
        ship.GetComponent<Turret>().small = false;


    }
    private void setScoreUpgradeReset()
    {
        Turret.scoreToUpgrade = 0;
        ship.GetComponent<Turret>().scoreToUpgradeRequired += ship.GetComponent<Turret>().scoreToUpgradeIncrement;
    }
    private bool Abort()
    {
        if (Input.GetKeyDown("g"))
        {
            source.PlayOneShot(select);
            return true;
        }
        else
        {
            return false;
        }
    }
}
