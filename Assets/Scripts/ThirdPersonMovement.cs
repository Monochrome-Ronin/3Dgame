using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    FixedJoystick leftJoyStick;
    [SerializeField]
    Transform cam;

    [SerializeField]
    float walkSpeed = 3;
    [SerializeField]
    float sprintSpeed = 5;
    float realSpeed;
    bool sprining;

    float camTurnTime = .1f;
    float camTurnVelocity;
    CharacterController controller;
    Vector2 movement;

    float lastZ;
    bool movementUp;
    float jumpHeight = 2;
    float gravity = -9;
    bool isGrounded;
    Vector3 velocity;

    Animator anim;

    void Start()
    {

        realSpeed = walkSpeed;

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lastZ = transform.position.z;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, .1f, 1);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("movementUp", movementUp);
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //рух у просторі за допомагою лівого джойстика
        movement += new Vector2(leftJoyStick.Horizontal, leftJoyStick.Vertical);

        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            realSpeed = sprintSpeed;
            sprining = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            realSpeed = walkSpeed;
            sprining = false;
        }

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref camTurnVelocity, camTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 MoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(MoveDirection.normalized * realSpeed * Time.deltaTime);
            if(sprining == true)
            {
                anim.SetFloat("speed", 2);
            }
            else
            {
                anim.SetFloat("speed", 1);
            }
        }
        else
        {
            anim.SetFloat("speed", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -1);
        }

        if(transform.position.z - lastZ >= 0)
        {
            movementUp = false;
        }
        else
        {
            movementUp = true;
        }

        lastZ = transform.position.z;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
    }
}
