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
    public void Laser()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }
}
