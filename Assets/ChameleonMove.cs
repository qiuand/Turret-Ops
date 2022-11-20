using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonMove : MonoBehaviour
{
    float moveCooldown;
    float moveDuration = 3.0f;
    bool positionFound = false;
    Vector3 newPos;
    float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        moveCooldown = moveDuration;

        // Update is called once per frame
    }
    void Update()
    {
        moveCooldown -= Time.deltaTime;
        if (moveCooldown <= 0)
        {
            positionFound = false;
            moveCooldown = moveDuration;
        }
        if (positionFound == false)
        {
            newPos = new Vector3(Random.Range(RoamRange.roamRange.rect.xMin, RoamRange.roamRange.rect.xMax), Random.Range(RoamRange.roamRange.rect.yMin, RoamRange.roamRange.rect.yMax), 0) + RoamRange.roamRange.transform.position;
            positionFound = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }
}
