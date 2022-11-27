using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialTrack: MonoBehaviour
{
    public bool inTutorial = false;
}
public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool inTutorial = false;
    private void Start()
    {
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
        if (Input.GetKeyDown("g"))
        {
            EnemySpawn.waveCount = 1;
            inTutorial = true;
            SceneManager.LoadScene("Game");
        }
    }
}

