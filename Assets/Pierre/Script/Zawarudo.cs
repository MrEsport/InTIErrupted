using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zawarudo : MonoBehaviour
{
    public float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            CheckButtons();
            timer = 0;
        }
    }
}
