using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserToggle : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            //if (laser.activeSelf)
            //{
            //    laser.SetActive(false);
            //} else
            //{
            //    laser.SetActive(true);
            //}
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        }
    }
}
