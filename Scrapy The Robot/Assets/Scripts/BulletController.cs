using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float life = 3;
    public float delayTime = 4.0f; // for enemies
    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && collision.collider.GetType() == typeof(BoxCollider))
        {
            print("yolo");
            StartCoroutine(DisableAndDestroyCoroutine(collision.gameObject));
            //Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private IEnumerator DisableAndDestroyCoroutine(GameObject myGameObject)
    {
        // Disable the game object
        myGameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        myGameObject.GetComponent<BoxCollider>().enabled = false;
        myGameObject.GetComponent<SphereCollider>().enabled = false;
        myGameObject.GetComponent<EnemyStateManager>().playDeathSound();
        //myGameObject.SetActive(false);

        // Wait for a few seconds
        yield return new WaitForSeconds(delayTime);

        // Destroy the game object
        Destroy(myGameObject);
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