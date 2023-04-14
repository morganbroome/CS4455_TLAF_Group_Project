using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scrappy;
    public Vector3 offset;

    void Start()
    {
        this.transform.position = scrappy.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = scrappy.transform.position + offset;
    }
}
