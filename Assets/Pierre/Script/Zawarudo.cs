using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class Zawarudo : MonoBehaviour
{
    public static bool stop = false;
    private float timer = 0;
    private float timerEvent = 0;
    public int waitingTime, aleaMin, aleaMax;

    //public TextMeshProUGUI chronoCompteur;
    public GameObject charaCall;
    private Caracters characters;

    private float timer2 = 0;
    private bool tokiwotomare = false;
    void Start()
    {
        timerEvent = Random.Range(aleaMin, aleaMax);
        characters = charaCall.GetComponent<Caracters>();
    }

    void Update()
    {
        if (stop)
            return;

        if (!tokiwotomare)
            timer += Time.deltaTime;
        //chronoCompteur.text = timer.ToString();

        timer2 += Time.deltaTime;

        if (timer2 >= waitingTime && tokiwotomare)
            tokiwotomare = false;

        if (timer >= timerEvent)
        {
            characters.CallToCome();
            timer = 0;
            timer2 = 0;
            timerEvent = Random.Range(aleaMin, aleaMax);
            tokiwotomare = true;
            /*Debug.Log("Time Stop");
            StartCoroutine(WaitChara());
            Debug.Log("Every go again");*/
        }
    }

    /*IEnumerator WaitChara()
    {
        characters.CallToCome();
        yield return new WaitForSeconds(12);
    }*/
}
