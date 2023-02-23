using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using CS4455.Utility;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Vector3 chassisOffset;
    private int groundContactCount = 0;
    private bool hasDoubleJumped;

    public float speed = 0;
    public float jumpForce = 0;
    public GameObject chassis;
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;
    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    public Vector3 fullMovement;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chassisOffset = chassis.transform.position - transform.position;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        Vector3 horizontalMovement = movementVector.x * transform.right;
        Vector3 forwardMovement = movementVector.y * transform.forward;
        fullMovement = horizontalMovement + forwardMovement;
    }

    void OnJump()
    {
        if(IsGrounded)
        {
            //jump
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        } 
        else if(!hasDoubleJumped)
        {
            //jump
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            //particle effect

            //limit to one
            hasDoubleJumped = true;
        }
    }

    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        Vector3 movement = fullMovement;

        rb.AddForce(movement * speed, ForceMode.VelocityChange);

        bool isGrounded = IsGrounded || CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
    }

    void Update()
    {
        chassis.transform.position = transform.position + chassisOffset;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Ground")
        {
            ++groundContactCount;
            hasDoubleJumped = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Ground")
        {
            --groundContactCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.GetComponent<Collectable>().HandleCollectablePickup();
            other.gameObject.SetActive(false);
        }
    }

    public static bool CheckGroundNear(
        Vector3 charPos,
        float jumpableGroundNormalMaxAngle,
        float rayDepth, //how far down from charPos will we look for ground?
        float rayOriginOffset, //charPos near bottom of collider, so need a fudge factor up away from there
        out bool isJumpable
    )
    {

        bool ret = false;
        bool _isJumpable = false;


        float totalRayLen = rayOriginOffset + rayDepth;

        Ray ray = new Ray(charPos + Vector3.up * rayOriginOffset, Vector3.down);

        int layerMask = 1 << LayerMask.NameToLayer("Default");


        RaycastHit[] hits = Physics.RaycastAll(ray, totalRayLen, layerMask);

        RaycastHit groundHit = new RaycastHit();

        foreach (RaycastHit hit in hits)
        {

            if (hit.collider.gameObject.CompareTag("Ground"))
            {

                ret = true;

                groundHit = hit;

                _isJumpable = Vector3.Angle(Vector3.up, hit.normal) < jumpableGroundNormalMaxAngle;

                break; //only need to find the ground onc

            }

        }

        Helper.DrawRay(ray, totalRayLen, hits.Length > 0, groundHit, Color.magenta, Color.green);

        isJumpable = _isJumpable;

        return ret;
    }
}
