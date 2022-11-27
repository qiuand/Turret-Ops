using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
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
            Turret.Checkpoints();
            SceneManager.LoadScene("Game");

        }
        if (Input.GetKeyDown("g"))
        {
            Turret.Checkpoints();
            SceneManager.LoadScene("Main");
        }

    }
}

