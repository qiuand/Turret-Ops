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
    bool clickFlag = false;
    AudioSource src;
    public GameObject menuAnim;
    public GameObject text;
    public static int index = 1;
    string difficulty;

    // Start is called before the first frame update
    public static bool inTutorial = false;
    private void Start()
    {
        NewBehaviourScript.highScoreStore = Turret.highScoreList;
        src = GetComponent<AudioSource>();
        text.GetComponent<TMPro.TextMeshProUGUI>().text = "Change Difficulty<br><color=green>(Normal: Extra Score)</color>";
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKey(KeyCode.L))
                {
                    PlayerPrefs.SetFloat("hs1", 0);
                    PlayerPrefs.SetFloat("hs2", 0);
                    PlayerPrefs.SetFloat("hs3", 0);
                    PlayerPrefs.SetFloat("hs4", 0);
                    PlayerPrefs.SetFloat("hs5", 0);

                    PlayerPrefs.SetFloat("hsw1", 0);
                    PlayerPrefs.SetFloat("hsw2", 0);
                    PlayerPrefs.SetFloat("hsw3", 0);
                    PlayerPrefs.SetFloat("hsw4", 0);
                    PlayerPrefs.SetFloat("hsw5", 0);
                }
            }
        }
        Cursor.visible = false;

        if (clickFlag == false)
        {
            if (Input.GetKeyDown("1"))
            {
                clickFlag = true;
                src.Play();
                EnemySpawn.waveCount = 1;
                inTutorial = true;
                StartGame();
                /*            SceneManager.LoadScene("Game");*/
            }
            if (Input.GetKeyDown("space"))
            {
                clickFlag = true;
                src.Play();
                EnemySpawn.beginNextWave = true;
                EnemySpawn.waveCount = 1;
                inTutorial = false;
                StartGame();
                /*            SceneManager.LoadScene("Game");*/
            }
        }
        if (Input.GetKeyDown("2"))
        {
            src.Play();
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
    public void StartGame()
    {
        menuAnim.GetComponent<Animator>().SetBool("Takeoff", true);
        StartCoroutine(WaitFunc());
    }
    IEnumerator WaitFunc()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Game");
    }
}

