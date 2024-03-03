using UnityEngine;

/* represents state of a player (character) during jump.*/
public class PlayerJumpState : IPlayerState
{
    private Animator _animator;
    private Rigidbody _rigidBody;

    public void OnEnter(PlayerController controller)
    {
        Debug.Log("KK: Enter JUMP");

        _animator = controller.GetComponent<Animator>();
        _rigidBody = controller.GetComponent<Rigidbody>();

        _rigidBody.AddForce(new Vector3(0, controller.PlayerParams.CharacterJumpForce, 0));
        _animator.SetBool(AnimationKeys.PLAYER_JUMP, true);
    }
    public void OnFixedUpdate(PlayerController controller)
    {
        _rigidBody.AddForce(controller.Movement * controller.PlayerParams.CharacterSpeed * Time.fixedDeltaTime);
        var verticalSpeed = _rigidBody.velocity.y;

        controller.transform.forward = controller.Rotation;

        if (verticalSpeed < 0 && controller._groundDetector.IsGrounded)
        {
            controller.ChangeState(controller._playerIdleState);
        }
    }

    public void OnExit(PlayerController controller)
    {
        _animator.SetBool(AnimationKeys.PLAYER_JUMP, false);
    }
}

