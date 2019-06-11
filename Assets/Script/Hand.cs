using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    GameObject takeObject;
    public int childCount;
    public GameObject Obj;
    public GameObject anotherObj;

    public Scope Scope;
    public Blackboard Blackboard;
    public GameObject HP;
    public GameObject Audio;
    int scope_total = 0;
    int scope_body = 1;
    int scope_tool = 1;
    int scope_medicine = 2;
    public Material skinMaterial;
    GameObject[] skinTag;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        //gameObjectChild = this.gameObject.transform.GetChild(0).gameObject;
        skinTag = GameObject.FindGameObjectsWithTag("Skin");
    }

    // Update is called once per frame
    void Update()
    {
        // move character
        /*
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1f, 0f, 0f) * times;
        else if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(1f, 0f, 0f) * times;
        else if (Input.GetKey(KeyCode.Q))
            transform.position += new Vector3(0f, 1f, 0f) * times;
        else if (Input.GetKey(KeyCode.E))
            transform.position -= new Vector3(0f, 1f, 0f) * times;
        else if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0f, 0f, 1f) * times;
        else if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0f, 0f, 1f) * times;
        */

        // keep object position
        try
        {
            if (takeObject.name == "Bottle")
                takeObject.transform.position = transform.position + new Vector3(-50f, -20f, -10f);
            else
                takeObject.transform.position = transform.position + new Vector3(0f, 10f, -10f);
        }
        catch (Exception e)
        { }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Washbasin")
        {
            other.transform.GetChild(1).transform.gameObject.SetActive(!other.transform.GetChild(1).transform.gameObject.activeSelf);
            return;
        }
        if (other.transform.root.name == "Tools")
        {
            // add take object sound
            try
            {
                other.GetComponent<AudioSource>().Play();
            }
            catch (Exception e)
            { }

            if (Obj.transform.childCount > 0)
                Destroy(takeObject);

            {
                // take object in hand
                if (other.name == "Bottle")
                    takeObject = Instantiate(other.gameObject, other.transform.position + new Vector3(-50f, -20f, -10f), other.transform.rotation);
                else
                    takeObject = Instantiate(other.gameObject, other.transform.position + new Vector3(0f, 10f, -10f), other.transform.rotation);

                takeObject.transform.localScale = new Vector3(takeObject.transform.localScale.x * other.gameObject.transform.parent.localScale.x,
                                                               takeObject.transform.localScale.y * other.gameObject.transform.parent.localScale.y,
                                                               takeObject.transform.localScale.z * other.gameObject.transform.parent.localScale.z);


                takeObject.transform.SetParent(Obj.transform);
                takeObject.GetComponent<Collider>().isTrigger = true;
                takeObject.GetComponent<Rigidbody>().useGravity = false;
                takeObject.name = takeObject.name.Replace("(Clone)", "");
                if (takeObject.name == "Bowl" && !takeObject.transform.GetChild(2).gameObject.activeSelf)
                    takeObject.name = "Bowl_fail";
            }
        }

        // treatment man
        if (other.transform.root.name == "Man")
        {
            //print("Step: " + Scope.step);
            scope_total = 0;

            // check body
            if (other.transform.name == Scope.treat_body[Scope.step])
                scope_total += scope_body;
            else
                scope_total -= scope_body;

            // record take object name ( none for empty )
            String thisHandObject = "";
            String anotherHandObject = "";
            try
            {
                thisHandObject = Obj.transform.GetChild(0).name;
            }
            catch (Exception e) { }
            try
            {
                anotherHandObject = anotherObj.transform.GetChild(0).name;
            }
            catch (Exception e) { }

            if (thisHandObject != "")
            {
                // check tool
                if ((Scope.treat_tool[Scope.step] == thisHandObject) || (Scope.treat_tool[Scope.step] == anotherHandObject))
                    scope_total += scope_tool;
                else
                    scope_total -= scope_tool;

                // check medicine
                if ((Scope.treat_medicine[Scope.step] == thisHandObject) || (Scope.treat_medicine[Scope.step] == anotherHandObject))
                    scope_total += scope_medicine;
                else
                    scope_total -= scope_medicine;

                // clean take object
                if (Obj.transform.childCount > 0)
                {
                    Destroy(Obj.transform.GetChild(0).gameObject);
                    thisHandObject = "";
                }
                if (anotherObj.transform.childCount > 0)
                {
                    Destroy(anotherObj.transform.GetChild(0).gameObject);
                    anotherHandObject = "";
                }

                // health point change
                if (scope_total >= 0 && Scope.GetTransform.localScale.x < Scope.max)
                {
                    //print("Health Point Plus.");
                    HP.GetComponent<Transform>().position += Scope.offset / 2;
                    HP.GetComponent<Transform>().localScale += Scope.times;
                    Audio.transform.GetChild(0).GetComponent<AudioSource>().Play();
                }
                else if (scope_total < 0 && Scope.GetTransform.localScale.x > 0)
                {
                    //print("Health Point Minus.");
                    HP.GetComponent<Transform>().position -= Scope.offset / 2;
                    HP.GetComponent<Transform>().localScale -= Scope.times;
                    Audio.transform.GetChild(1).GetComponent<AudioSource>().Play();
                }

                // recover skin color
                for (int i = 0; i < skinTag.Length; i++)
                    skinTag[i].GetComponent<Renderer>().material = skinMaterial;

                // next step
                if (Scope.step + 1 != Scope.treat_body.Length)
                    Scope.step++;
                else if (Scope.step + 1 == Scope.treat_body.Length)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //Blackboard.play = false;
                    //Scope.step = 0;
                    //HP.GetComponent<Transform>().localScale = new Vector3(120f, 40f, 40f);
                }
            }
        }
    }
}
