using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField, Range(0, 20)] private float _sensitivty = 5f;
    [SerializeField] private float _verticalRange = 60f;

    public float Sens { get { return _sensitivty; } set { _sensitivty = value; } }

    private float _currentVerticalRotation = 0f;
    private Camera _camera;
    [SerializeField] private Transform _body;

    private const string SENSPREFSNAME = "Sens";

    private void Awake()
    {
        _camera = Camera.main;
        //_body = transform.parent;
        _sensitivty = PlayerPrefs.GetFloat(SENSPREFSNAME, 500f) / 100f;
    }

    public void Update()
    {
        if (_sensitivty != PlayerPrefs.GetFloat(SENSPREFSNAME, 500f) / 100f)
        {
            _sensitivty = PlayerPrefs.GetFloat(SENSPREFSNAME, 500f) / 100f;
        }
    }

    public void UpdateRotation(Vector2 viewDelta)
    {
        _body.Rotate(0f, viewDelta.x * _sensitivty / 20f, 0f);

        _currentVerticalRotation -= viewDelta.y * _sensitivty / 20f;
        _currentVerticalRotation = Mathf.Clamp(_currentVerticalRotation, -_verticalRange, _verticalRange);
        _camera.transform.localRotation = Quaternion.Euler(_currentVerticalRotation, 0f, 0f);
    }
}
