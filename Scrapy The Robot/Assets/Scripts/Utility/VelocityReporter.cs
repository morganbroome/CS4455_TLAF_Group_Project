using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VelocityReporter : MonoBehaviour
{
    private Vector3 prevPos;
    public Vector3 rawVelocity
    {
        get;
        private set;
    }
    public Vector3 velocity
    {
        get;
        private set;
    }

    private Vector3 currDir;
    public Vector3 direction
    {
        get
        {
            return currDir;
        }
        set
        {
            currDir.x = (value.x > 0) ? 1 : (value.x < 0) ? -1 : 0;
            currDir.y = (value.y > 0) ? 1 : (value.y < 0) ? -1 : 0;
            currDir.z = (value.z > 0) ? 1 : (value.z < 0) ? -1 : 0;
        }
    }

    public float smoothingTimeFactor = 0.5f;
    private Vector3 smoothingParamVel;

    // For debugging
    public bool debugFlag = false;

    void Start()
    {
        prevPos = this.transform.position;
    }

    void Update()
    {
        this.direction = this.transform.position - prevPos;
        if (!Mathf.Approximately(Time.deltaTime, 0f))
        {
            rawVelocity = (this.transform.position - prevPos) / Time.deltaTime;
            velocity = Vector3.SmoothDamp(velocity, rawVelocity, ref smoothingParamVel, smoothingTimeFactor);

            // Debugging
            if (debugFlag)
            {
                Debug.Log("Current position: " + this.transform.position + "\nPrevious position: " + prevPos
                            + "\nTime: " + Time.deltaTime + "\nDifference: " + (this.transform.position - prevPos)
                            + "\nVelocity: " + velocity + "\nRaw velocity: " + rawVelocity);
            }
        }
        else
        {
            rawVelocity = Vector3.zero;
            velocity = Vector3.zero;
        }
        prevPos = this.transform.position;
    }
}
