using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private const string IDLE = "Idle";
    private const string INTERACT = "Interact";
    private const string FALLING = "Falling";
    private const string JUMP = "Jump";
    private const string RUN = "Run";
    private const string WALKING = "Walking";
    public string currentState;

    public void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        anim.StopPlayback();
        anim.Play(newState);
        currentState = newState;
    }

    private void Start()
    {
        BasePlayer.OnPlayerWalking += BasePlayer_OnPlayerWalking;
        BasePlayer.OnPlayerIdle += BasePlayer_OnPlayerIdle;
        BasePlayer.OnPlayerRunning += BasePlayer_OnPlayerRunning;
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
