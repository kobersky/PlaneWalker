using UnityEngine;

/* represents state of a player (character) when idle (not moving).*/
public class PlayerIdleState : IPlayerState
{
    private Animator _animator;
    private Rigidbody _rigidBody;

    public void OnEnter(PlayerController controller)
    {
        Debug.Log("KK: Enter IDLE");

        _animator = controller.GetComponent<Animator>();
        _rigidBody = controller.GetComponent<Rigidbody>();

    }

    public void OnFixedUpdate(PlayerController controller)
    {
        var verticalSpeed = _rigidBody.velocity.y;

/*        if (verticalSpeed > 0 && !controller._groundDetector.IsGrounded)
        {
            controller.ChangeState(controller._playerJumpState);
        }
        else */if (Mathf.Abs(controller.Movement.x) > 0 ||
            Mathf.Abs(controller.Movement.z) > 0)
        {
            controller.ChangeState(controller._playerRunState);
        }


    }

    public void OnExit(PlayerController controller)
    {
        _animator.SetBool(AnimationKeys.PLAYER_RUN, false);

    }
}
