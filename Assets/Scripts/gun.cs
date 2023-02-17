using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TutorialTrack: MonoBehaviour
{
    public bool inTutorial = false;
}
public class gun : MonoBehaviour
{
    public GameObject text;
    public static int index = 1;
    string difficulty;

    // Start is called before the first frame update
    public static bool inTutorial = false;
    private void Start()
    {
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Change Difficulty<br><color=green>(Normal: Extra Score)</color>";
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetKeyDown("space")){
            EnemySpawn.waveCount = 1;
            inTutorial = false;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown("1"))
        {
            EnemySpawn.waveCount = 1;
            inTutorial = true;
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown("2"))
        {
            if (index < 2)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            switch (index)
            {
                case 0:
                    difficulty = "Easy: Normal Score";
                    break;
                case 1:
                    difficulty = "Normal: Extra Score";
                    break;
                case 2:
                    difficulty = "Hard: Extra Score++";
                    break;
            }
            switchText();
        }

    }
    public void switchText()
    {
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Change Difficulty<br><color=green>(" + difficulty + ")</color>";
    }
}

