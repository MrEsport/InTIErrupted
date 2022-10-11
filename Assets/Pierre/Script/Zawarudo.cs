using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zawarudo : MonoBehaviour
{
    public float timer = 0.0f;
    public TextMeshProUGUI chronoCompteur;


    void Update()
    {
        timer += Time.deltaTime;
        chronoCompteur.text = timer.ToString();
    }
}
