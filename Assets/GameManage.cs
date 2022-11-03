using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            Screen.fullScreen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
