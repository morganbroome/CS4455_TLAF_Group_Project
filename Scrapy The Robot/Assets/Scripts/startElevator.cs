using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startElevator : MonoBehaviour
{
    public Animation anim;
    public GameObject holding;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}


    //private void OnCollisionEnter(Collision collision)
    //{
    //    anim.Play("elevate");
    //}

    //public GameObject movePlatform;
    //public GameObject character;

    //private void OnTriggerStay(Collider other)
    //{
    //    movePlatform.transform.position += movePlatform.transform.up * Time.deltaTime;
    //    //character.transform.position += character.transform.up * Time.deltaTime;
    //    other.transform.SetParent(transform);
    //}
    public float animSpeed = 0.3f;
    private void OnCollisionEnter(Collision collision)
    {
        anim.Play("newElevate");
        anim["newElevate"].speed = animSpeed;
        collision.transform.SetParent(holding.transform);

    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}