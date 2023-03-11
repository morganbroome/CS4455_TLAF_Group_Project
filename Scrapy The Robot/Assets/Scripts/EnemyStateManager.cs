using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase
    }
    public State state;
    public float range = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Patrol;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = State.Chase;
            GetComponent<SphereCollider>().radius *= range;
            Debug.Log("Player detected");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = State.Patrol;
            GetComponent<SphereCollider>().radius /= range;
            Debug.Log("Player lost");
        }
    }
}
