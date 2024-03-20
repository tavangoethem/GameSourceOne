using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    [SerializeField] private bool LockOff;
    void Start()
    {
        if (LockOff == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        if (LockOff == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
