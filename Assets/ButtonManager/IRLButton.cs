using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IRLButton
{
    public EButton group;
    public int count;
    [SerializeField] private KeyCode[] keys;

    public bool GetButton()
    {
        for (int i = 0; i < keys.Length; ++i)
            if (!Input.GetKey(keys[i])) return false;
        return true;
    }

    public void ResetCount() => count = 0;
    public void Increment() => count++;
}

public struct PushedButton
{
    public EButton button;
    public int count;
}
