using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int count;
    public GameObject WaterTap;
    public GameObject bowl;

    void Start()
    {
        if (this.transform.parent.name != "Tools")
            return;

        count = 0;
        this.transform.GetChild(0).transform.gameObject.SetActive(false);
        this.transform.GetChild(1).transform.gameObject.SetActive(false);
        this.transform.GetChild(2).transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.parent.name != "Tools")
            return;
        if (WaterTap.activeSelf && count <= 6)
        {
            InvokeRepeating("timer", 1, 1);
        }
        //
        if (count == 1)
            this.transform.GetChild(0).transform.gameObject.SetActive(true);
        else if (count == 3)
            this.transform.GetChild(1).transform.gameObject.SetActive(true);
        else if (count == 6)
            this.transform.GetChild(2).transform.gameObject.SetActive(true);
        //

    }
    void timer()
    {
        count += 1;
        CancelInvoke("timer");
    }
    //
    private void OnTriggerEnter(Collider other)
    {
        if (this.transform.parent.name != "Tools")
            return;
        if (other.tag == "hand")
        {
            count = 0;
            this.transform.GetChild(0).transform.gameObject.SetActive(false);
            this.transform.GetChild(1).transform.gameObject.SetActive(false);
            this.transform.GetChild(2).transform.gameObject.SetActive(false);
        }
    }
}