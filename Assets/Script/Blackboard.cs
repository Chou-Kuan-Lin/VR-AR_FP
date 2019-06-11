using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Blackboard : MonoBehaviour
{
    public Scope scope;
    public String[] text;
    String[] endText = { "治療失敗導致傷口惡化，需送醫治療", "算是處理好傷口了，但還能更好！", "傷口處理非常完善！可以加快復原" };
    public GameObject Audio;
    public bool play = false;

    // Update is called once per frame
    void Update()
    {
        //print(scope.step);
        if (play == false)
            if (scope.step + 1 == text.Length)
            {
                // show the end text
                if (scope.GetTransform.localScale.x < scope.max * 0.3)
                {
                    this.GetComponent<TextMesh>().text = endText[0];
                    Audio.transform.GetChild(2).GetComponent<AudioSource>().Play();
                }
                else if (scope.GetTransform.localScale.x > scope.max * 0.7)
                {
                    this.GetComponent<TextMesh>().text = endText[2];
                    Audio.transform.GetChild(0).GetComponent<AudioSource>().Play();
                }
                else
                {
                    this.GetComponent<TextMesh>().text = endText[1];
                    Audio.transform.GetChild(1).GetComponent<AudioSource>().Play();
                }
                play = true;
            }
        else
            this.GetComponent<TextMesh>().text = text[scope.step];
    }
}
