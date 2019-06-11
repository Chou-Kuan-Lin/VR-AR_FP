using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    int order;
    int count = 1;
    Color color_ready = Color.red;
    Color color_finish = Color.green;
    Color color_next = Color.blue;
    //Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        order = int.Parse(gameObject.name.Substring(4, 2));
        //animator = this.GetComponent<Animator>();
        //animator.enabled = false;

        // ready
        if (order == 1)
            gameObject.GetComponent<Renderer>().material.color = color_ready;
        // next
        else
            gameObject.GetComponent<Renderer>().material.color = color_next;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // finish
            if (order == count)
            {
                //animator.enabled = true;
                gameObject.GetComponent<Renderer>().material.color = color_finish;
            }
            // ready
            if (order == count + 1)
                gameObject.GetComponent<Renderer>().material.color = color_ready;

            count++;
        }
    }
}
