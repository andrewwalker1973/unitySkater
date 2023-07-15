using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;


    // Collider Logic
    private Vector3 initalCenter;
    private float initialSize;
    private float slideStart;


    public override void Construct()
    {
        motor.anim?.SetTrigger("Slide");
        slideStart = Time.time;


        initialSize = motor.controller.height;
        initalCenter = motor.controller.center;


        motor.controller.height = initialSize * 0.5f;
        motor.controller.center = initalCenter * 0.5f;

    }

    public override void Destruct()
    {
        
        motor.controller.height = initialSize;
        motor.controller.center = initalCenter;
        motor.anim?.SetTrigger("Running");


    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }

        if (!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());

        if (InputManager.Instance.SwipeUp)
            motor.ChangeState(GetComponent<JumpingState>());

        if (Time.time - slideStart > slideDuration)
            motor.ChangeState(GetComponent<RunningState>());

    }

    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed;

        return m;
    }
}
