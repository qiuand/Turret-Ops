using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WonScript : MonoBehaviour
{
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
/*        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Game");
        }*/
        if (Input.GetKeyDown("g"))
        {
            SceneManager.LoadScene("Main");
        }
/*        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Restart at wave " + EnemySpawn.waveCount + " with basic ship";
        text2.GetComponent<TMPro.TextMeshProUGUI>().text = text.GetComponent<TMPro.TextMeshProUGUI>().text;*/
    }
}

