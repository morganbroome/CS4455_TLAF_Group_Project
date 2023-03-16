using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float life = 3;
    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Breakable") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    //public bool shootOn = false;
    //public GameObject parent;

    //void FixedUpdate()
    //{
    //    if (shootOn)
    //    {
    //        transform.position += new Vector3(0, 0, (float)0.25);
    //    } else {
    //        transform.position = parent.transform.position;
    //    }

    //}

    //void Update() 
    //{
    //    if (Input.GetKeyDown("r"))
    //    {
    //        shootOn = !shootOn;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Breakable"))
    //    {
    //        other.gameObject.SetActive(false);
    //    } else
    //    {
    //        transform.position = parent.transform.position;
    //        shootOn = false;
    //    }
    //}
}