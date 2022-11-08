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
    List<string> upgradeList = new List<string> { "Shotgun", "Laser", "Chain Gun", "Grenade" };
    List<string> powerupList = new List<string> { "Piercing", "Repair", "Red Shield", "Electric Override" };
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
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "HEAT Rounds (Mechanic)<br><color=green>+Bullets destroy any ship</color><br><color=red>-Lasts 5 seconds<br>-30 second cooldown<br>Replaces current Overcharge</color>";
                break;
            case "Repair":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair (Mechanic)<br><color=green>+Automatically repair a bit of health every five seconds<br><color=red>-Replaces current Overcharge</color>";
                break;
            case "Red Shield":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Red Shield (Mechanic)<br><color=green>+Briefly block all incoming red fire<br><color=red>-Lasts 5 seconds<br>-30 escond cooldown<br>-Replaces current overcharge";
                break;

            case "Electric Override":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Optimized Config (Mechanic)<br><color=green>++No barrel heat<br><color=red>--Permanently swap Gunner/Mechanic viewports<br>-Replaces current Overcharge";
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
        }
        powerupList.Remove(upgradeList[upgradeIndex]);
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
    }
    private void setPowerupsFalse()
    {
        ship.GetComponent<Turret>().autoRepair = false;
        ship.GetComponent<Turret>().pierceUpgrade = false;
        ship.GetComponent<Turret>().redShield = false;
        ship.GetComponent<Turret>().electricOverride = false;
    }
}
