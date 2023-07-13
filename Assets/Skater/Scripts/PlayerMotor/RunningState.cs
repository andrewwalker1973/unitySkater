using UnityEngine;

public class RunningState : BaseState
{
    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed;

        return m;

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

          if (InputManager.Instance.SwipeUp && motor.isGrounded)
          {
              // motot.ChangeState(GetComponent<JumpingState>());
          }

          if (InputManager.Instance.SwipeDown)
          {

          }
    
     }


}
