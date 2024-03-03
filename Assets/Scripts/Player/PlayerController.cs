using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* controls input for the player, 
 * manages player states, 
 * reacts to events affecting the player*/
public class PlayerController : MonoBehaviour
{
    [SerializeField] public GroundDetector _groundDetector;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidBody;

    public PlayerParams PlayerParams;

    public Vector3 Movement { get; private set; }
    public Vector3 Rotation { get; private set; }

    public static event Action OnResourceCollected;
    public static event Action OnVitalItemCollected;
    public static event Action OnHazardItemTriggered;
    public static event Action OnExitItemTriggered;

    public IPlayerState _currentState;
    public PlayerIdleState _playerIdleState = new PlayerIdleState();
    public PlayerRunState _playerRunState = new PlayerRunState();
    public PlayerJumpState _playerJumpState = new PlayerJumpState();
    public PlayerHitState _playerHitState = new PlayerHitState();

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = new InputManager();
    }

    private void OnEnable()
    {
        SubscribeToPlayerInput();
        ResetMovementAndRotation();
        ChangeState(_playerIdleState);
    }

    private void ResetMovementAndRotation()
    {
        Movement = Vector3.zero;
        Rotation = Vector3.zero;
    }

    private void OnDisable()
    {
        UnsubscribeToPlayerInput();
    }

    void FixedUpdate()
    {
        _currentState.OnFixedUpdate(this);
    }

    public void ChangeState(IPlayerState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }
        _currentState = newState;
        _currentState.OnEnter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleItemInteraction(other);
    }

    private static void HandleItemInteraction(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            var itemType = interactable.GetItemType();
            switch (itemType)
            {
                case ItemType.ResourceItem:
                    OnResourceCollected?.Invoke();
                    break;
                case ItemType.VitalItem:
                    OnVitalItemCollected?.Invoke();
                    break;
                case ItemType.HazardItem:
                    OnHazardItemTriggered?.Invoke();
                    break;
                case ItemType.ExitItem:
                    OnExitItemTriggered?.Invoke();
                    break;
            }
        }
    }

    private void OnInputMovementPerformed(InputAction.CallbackContext callBackContext)
    {
        var (cameraForward, cameraRight) = CameraManager.Instance.GetNormalizedCameraVectors();

        var input = callBackContext.ReadValue<Vector2>();
        var moveDirection = new Vector3(input.x, 0, input.y);
        Movement = (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;
        Rotation = Movement; 
    }

    private void OnInputMovementCanceled(InputAction.CallbackContext callBackContext)
    {
        Movement = Vector3.zero;
    }

    private void OnInputJumpPerformed(InputAction.CallbackContext callBackContext)
    {
        if (_groundDetector.IsGrounded)
        {
            //snap to ground
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
            ChangeState(_playerJumpState);
        }
    }

    public void GetHit()
    {
        ChangeState(_playerHitState);
    }

    private void SubscribeToPlayerInput()
    {
        _inputManager.Enable();
        _inputManager.Player.Move.performed += OnInputMovementPerformed;
        _inputManager.Player.Move.canceled += OnInputMovementCanceled;
        _inputManager.Player.Jump.performed += OnInputJumpPerformed;
    }

    private void UnsubscribeToPlayerInput()
    {
        _inputManager.Player.Move.performed -= OnInputMovementPerformed;
        _inputManager.Player.Move.canceled -= OnInputMovementCanceled;
        _inputManager.Player.Jump.performed -= OnInputJumpPerformed;
        _inputManager.Disable();
    }
}
