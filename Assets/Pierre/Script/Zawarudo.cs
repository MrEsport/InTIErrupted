using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zawarudo : MonoBehaviour
{
    public int timer = 0;
    public TextMeshProUGUI chronoCompteur;


    void Update()
    {
        timer += Time.deltaTime;
        chronoCompteur.text = timer.ToString();
    }
}
