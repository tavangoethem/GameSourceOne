using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _movementSpeed;
    [SerializeField, Min(1)] private float _jumpStrength;

    private CharacterController _characterController;
    private CapsuleCollider _capsuleCollider;
    [SerializeField] private float _verticalVelocity = 0f;

    [SerializeField]private bool crouchingIsBlocked = false;

    Vector3 velocity = new Vector3();

    public bool canCrouch = false;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void UpdateMovement(Vector2 movementDelta)
    {
        movementDelta = movementDelta * _movementSpeed;
        velocity = new Vector3(movementDelta.x, _verticalVelocity, movementDelta.y);
        velocity = transform.rotation * velocity;
        _characterController?.Move(velocity * Time.deltaTime);

        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        _verticalVelocity = Mathf.Clamp(_verticalVelocity, Physics.gravity.y, Mathf.Infinity);
    }

    public void jumpNow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
        {
            _verticalVelocity = _jumpStrength;
        }
    }

    public void StartCrouchControl()
    {
        if (canCrouch == true)
        {
            _capsuleCollider.height = .5f;
            _capsuleCollider.center = new Vector3(0, -0.5f, 0);
            _characterController.height = 0.5f;
            _characterController.radius = .25f;
            _movementSpeed = _movementSpeed / 2f;
        }
    }
    public void AttemptExitCrouch()
    {
        if (canCrouch == true || _capsuleCollider.height == .5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 1.5f) == false)
            {
                _capsuleCollider.height = 1f;
                _capsuleCollider.center = new Vector3(0, 0, 0);
                _characterController.height = 2f;
                _characterController.radius = .5f;
                _movementSpeed = _movementSpeed * 2f;
            }
            else
            {
                StartCoroutine(CheckIfUncrouch());
            }
        }            
    }

    private IEnumerator CheckIfUncrouch()
    {
        RaycastHit hit;
        while (Physics.Raycast(transform.position, transform.up, out hit, 1.5f))
        {
            print(hit.transform.name);

            yield return null;
        }
        AttemptExitCrouch();
    }
}