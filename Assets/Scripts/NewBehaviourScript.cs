using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject scoreText2;

    public GameObject text;
    public GameObject text2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetKeyDown("2"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown("2"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Main");
        }
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "You can restart at checkpoint wave " + EnemySpawn.waveCount +"/"+EnemySpawn.maxWave+"!";
        text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
    }
}

