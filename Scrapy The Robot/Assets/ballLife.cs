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
}
