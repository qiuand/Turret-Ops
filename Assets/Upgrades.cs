using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    AudioSource source;
    public AudioClip ding;
    public static bool canUpgrade=false;
    string[] upgradeList = new string[] { "Shotgun", "Laser", "Chain Gun" };
    string[] powerupList = new string[] { "Piercing", "Repair" };
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
        }
    }

    public void RollUpgrades()
    {
        canUpgrade = true;
        displayChoice = Random.Range(0, upgradeList.Length);
        displayChoice2 = Random.Range(0, powerupList.Length);
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
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "HEAT Rounds (Mechanic Overcharge)<br><color=green>+Bullets destroy any ship</color><br><color=red>-Lasts 5 seconds<br>-30 second cooldown<br>Replaces current Overcharge</color>";
                break;
            case "Repair":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair (Mechanic Overcharge)<br><color=green>+Automatically repair a bit of health every five seconds<br><color=red>-Replaces current Overcharge</color>";
                break;
        }
    }
    public void displayUpgrades(GameObject textField, int displayIndex)
    {
        switch (upgradeList[displayIndex])
        {
            case "Laser":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy Condensor (Weapon) <br><color=green>+Ceoncentrated Laser Fire</color><br><color=red>-Severe barrel heat<br>-Replaces current weapon</color>";
                break;
            case "Shotgun":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy Refractor (Weapon)<br><color=green>+Tri-laser shot<br><color=red>-Increased barrel heat<br>-Replaces current weapon";
                break;
            case "Chain Gun":
                textField.GetComponent<TMPro.TextMeshProUGUI>().text = "Chain Gun (Weapon)<br><color=green>+Increased fire rate<br>+Decreased Barrel Heat<br><color=red>-Replaces current weapon";
                break;
        }
    }
    public void InstallPowerups()
    {
        source.PlayOneShot(ding);
        print("yoy");
        print("heya"+powerupList[upgradeIndex]);
        switch (powerupList[upgradeIndex])
        {
            case "Piercing":
                print("yesn't");
                ship.GetComponent<Turret>().installedUpgrade = "HEAT Overcharge";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press Emergency Override to Use";
                ship.GetComponent<Turret>().pierceUpgrade = true;
                ship.GetComponent<Turret>().autoRepair = false;
                break;
            case "Repair":
                ship.GetComponent<Turret>().installedUpgrade = "Auto-repair Module";
                ship.GetComponent<Turret>().powerupCoolText.GetComponent<TMPro.TextMeshProUGUI>().text = "Auto-repair ready";
                ship.GetComponent<Turret>().autoRepair = true;
                ship.GetComponent<Turret>().pierceUpgrade = false;
                break;
        }
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
        source.PlayOneShot(ding);
        switch (upgradeList[upgradeIndex])
        {
            case "Laser":
                ship.GetComponent<Turret>().installedGun = "Energy Condensor";
                ship.GetComponent<Turret>().laserUpgrade = true;
                ship.GetComponent<Turret>().shotgun = false;
                ship.GetComponent<Turret>().basicGun = false;
                ship.GetComponent<Turret>().chainGun = false;
                break;
            case "Shotgun":
                ship.GetComponent<Turret>().installedGun = "Energy Refractor";
                ship.GetComponent<Turret>().laserUpgrade = false;
                ship.GetComponent<Turret>().shotgun = true;
                ship.GetComponent<Turret>().basicGun = true;
                ship.GetComponent<Turret>().chainGun = false;
                break;
            case "Chain Gun":
                ship.GetComponent<Turret>().installedGun = "Chain Gun";
                ship.GetComponent<Turret>().laserUpgrade = false;
                ship.GetComponent<Turret>().shotgun = false;
                ship.GetComponent<Turret>().basicGun =true;
                ship.GetComponent<Turret>().chainGun = true;
                break;

        }
        upgrade1.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        upgrade2.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        EnemySpawn.beginNextWave = true;
        spawner.GetComponent<EnemySpawn>().waveCount++;

        spawner.GetComponent<EnemySpawn>().waveDuration += spawner.GetComponent<EnemySpawn>().waveTimeIncrement;
        spawner.GetComponent<EnemySpawn>().waveTime = spawner.GetComponent<EnemySpawn>().waveDuration;
        spawner.GetComponent<EnemySpawn>().waveTimer = spawner.GetComponent<EnemySpawn>().waveTiming;
    }
}
