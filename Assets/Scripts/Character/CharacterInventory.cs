using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private Keys _curKeys;

    public Keys CurKeys { get { return _curKeys; } }

    public void UpdateKeys(Keys keysToAdd)
    {
        if (_curKeys.HasFlag(keysToAdd) == true)
            return;
        _curKeys += (int)keysToAdd;
    }
}
