using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCharacter : CharacterBase
{
    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
