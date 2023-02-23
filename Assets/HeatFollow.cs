using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatFollow : MonoBehaviour
{
    public Transform turretPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(turretPos.position.x+1.25f, turretPos.position.y, turretPos.position.z - 3);
    }
}
