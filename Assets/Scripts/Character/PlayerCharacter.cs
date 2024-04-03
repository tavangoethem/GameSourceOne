using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCharacter : CharacterBase
{
    public static PlayerCharacter Instance;

    public override void Awake()
    {
        base.Awake();
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
