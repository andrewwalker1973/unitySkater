using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;


    public CharacterController controller;
    public Animator anim;
    private BaseState state;
    private bool isPaused;
    public AudioClip hitSound;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        state = GetComponent<RunningState>();
        state.Construct();

        isPaused = true;
    }

    private void Update()
    {
        if(!isPaused)
            UpdateMotor();
    }

    private void UpdateMotor()
    {
        // Check if we are grounded
        isGrounded = controller.isGrounded;


        // How should we be moving
        moveVector = state.ProcessMotion();


        // are we trying to change state
        state.Transition();

        // feed animator values
        anim?.SetBool("IsGrounded", isGrounded);
        anim?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        // Move Player
        controller.Move(moveVector * Time.deltaTime);


    }

    public float SnapToLane()
    {
        float r = 0.0f;

        if (transform.position.x != (currentLane * distanceInBetweenLanes))  // if we not ina lane
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
        }
        else
        {
            r = 0;
        }


        return r;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    

    public void PausePlayer()
    {
        isPaused = true;
    }

    public void ResumePlayer()
    {
        isPaused = false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName == "Death")
        {
            AudioManager.Instance.PlaySFX(hitSound, 0.7f);
            ChangeState(GetComponent<DeathState>());
        }
    }


    public void RespawnPlayer()
    {

           ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Respawn);
        
    }

    public void ResetPlayer()
    {
        currentLane = 0;
        transform.position = Vector3.zero;
        anim?.SetTrigger("Idle");
        PausePlayer();
        ChangeState(GetComponent<RunningState>());
    }


}
