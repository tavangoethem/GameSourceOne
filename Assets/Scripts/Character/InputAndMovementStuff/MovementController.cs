using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _baseMovementSpeed = 10;
    [SerializeField, Min(1)] private float _slowMovementSpeed = 5;
    [SerializeField, Min(1)] private float _jumpStrength;

    [SerializeField] private LayerMask _outsideLayer;
    [SerializeField] private LayerMask _insideLayer;
    private LayerMask _tempMask;

    private CharacterController _characterController;
    private CapsuleCollider _capsuleCollider;
    [SerializeField] private float _verticalVelocity = 0f;

    [SerializeField]private bool crouchingIsBlocked = false;

    Vector3 velocity = new Vector3();

    public bool canCrouch = false;

    public AudioSource crouchingClip;
    public AudioSource indoorClip;
    public AudioSource outdoorClip;

    private const string SFXPREFSNAME = "SFXVolume";
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
            if (((Mathf.Abs(velocity.x) > 2f) || Mathf.Abs(velocity.z) > 2f) && (indoorClip != null || outdoorClip != null))
            {
                _tempMask = (_tempMask | (1 << hit.transform.gameObject.layer));
                //(mask | (1 << layer));
                if (_insideLayer == _tempMask && !indoorClip.isPlaying)
                {
                    indoorClip.volume = PlayerPrefs.GetFloat(SFXPREFSNAME);
                    indoorClip.Play();
                }
                else if (_outsideLayer == _tempMask && !outdoorClip.isPlaying)
                {

                    outdoorClip.volume = PlayerPrefs.GetFloat(SFXPREFSNAME);
                    outdoorClip.Play();
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
            if (crouchingClip != null && !crouchingClip.isPlaying)
            {
                crouchingClip.volume = PlayerPrefs.GetFloat(SFXPREFSNAME);
                crouchingClip.Play();
            }
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
                if (crouchingClip != null && !crouchingClip.isPlaying)
                    crouchingClip.Play();
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