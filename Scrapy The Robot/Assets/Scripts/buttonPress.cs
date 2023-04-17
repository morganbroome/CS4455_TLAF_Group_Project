using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPress : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public AudioSource audioSource;
    public AudioClip clip;
    public int counter = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (counter > 0)
        {
            door1.SetActive(false);
            door2.SetActive(false);
            audioSource.PlayOneShot(clip);
            counter -= 1;
        }

    }
}
