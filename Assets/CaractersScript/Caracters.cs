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
    private int delayToDisappear = 5;

    [SerializeField]
    private List<string> _text;

    enum State
    {
        Normal,
        Sus,
        Detect
    }

    [SerializeField]
    private State state;

    private int _textIndex;
    
    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(State.Normal);
        
        _noise = GetComponent<AudioSource>();
        
        //CallToCome();
    }

    private void CallToCome()
    {
        _textIndex = Random.Range(0, _text.Count);
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

        Debug.Log($"{_text[_textIndex]}");
        ChangeState(State.Sus);
        // ChangeState(State.detect);

        if (state != State.Detect)
            StartCoroutine(Disappear());
        else
            Debug.Log("Game over");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Disappear()
    {
        Debug.Log("Caracter move back");
        
        yield return new WaitForSeconds(delayToDisappear);

        Debug.Log("disappear caracters");
        Destroy(this);
    }
}
