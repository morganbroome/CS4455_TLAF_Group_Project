using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLife : MonoBehaviour
{
    public float life = 15;
    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = new Vector3 (0,0,0) ;
            //Destroy(collision.gameObject);
        }
        
    }
}
