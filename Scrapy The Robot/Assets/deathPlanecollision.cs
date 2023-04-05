using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class deathPlanecollision : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {

        audioSource.PlayOneShot(clip);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
