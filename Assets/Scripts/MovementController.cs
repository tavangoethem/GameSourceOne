using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _movementSpeed;
    [SerializeField, Min(1)] private float _jumpSpeed;

    private CharacterController _characterController;
    private CapsuleCollider _capsuleCollider;

    [SerializeField]private bool crouchingIsBlocked = false;

    Vector3 velocity = new Vector3();
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void UpdateMovement(Vector2 movementDelta)
    {
        movementDelta = movementDelta * _movementSpeed;
        velocity = new Vector3(movementDelta.x, Physics.gravity.y, movementDelta.y);
        velocity = transform.rotation * velocity;
        _characterController?.Move(velocity * Time.deltaTime);
    }

    public void jumpNow()
    {
        velocity.y += Mathf.Sqrt(-3.0f * Physics.gravity.y * _jumpSpeed);
        _characterController?.Move(velocity * Time.deltaTime);
    }

    public void StartCrouchControl()
    {
        _capsuleCollider.height = .5f;
        _capsuleCollider.center = new Vector3(0,-0.5f,0);
        _characterController.height = 0.5f;
        _characterController.radius = .25f;
        _movementSpeed = _movementSpeed / 2f;
    }
    public void AttemptExitCrouch()
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