using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _baseMovementSpeed = 10;
    [SerializeField, Min(1)] private float _slowMovementSpeed = 5;
    [SerializeField, Min(1)] private float _jumpStrength;

    private CharacterController _characterController;
    private CapsuleCollider _capsuleCollider;
    [SerializeField] private float _verticalVelocity = 0f;

    [SerializeField]private bool crouchingIsBlocked = false;

    Vector3 velocity = new Vector3();

    public bool canCrouch = false;

    public AudioManager audioManager;
    public AudioClip crouchingClip;
    public AudioClip indoorClip;
    public AudioClip outdoorClip;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void UpdateMovement(Vector2 movementDelta)
    {
        if (_capsuleCollider.height == 1f)
            movementDelta = movementDelta * _baseMovementSpeed;
        else
            movementDelta = movementDelta * _slowMovementSpeed;

        velocity = new Vector3(movementDelta.x, _verticalVelocity, movementDelta.y);
        velocity = transform.rotation * velocity;
        _characterController?.Move(velocity * Time.deltaTime);

        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        _verticalVelocity = Mathf.Clamp(_verticalVelocity, Physics.gravity.y, Mathf.Infinity);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
        {
            if (velocity.magnitude > 0.01f && audioManager != null)
            {
                if (hit.rigidbody.gameObject.layer.ToString() == "Indoors")
                {
                    audioManager?.PlaySFX(indoorClip);
                }
                else if (hit.rigidbody.gameObject.layer.ToString() == "Outdoors")
                {
                    audioManager?.PlaySFX(outdoorClip);
                }
            }
        }
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
        if (canCrouch == true && _capsuleCollider.height == 1f)
        {
            if (audioManager != null)
                audioManager?.PlaySFX(crouchingClip);
            _capsuleCollider.height = .5f;
            _capsuleCollider.center = new Vector3(0, -0.5f, 0);
            _characterController.height = 0.5f;
            _characterController.radius = .25f;
        }
    }
    public void AttemptExitCrouch()
    {
        if (canCrouch == true && _capsuleCollider.height == .5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 1.5f) == false)
            {
                audioManager?.PlaySFX(crouchingClip);
                _capsuleCollider.height = 1f;
                _capsuleCollider.center = new Vector3(0, 0, 0);
                _characterController.height = 2f;
                _characterController.radius = .5f;
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