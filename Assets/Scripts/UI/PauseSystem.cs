using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;

    private void Start()
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                pauseMenu.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1f;
            }else if (Time.timeScale == 1f)
            {
                pauseMenu.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0f;
            }
        }

    }

}
