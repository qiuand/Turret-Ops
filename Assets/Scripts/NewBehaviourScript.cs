using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public static List<float> highScoreStore=Turret.highScoreList;
    public GameObject scoreText;
    public GameObject scoreText2;
    public GameObject highScore;
    public GameObject highScore2;
    public GameObject text;
    public GameObject text2;
    string highScoreMsg = "";
    // Start is called before the first frame update
    void Start()
    {
        if (Turret.highScoreFlag)
        {
            highScoreMsg = "<b><color=green>New High Score!</b><br></color>";
            PlayerPrefs.SetFloat("hs1", Turret.highScoreList[0]);
            PlayerPrefs.SetFloat("hs2", Turret.highScoreList[1]);
            PlayerPrefs.SetFloat("hs3", Turret.highScoreList[2]);
            PlayerPrefs.SetFloat("hs4", Turret.highScoreList[3]);
            PlayerPrefs.SetFloat("hs5", Turret.highScoreList[4]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetKeyDown("1"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Game");
            Turret.highScoreFlag = false;
        }
        if (Input.GetKeyDown("2"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Main");
            Turret.highScoreFlag = false;
        }
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>Restart at<br>WAVE " + EnemySpawn.waveCount;
        text2.GetComponent<TMPro.TextMeshProUGUI>().text = "<b>You can restart from WAVE " + EnemySpawn.waveCount + "!";/*text.GetComponent<TMPro.TextMeshProUGUI>().text;*/
/*        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
*//*        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
*/        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=green><b>Final Score: " + Turret.score + "</color></b><br><br>" + highScoreMsg + "<b>High Scores:</b><br><br>1st Place: " + Turret.highScoreList[0] + "<br>2nd Place: " + Turret.highScoreList[1] + "<br>3rd Place: " + Turret.highScoreList[2] + "<br>4th Place: " + Turret.highScoreList[3] + "<br>5th Place: " + Turret.highScoreList[4];
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = highScore.GetComponent<TMPro.TextMeshProUGUI>().text;
    }
}

