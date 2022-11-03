using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            SceneManager.LoadScene("Game");

        }
    }
}
