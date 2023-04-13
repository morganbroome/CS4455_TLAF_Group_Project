using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPress : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    private void OnCollisionEnter(Collision collision)
    {
        door1.SetActive(false);
        door2.SetActive(false);

    }
}
