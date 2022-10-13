using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class Zawarudo : MonoBehaviour
{
    private float timer = 0;
    private float timerEvent = 0;
    public int waitingTime, aleaMin, aleaMax;

    //public TextMeshProUGUI chronoCompteur;
    public GameObject charaCall;
    private Caracters characters;
    public GameObject soundCall;
    private SoundTransmitter soundToPlay;

    private float timer2 = 0;
    private bool door, tokiwotomare = false;
    void Start()
    {
        timerEvent = Random.Range(aleaMin, aleaMax);
        characters = charaCall.GetComponent<Caracters>();
        soundToPlay = soundCall.GetComponent<SoundTransmitter>();
    }

    void Update()
    {
        if (!tokiwotomare)
            timer += Time.deltaTime;
        //chronoCompteur.text = timer.ToString();

        timer2 += Time.deltaTime;

        if (timer2 >= waitingTime && tokiwotomare)
        {
            soundToPlay.Play("DoorClose");
            tokiwotomare = false;
        }

        if (timer2 >= 5 && door)
        {
            soundToPlay.Play("DoorOpen");
            door = false;
        }

        if (timer >= timerEvent)
        {
            characters.CallToCome();
            soundToPlay.Play("Knock123");
            timer = 0;
            timer2 = 0;
            timerEvent = Random.Range(aleaMin, aleaMax);
            tokiwotomare = true;
            door = true;
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
