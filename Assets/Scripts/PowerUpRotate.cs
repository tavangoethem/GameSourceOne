using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PowerUpRotate : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 2.0f;
    void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);       
    }
}
