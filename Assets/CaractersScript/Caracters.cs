using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// struct par personnage (bruit / delai / Texte / etat ?) || POO

public class Caracters : MonoBehaviour
{
    private AudioSource _noise;
    
    [SerializeField]
    private int delay = 5;
    [SerializeField]
    private int delayToDesapers = 5;

    private List<string> _text;

    enum State
    {
        Normal,
        Sus,
        Detect
    }

    [SerializeField]
    private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Normal);
        
        _noise = GetComponent<AudioSource>();
        
        _noise.Play();
        
        StartCoroutine(ComeToRoom());
    }

    private void ChangeState(State value)
    {
        state = value;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ComeToRoom()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Caracter in the room !");

        ChangeState(State.Sus);
        // ChangeState(State.detect);

        if (state != State.Detect)
            StartCoroutine(Desapers());
        else
            Debug.Log("Game over");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Desapers()
    {
        Debug.Log("Caracter move back");
        
        yield return new WaitForSeconds(delayToDesapers);

        Debug.Log("Destroy caracters");
        Destroy(this);
    }
}
