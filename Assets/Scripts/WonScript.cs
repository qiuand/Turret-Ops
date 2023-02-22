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
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=green><b>Final Score: " + Turret.score + "</color></b><br><br>" + highScoreMsg + "<b>High Scores:</b><br><br>1st Place: " + Turret.highScoreList[0] + "<br>2nd Place: " + Turret.highScoreList[1] + "<br>3rd Place: " + Turret.highScoreList[2] + "<br>4th Place: " + Turret.highScoreList[3] + "<br>5th Place: " + Turret.highScoreList[4];

        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text;
        /*        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Restart at wave " + EnemySpawn.waveCount + " with basic ship";
                text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;*/
    }
}

