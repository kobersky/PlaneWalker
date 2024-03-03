using UnityEngine;

/* represents state of a player (character) during running.*/
public class PlayerRunState : IPlayerState
{
    private Animator _animator;
    private Rigidbody _rigidBody;

    public void OnEnter(PlayerController controller)
    {
        Debug.Log("KK: Enter RUN");
        _animator = controller.GetComponent<Animator>();
        _rigidBody = controller.GetComponent<Rigidbody>();

        _animator.SetBool(AnimationKeys.PLAYER_RUN, true);
    }

    public void OnFixedUpdate(PlayerController controller)
    {
        _rigidBody.AddForce(controller.Movement * controller.PlayerParams.CharacterSpeed * Time.fixedDeltaTime);
        controller.transform.forward = controller.Rotation;

        if (Mathf.Abs(controller.Movement.x) == 0 &&
            Mathf.Abs(controller.Movement.z) == 0)
        {
            controller.ChangeState(controller._playerIdleState);
        }
    }

    public void OnExit(PlayerController controller)
    {
        _animator.SetBool(AnimationKeys.PLAYER_RUN, false);
    }
}

