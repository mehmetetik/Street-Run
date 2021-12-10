using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed;
    private int desiredLane = 1;
    private float laneDistance = 3.5f;
    private CharacterController controller;

    public float transitionSpeed;

    public float jumpForce;

    public float Gravity = -20;
    private Vector3 direction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(!GameCtrl.isGameStarting)
            return;

        direction.z = forwardSpeed;

        direction.y += Gravity * Time.deltaTime;

        controller.Move(direction * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (SwipeCtrl.swipeUp)
            {
                Jump();
            }
        }

        if (SwipeCtrl.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeCtrl.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
        
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}

