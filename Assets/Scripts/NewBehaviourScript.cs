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
        if (Input.GetKeyDown("space"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown("1"))
        {
            Turret.score = 0;
            SceneManager.LoadScene("Main");
        }
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Try again from checkpoint wave " + EnemySpawn.waveCount + "?";
        text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
    }
}

