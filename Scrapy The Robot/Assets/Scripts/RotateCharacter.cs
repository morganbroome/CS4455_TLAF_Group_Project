using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    public Vector2 turn;
    public float smooth = 5.0f;

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y = 0;
        Quaternion target = Quaternion.Euler(0, turn.x, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
