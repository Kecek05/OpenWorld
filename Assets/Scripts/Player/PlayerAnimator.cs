using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //Hashed Parameters for performance
    private static readonly int IDLE = Animator.StringToHash("Idle");
    private static readonly int INTERACT = Animator.StringToHash("Interact");
    private static readonly int FALLING = Animator.StringToHash("Falling");
    private static readonly int JUMP = Animator.StringToHash("Jump");
    private static readonly int RUN = Animator.StringToHash("Run");
    private static readonly int WALKING = Animator.StringToHash("Walking");

    private int currentState;

    public void ChangeAnimationState(int newState)
    {
        if(currentState == newState) return;
        anim.StopPlayback();

        if (currentState == IDLE) // if its in idle, fade faster
            anim.CrossFade(newState, 0.05f);
        else
            anim.CrossFade(newState, 0.2f);
        currentState = newState;
    }

    public void PlayInteractAnim()
    {
        anim.CrossFade(INTERACT, 0.1f, 1);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayInteractAnim();
        }
    }

    private void Start()
    {
        BasePlayer.OnPlayerWalking += BasePlayer_OnPlayerWalking;
        BasePlayer.OnPlayerIdle += BasePlayer_OnPlayerIdle;
        BasePlayer.OnPlayerRunning += BasePlayer_OnPlayerRunning;
        BasePlayer.OnPlayerJumping += BasePlayer_OnPlayerJumping;
        BasePlayer.OnPlayerFalling += BasePlayer_OnPlayerFalling;
    }

    private void BasePlayer_OnPlayerFalling()
    {
        ChangeAnimationState(FALLING);
    }

    private void BasePlayer_OnPlayerJumping()
    {
        ChangeAnimationState(JUMP);
    }

    private void BasePlayer_OnPlayerRunning()
    {
        ChangeAnimationState(RUN);
    }

    private void BasePlayer_OnPlayerIdle()
    {
        ChangeAnimationState(IDLE);
    }

    private void BasePlayer_OnPlayerWalking()
    {
        ChangeAnimationState(WALKING);
    }
}
