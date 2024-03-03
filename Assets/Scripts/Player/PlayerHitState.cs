using UnityEngine;

/* represents state of a player (character) when getting hit.*/
public class PlayerHitState : IPlayerState
{
    private Animator _animator;
    private Rigidbody _rigidBody;

    private const float HIT_FACTOR = 0.62f;

    private int RandomDirection => Mathf.RoundToInt(Random.Range(0f, 1f)) * 2 - 1;
    public void OnEnter(PlayerController controller)
    {
        _animator = controller.GetComponent<Animator>();
        _rigidBody = controller.GetComponent<Rigidbody>();

        Bounce(controller);

        _animator.SetBool(AnimationKeys.PLAYER_HIT, true);
    }

    public void OnFixedUpdate(PlayerController controller)
    {
        var verticalSpeed = _rigidBody.velocity.y;

        if (verticalSpeed < 0 && controller._groundDetector.IsGrounded)
        {
            _animator.SetBool(AnimationKeys.PLAYER_HIT, false);
            controller.ChangeState(controller._playerIdleState);
        }
    }
    public void OnExit(PlayerController controller)
    {

    }

    private void Bounce(PlayerController controller)
    {
        _rigidBody.velocity= Vector3.zero;
        var hitForce = new Vector3(RandomDirection * HIT_FACTOR * controller.PlayerParams.CharacterHitForce,
                controller.PlayerParams.CharacterJumpForce,
                RandomDirection * HIT_FACTOR * controller.PlayerParams.CharacterHitForce);
              
        _rigidBody.AddForce(hitForce);
    }
}
