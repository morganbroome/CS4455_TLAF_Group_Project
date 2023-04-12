using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVertical : MonoBehaviour
{
    public Vector2 turn;
    public float smooth = 5.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        Quaternion target = Quaternion.Euler(-turn.y, turn.x, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
