using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    private Camera cam;
    private float zoom=2.09f;
    public float zoomStrength = 2.65487f;
    private float lerpSpeed = 7000;
    public bool zoomIn = false;
    private Vector2 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        originalPos=transform.position;
/*        zoom = cam.orthographicSize;*/
    }

    // Update is called once per frame
    void Update()
    {

        if (zoomIn)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime);
            cam.transform.position = new Vector3 (Mathf.Lerp(transform.position.x, 1.19f, Time.deltaTime), Mathf.Lerp(transform.position.y, -3.14f, Time.deltaTime), -10f);
/*            cam.orthographicSize = 3.21018f;*/
/*            cam.transform.position = new Vector3(-1.26f, -1.36f, -10f);*/
        }
/*        {
            if (cam.orthographicSize > zoom)
            {
                /*    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * lerpSpeed);*/
                /*            }*/
/*            }*/
        if (zoomIn == false)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, Time.deltaTime);
            cam.transform.position = new Vector3(Mathf.Lerp(transform.position.x, originalPos.x, Time.deltaTime), Mathf.Lerp(transform.position.y, originalPos.y, Time.deltaTime), -10f);
        }
/*        {
            if (cam.orthographicSize > zoom)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * lerpSpeed);
            }
        }*/
/*        zoom -= zoomStrength;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime*lerpSpeed);*/
    }
    public void ZoomIn()
    {
        if (cam.orthographicSize > zoom)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * lerpSpeed);
        }
        cam.transform.position = new Vector3(-1.26f, -0.71f, -10f);
        cam.orthographicSize = 3.21018f;
    }
    public void ZoomOut()
    {
        cam.transform.position = new Vector3(-0.55f, 1.13f, -10f);
        cam.orthographicSize = 5f;
    }
}
