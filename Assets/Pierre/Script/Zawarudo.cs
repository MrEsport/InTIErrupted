using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zawarudo : MonoBehaviour
{
    public float timer = 0;
    private float timerEvent = 0;
    private bool isCalculated = false;

    public TextMeshProUGUI chronoCompteur;
    public GameObject charaCall;

    void Start()
    {
        timerEvent = Random.Range(8, 12);

    }

    void Update()
    {
        timer += Time.deltaTime;
        chronoCompteur.text = timer.ToString();

        if (timer >= 5 && !isCalculated)
            isCalculated = true;

        if (timer >= timerEvent)
        {
            timer = 0;
            timerEvent = Random.Range(8, 12);
            isCalculated = false;
        }
    }
}
