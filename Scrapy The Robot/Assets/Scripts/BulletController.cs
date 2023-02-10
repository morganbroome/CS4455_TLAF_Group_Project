using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public bool shootOn = false;
    public GameObject parent;

    void FixedUpdate()
    {
        if (shootOn)
        {
            transform.position += new Vector3((float)0.25, 0, 0);
        } else {
            transform.position = parent.transform.position;
        }

    }

    void Update() 
    {
        if (Input.GetKeyDown("r"))
        {
            shootOn = !shootOn;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            other.gameObject.SetActive(false);
        } else
        {
            transform.position = parent.transform.position;
            shootOn = false;
        }
    }
}