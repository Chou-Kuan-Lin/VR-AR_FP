using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scope : MonoBehaviour
{
    public Transform GetTransform;
    Renderer GetRenderer;
    public Vector3 times;
    public Vector3 offset;
    public int max;
    Color color_low = Color.red;
    Color color_middle = Color.yellow;
    Color color_high = Color.green;

    // teatment variables
    // Separator: . symbol
    public String[] treat_body;
    public String[] treat_tool;
    public String[] treat_medicine;
    public int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetTransform = this.GetComponent<Transform>();
        GetRenderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // min point
        // keyboard controller
        if (Input.GetKeyDown(KeyCode.UpArrow) && GetTransform.localScale.x < max)
        {
            GetComponent<Transform>().position += offset / 2;
            GetComponent<Transform>().localScale += times;
            //this.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && GetTransform.localScale.x > 0)
        {
            GetComponent<Transform>().position -= offset / 2;
            GetComponent<Transform>().localScale -= times;
            //this.transform.GetChild(1).GetComponent<AudioSource>().Play();
        }

        // change health point color
        if (GetTransform.localScale.x < max * 0.3)
            GetRenderer.material.color = color_low;
        else if (GetTransform.localScale.x > max * 0.7)
            GetRenderer.material.color = color_high;
        else
            GetRenderer.material.color = color_middle;
    }
}
