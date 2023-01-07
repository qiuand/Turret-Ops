using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool startShake = false;
    float duration = 0.25f;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (startShake)
        {
            startShake = false;
            StartCoroutine(ShakeCam());
        }
    }
        IEnumerator ShakeCam()
        {
            Vector3 startPos = transform.position;
            float timeElapsed = duration;
            while (timeElapsed > 0)
            {
            timeElapsed -= Time.deltaTime;
                float strength = curve.Evaluate(timeElapsed / duration);
                transform.position = startPos + Random.insideUnitSphere*strength;
                yield return null;
            }
            transform.position = startPos;
        timeElapsed = duration;
        }
    }

