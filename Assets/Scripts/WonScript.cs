using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WonScript : MonoBehaviour
{
    string highScoreMsg = "";
    public GameObject scoreText;
    public GameObject scoreText2;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("hs1", Turret.highScoreList[0]);
        PlayerPrefs.SetFloat("hs2", Turret.highScoreList[1]);
        PlayerPrefs.SetFloat("hs3", Turret.highScoreList[2]);
        PlayerPrefs.SetFloat("hs4", Turret.highScoreList[3]);
        PlayerPrefs.SetFloat("hs5", Turret.highScoreList[4]);

        PlayerPrefs.SetFloat("hsw1", Turret.highWaveList[0]);
        PlayerPrefs.SetFloat("hsw2", Turret.highWaveList[1]);
        PlayerPrefs.SetFloat("hsw3", Turret.highWaveList[2]);
        PlayerPrefs.SetFloat("hsw4", Turret.highWaveList[3]);
        PlayerPrefs.SetFloat("hsw5", Turret.highWaveList[4]);
        NewBehaviourScript.highScoreStore = Turret.highScoreList;
        if (Turret.highScoreFlag)
        {
            highScoreMsg = "<b><color=green>New High Score!</b></color><br>";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
/*        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Game");
        }*/
        if (Input.GetKeyDown("1"))
        {
            Turret.highScoreFlag = false;
            Turret.score = 0;
            SceneManager.LoadScene("Main");
            EnemySpawn.beginNextWave = true;
        }
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=green><b>Final Score: " + Turret.score + "</color></b><br><br>" + highScoreMsg + "<b>High Scores:</b><br><br>1st Place: " + Turret.highScoreList[0] + " (Wave " + Turret.highWaveList[0] + ")<br>2nd Place: " + Turret.highScoreList[1] + " (Wave "+Turret.highWaveList[1]+"<br>3rd Place: " + Turret.highScoreList[2] + " (Wave " + Turret.highWaveList[2] + ")<br>4th Place: " + Turret.highScoreList[3] + " (Wave " + Turret.highWaveList[3] + ")<br>5th Place: " + Turret.highScoreList[4]+" (Wave "+Turret.highWaveList[4]+")";
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text;
        /*        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Restart at wave " + EnemySpawn.waveCount + " with basic ship";
                text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;*/
    }
}

