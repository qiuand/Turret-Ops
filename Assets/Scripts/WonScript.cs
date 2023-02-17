using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WonScript : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject scoreText2;
    // Start is called before the first frame update
    void Start()
    {
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
            Turret.score = 0;
            SceneManager.LoadScene("Main");
        }
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
        scoreText2.GetComponent<TMPro.TextMeshProUGUI>().text = "Final Score: " + Turret.score;
        /*        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Restart at wave " + EnemySpawn.waveCount + " with basic ship";
                text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;*/
    }
}

