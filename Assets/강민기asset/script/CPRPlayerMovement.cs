using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRPlayerMovement : MonoBehaviour
{
    private CPRPlayerAnimation pa;
    [SerializeField]
    private GameObject PlayerModel;
    [SerializeField]
    private float speed = 3;
    private Rigidbody rb;
    public CPRCameraMovement cmm;

    private const float RAY_DISTANCE = 2f;
    private RaycastHit slopeHit;
    private int groundlayer;

    private Vector3 moveDirection;

    void Start()
    {
        pa = GetComponentInChildren<CPRPlayerAnimation>();
        rb = GetComponentInChildren<Rigidbody>();
        //groundlayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (cmm.isMove)
        {
            //jump
            if (Input.GetKeyDown(KeyCode.Space) && !pa._isCPR)
            {
                pa._isjump = true;

                //ground check by raycast
                Ray ray = new Ray(transform.position, Vector3.down);
                if (Physics.Raycast(ray, 0.15f))
                {
                    rb.AddForce(new Vector3(0, 200, 0));
                }
            }
            else
            {
                pa._isjump = false;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            moveDirection = transform.TransformDirection(new Vector3(x, 0, z)); //local position

            //if (IsOnSlope())
            //{
            //    moveDirection = AdjustDirectionToSlope(moveDirection);
            //}

            if (moveDirection != Vector3.zero)
            {
                pa._iswalk = true;
                if (Input.GetKey(KeyCode.LeftShift)) //run
                {
                    pa._isrun = true;
                    speed = 7;
                }
                else
                {
                    pa._isrun = false;
                    speed = 3;
                }
            }
            else
            {
                pa._iswalk = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                pa._isCPR = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero && !pa._isCPR)
        {
            Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            //player model rotates to forward vectors y axis
            PlayerModel.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }

    public bool IsOnSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out slopeHit, RAY_DISTANCE, groundlayer))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle != 0f;
        }
        return false;
    }

    private Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void OnDisable()
    {
        pa._isjump = false;
        pa._isrun = false;
        pa._iswalk = false;
        pa._isCPR = false;
    }
}
