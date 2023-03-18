using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // This is for event handling. It is basically an implementations of the observer pattern. Any gameobject tagged as "Collectable"
    // will follow the smae behavior pattern and this elps to centarlize tasks like updating the UI and playing sounds.
    // Later on we can pass a gomeobject to the delegate so that we can be more specific about the event and event handling.
    public delegate void CollectableDelegate();
    public static event CollectableDelegate onCollectablePickup;
    
    public GameObject particles;
    //public AudioSource audioSource;
    //public AudioClip clip;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 50 * Random.Range(1, 3), 0.0f) * Time.deltaTime);
    }

    public void HandleCollectablePickup()
    {
       
        onCollectablePickup?.Invoke();
        Instantiate(particles, transform.position, Quaternion.identity);
       


    }

 
}
