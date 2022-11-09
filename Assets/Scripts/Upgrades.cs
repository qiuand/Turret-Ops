using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    public GameObject upgradeLayer;
    AudioSource source;
    public AudioClip ding;
    public static bool canUpgrade=false;
    List<string> upgradeList = new List<string> { "Shotgun", "Laser", "Chain Gun", "Grenade", "Speed", "Single Shot", "Ricochet Shot", "Reactive Armour", "Charge Shot","Overcharge" };
    List<string> powerupList = new List<string> { "Piercing", "Repair", "Red Shield", "Electric Override", "Green Shield", "Enhanced Materials", "Thermal Imaging", "Heavy Armour", "Double Duty", "Small" };
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
    // Start is called before the first frame update
    void Start()
    {
        upgradeLayer.SetActive(false);
        source = GetComponent<AudioSource>();
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (canUpgrade)
        {
            if (Input.GetKeyDown("2"))
            {
                print("yee");
                chosenUpgrade = upgradeList[displayChoice2];
                upgradeChosen = true;
                upgradeIndex = displayChoice2;
                InstallPowerups();
                canUpgrade = false;
            }
            if (Input.GetKeyDown("1"))
            {
                print("Oh boy");
                chosenUpgrade = upgradeList[displayChoice];
                upgradeChosen = true;
            upgradeIndex = displayChoice;
                InstallUpgrades();
                canUpgrade = false;
            }
            if (Input.GetKeyDown("3"))
            {
                Skip();
            }
        }
    }
    public void Skip()
    {
        upgradeLayer.SetActive(false);
        source.PlayOneShot(ding);
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        spawner.GetComponent<EnemySpawn>().waveCount++;

        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
    }
    public void RollUpgrades()
    {
        upgradeLayer.SetActive(true);
        canUpgrade = true;
        displayChoice = Random.Range(0, upgradeList.Count);
        displayChoice2 = Random.Range(0, powerupList.Count);
/*        while (displayChoice2 == displayChoice)
        {
            displayChoice2 = Random.Range(0, upgradeList.Length);
        }*/
        displayUpgrades(upgrade1, displayChoice);
        displayPowerups(upgrade2);
        upgradesRolled = true;
    }
    public void displayPowerups(GameObject textField)
    {
        switch (powerupList[displayChoice2])
        {
            case "Piercing":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "HEAT Rounds (Mechanic)<br><color=green>+Bullets destroy any ship</color><br><color=red>-Lasts 5 seconds<br>-30 second cooldown<br>Replaces current Mechanic upgrade</color>";
                break;
            case "Repair":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair (Mechanic)<br><color=green>+Automatically repair a bit of health every five seconds<br><color=red>-Replaces current Mechanic upgrade</color>";
                break;
            case "Red Shield":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Red Shield (Mechanic)<br><color=green>+Briefly block all incoming red fire<br><color=red>-Lasts 5 seconds<br>-30 escond cooldown<br>-Replaces current Mechanic upgradee";
                break;
            case "Green Shield":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Green Shield (Mechanic)<br><color=green>+Briefly block all incoming green fire<br><color=red>-Lasts 5 seconds<br>-30 escond cooldown<br>-Replaces current Mechanic upgradee";
                break;

            case "Electric Override":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Optimized Config (Mechanic)<br><color=green>++No barrel heat<br><color=red>--Permanently swap Gunner/Mechanic viewports<br>-Replaces current Mechanic upgrade";
                break;

            case "Enhanced Materials":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Enhanced Materials (Mechanic)<br><color=green>+Less hits to repair ship<color=red>-Replaces current Mechanic upgrade";
                break;
            case "Thermal Imaging":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Camera Override (Mechanic)<br><color=green>+Briefly view the Gunner's viewport<br><color=red>-Lasts 10 seconds<br>-30 second cooldown<br>-Replaces current Mechanic upgrade";
                break;
            case "Heavy Armour":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Heavy Armour (Mechanic)<br><color=green>++Massively increased health<br>+Less hits needed for repair<br><color=red>--Extremely slow turn speed<br>-Replaces current Mechanic upgrade";
                break;
            case "Double Duty":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Double Duty Sniper Tank (Mechanic)<br><color=green>++Massively increased spotting distance<br>++Greatly increased health<br>+Increased rotation speed<br><color=red>----Enemies start coming at you from the rear<br>-replaces current Mechanic upgrade";
                break;
            case "Small":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Small Frame (Mechanic)<br><color=green>+Smaller ship size<br><color=red>-Replaces current Mechanic upgrade";
                break;

        }
    }
    public void displayUpgrades(GameObject textField, int displayIndex)
    {
        switch (upgradeList[displayIndex])
        {
            case "Laser":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy Condensor (Gunner) <br><color=green>+Ceoncentrated Laser Fire</color><br><color=red>-Severe barrel heat<br>-Replaces current weapon</color>";
                break;
            case "Shotgun":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy Refractor (Gunner)<br><color=green>+Tri-laser shot<br><color=red>-Increased barrel heat<br>-Replaces current weapon";
                break;
            case "Chain Gun":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Chain Gun (Gunner)<br><color=green>+Increased fire rate<br>+Decreased Barrel Heat<br><color=red>-Replaces current weapon";
                break;
            case "Grenade":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Particle Smasher (Gunner)<br><color=green>++Launches giant energy emitting spheres<br><color=red>--Extreme heat<br>-Slow projectile speed<br>-Replaces current weapon";
                break;
            case "Speed":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Improved Bearings (Gunner)<br><color=green>+Faster turret turn speed<color=red><br>-Replaces current weapon";
                break;
            case "Single Shot":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Railgun Overcharge (Gunner)<br><color=green>++High power piercing bullet that rips through any colour ship<color=red><br>--Instantly overheats barrel<br>Replaces current weapon";
                break;
            case "Ricochet Shot":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Matter Destabilizer (Gunner)<br><color=green>+Burst fire<br>+Bullets ricochet<br><color=red>--Instantly overheats barrel<br>Replaces current weapon";
                break;
            case "Reactive Armour":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Reactive Armour (Gunner)<br><color=green>++Hold fire to greatly reduce damage from incoming fire of selected colour<br><color=red>--Removes gun<br>-Replaces current gunner upgrade";
                break;
            case "Charge Shot":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Double Shot (Gunner)<br><color=green>+Dual shot split fire<br><color=red>-Slightly increased heat<br>-Replaces current upgrade";
                break;
            case "Overcharge":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Rerouted Overcharger (Gunner)<br><color=green>++Rapid-fire armour piercing machine gun<br><color=red>--Each shot has a chance of causing malfunctions<br>-replaces current weapon";
                break;
        }
    }
    public void InstallPowerups()
    {
        upgradeLayer.SetActive(false);
        source.PlayOneShot(ding);
        print("yoy");
        print("heya"+powerupList[upgradeIndex]);
        switch (powerupList[upgradeIndex])
        {
            case "Piercing":
                print("yesn't");
                ship.GetComponent<Turret>().installedUpgrade = "HEAT Round Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press Emergency Override to Use";
                setPowerupsFalse();
                ship.GetComponent<Turret>().pierceUpgrade = true;
                break;
            case "Repair":
                ship.GetComponent<Turret>().installedUpgrade = "Auto-repair Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair ready";
                setPowerupsFalse();
                ship.GetComponent<Turret>().autoRepair = true;
                break;
            case "Red Shield":
                ship.GetComponent<Turret>().installedUpgrade = "Red Shield Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Red Shield-charge ready";
                setPowerupsFalse();
                ship.GetComponent<Turret>().redShield = true;
                break;
            case "Electric Override":
                ship.GetComponent<Turret>().installedUpgrade = "Optimized Config";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().electricOverride = true;
                break;
            case "Green Shield":
                setPowerupsFalse();
                ship.GetComponent<Turret>().installedUpgrade = "Green Shield Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Green Shield-charge ready";
                ship.GetComponent<Turret>().greenShield = true;
                break;
            case "Enhanced Materials":
                ship.GetComponent<Turret>().installedUpgrade = "Enhanced Materials";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().enhancedMaterials = true;
                break;
            case "Thermal Imaging":
                ship.GetComponent<Turret>().installedUpgrade = "Thermal Imaging";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().thermalImaging = true;
                break;
            case "Heavy Armour":
                ship.GetComponent<Turret>().installedUpgrade = "Heavy Armour";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().heavyArmour = true;
                break;
            case "Double Duty":
                ship.GetComponent<Turret>().installedUpgrade = "Double Duty Sniper Tank";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().doubleDuty = true;
                break;
            case "Small":
                ship.GetComponent<Turret>().installedUpgrade = "Small Frame";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Action Needed";
                setPowerupsFalse();
                ship.GetComponent<Turret>().small = true;
                break;
        }
        powerupList.Remove(powerupList[upgradeIndex]);
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        spawner.GetComponent<EnemySpawn>().waveCount++;

        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
    }
    public void InstallUpgrades()
    {
        upgradeLayer.SetActive(false);
        source.PlayOneShot(ding);
        switch (upgradeList[upgradeIndex])
        {
            case "Laser":
                ship.GetComponent<Turret>().installedGun = "Energy Condensor";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().laserUpgrade = true;

                break;
            case "Shotgun":
                ship.GetComponent<Turret>().installedGun = "Energy Refractor";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().shotgun = true;
                ship.GetComponent<Turret>().basicGun = true;

                break;
            case "Chain Gun":
                ship.GetComponent<Turret>().installedGun = "Chain Gun";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().basicGun =true;
                ship.GetComponent<Turret>().chainGun = true;
                break;
            case "Grenade":
                ship.GetComponent<Turret>().installedGun = "Spitter Launcher";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().smasher = true;
                break;
            case "Speed":
                ship.GetComponent<Turret>().installedGun = "Fast Blaster";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().speed = true;
                break;
            case "Single Shot":
                ship.GetComponent<Turret>().installedGun = "Railgun";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().singleShot = true;
                break;
            case "Ricochet Shot":
                ship.GetComponent<Turret>().installedGun = "Matter Destabilizer";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().ricochet = true;
                break;
            case "Reactive Armour":
                ship.GetComponent<Turret>().installedGun = "Reactive Armour";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().reactiveArmour = true;
                break;
            case "Charge Shot":
                ship.GetComponent<Turret>().installedGun = "Dual Blaster";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().dualShot = true;
                break;
            case "Overcharge":
                ship.GetComponent<Turret>().installedGun = "Overcharger";
                setGunUpgradesFalse();
                ship.GetComponent<Turret>().overChargeGun = true;
                break;
        }
        upgradeList.Remove(upgradeList[upgradeIndex]);
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        spawner.GetComponent<EnemySpawn>().waveCount++;

        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
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
}
