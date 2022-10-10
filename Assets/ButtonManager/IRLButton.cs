using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IRLButton
{
    public string name;
    [SerializeField] private KeyCode[] keys;

    public bool GetButton()
    {
        for (int i = 0; i < keys.Length; ++i)
            if (!Input.GetKey(keys[i])) return false;
        return true;
    }
}
