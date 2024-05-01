using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGMOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _BGMToPlay;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(_BGMToPlay, 2f);
    }
}
