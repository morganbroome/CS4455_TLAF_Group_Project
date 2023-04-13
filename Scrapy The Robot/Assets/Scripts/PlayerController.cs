using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
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
    public Vector2 turn;
    public float smooth = 5.0f;

    private Vector2 movementVector;


    //passthroughs
    public GameObject gun;
    public GameObject pauseMenu;
    public GameObject lasertoggle;

    Gun gunScript;
    PauseMenuToggle pause;
    LaserToggle laser;

    //new camera stuff
    public Vector2 _move;
    public Vector2 _look;
    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float Camspeed = 1f;
    public Camera cam;
    public GameObject followTransform;
    public Slider slider;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chassisOffset = chassis.transform.position - transform.position;
        gunScript = gun.GetComponent<Gun>();
        pause = pauseMenu.GetComponent<PauseMenuToggle>();
        laser = lasertoggle.GetComponent<LaserToggle>();
    }

    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();




        //Vector3 horizontalMovement = movementVector.x * transform.right;
        //Vector3 forwardMovement = movementVector.y * transform.forward;
        //fullMovement = horizontalMovement + forwardMovement;
    }

    void OnJump()
    {
        if (IsGrounded)
        {
            //jump
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        }
        else if (!hasDoubleJumped)
        {
            //jump
            Vector3 currVelo = rb.velocity;
            rb.AddForce(new Vector3(0, jumpForce - currVelo.y, 0), ForceMode.VelocityChange);
            //particle effect

            //limit to one
            hasDoubleJumped = true;
        }
    }

    void OnFire()
    {
        gunScript.Fire();
    }

    void OnSecondary()
    {
        gunScript.Secondary();
    }

    void OnPause()
    {
        pause.Pause();
    }

    void OnLaser()
    {
        laser.Laser();
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 horizontalMovement = movementVector.x * transform.right;
        Vector3 forwardMovement = movementVector.y * transform.forward;
        fullMovement = horizontalMovement + forwardMovement;

        Vector3 movement = fullMovement;

        rb.AddForce(movement * speed, ForceMode.VelocityChange);

        //bool isGrounded = IsGrounded || CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        //turn.x += Input.GetAxis("Mouse X");
        //turn.y += Input.GetAxis("Mouse Y");
        //Quaternion target = Quaternion.Euler(0, turn.x, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        //New Camera stuff
        rotationPower = slider.value;

        #region Player Based Rotation

        //Move the player based on the X input on the controller
        transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
        #endregion
    }

    public Vector3 rocketshipPoint;
    public Vector3 wallPoint;
    public Vector3 originPoint;
    void Update()
    {
        chassis.transform.position = transform.position + chassisOffset;

        if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.B))
        {
            transform.position = rocketshipPoint; 
        }

        if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.N))
        {
            transform.position = wallPoint;
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.M))
        {
            transform.position = originPoint;
        }
    }

 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Ground")
        {
            ++groundContactCount;
            hasDoubleJumped = false;
        }

        if (collision.transform.gameObject.tag == "KillBox")
        {
            this.transform.position = new Vector3(0, 0.5f, 0);
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
