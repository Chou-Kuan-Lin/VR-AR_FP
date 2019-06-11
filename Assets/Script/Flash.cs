using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // timer
    float shake;
    Renderer skinRenderer;
    Color flashWarn = new Color(1 / 255f, 1 / 255f, 0.0f, 0f);
    public Scope Scope;

    void Start()
    {
        skinRenderer = this.gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        shake += Time.deltaTime / 2;
        //print("Shake: " + shake);

        /*
        if (Input.GetKey(KeyCode.UpArrow))
            skinRenderer.material.color += flashWarn;
        else if (Input.GetKey(KeyCode.DownArrow))
            skinRenderer.material.color -= flashWarn;
        */
        // print(renderer.material.color);

        // flash treatment part
        if (this.gameObject.name.Contains(Scope.treat_body[Scope.step]))
        {
            //print(Scope.treat_body[Scope.step]);
            if (shake % 1 > 0.5f)
                skinRenderer.material.color += flashWarn;
            else
                skinRenderer.material.color -= flashWarn;
        }
    }
}
