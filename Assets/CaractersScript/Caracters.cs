using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// lerp appear and disappear (change a)

public class Caracters : MonoBehaviour
{
    private AudioSource _noise;

    [SerializeField]
    private float delay = 5;
    [SerializeField]
    private float delayToDisappear = 5;
    [SerializeField]
    private float delayForSayingSomething = 0.2f;

    [SerializeField] 
    private TextData textData;

    [SerializeField] 
    private bool isCat;

    public enum State
    {
        Normal,
        Sus,
        Detect
    }

    [SerializeField]
    private State state;

    private int _indexWhenCome;
    private int _indexWhenNothingSus;
    private int _indexWhenSus;
    private int _indexWhenDetectSomething;
    
    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(State.Normal);
        
        _noise = GetComponent<AudioSource>();
        
        CallToCome();
    }

    public void CallToCome()
    {
        _indexWhenCome = Random.Range(0, textData.GetTextWhenCome().Count);
        _indexWhenNothingSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenDetectSomething = Random.Range(0, textData.GetTextWhenDetectSomething().Count);
        
        _noise.Play();
        
        StartCoroutine(ComeToRoom());
    }

    public void ChangeState(State value)
    {
        state = value;
    }

    public bool IsCat()
    {
        return isCat;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ComeToRoom()
    {
        // lerp fade in
        
        yield return new WaitForSeconds(delay);

        Debug.Log("<color=green>Character in the room !</color>");

        Debug.Log($"{textData.GetTextWhenCome()[_indexWhenCome]}");
        
        // ChangeState(State.Sus);
        // ChangeState(State.Detect);

        // yield return new WaitForSeconds(5);
        
        switch (state)
        {
            case State.Detect:
            {
                StartCoroutine(SayText(textData.GetTextWhenDetectSomething()[_indexWhenDetectSomething], delayForSayingSomething));
                Debug.Log("<color=red>Game over</color>");
                break;
            }
            case State.Normal:
            {
                StartCoroutine(SayText(textData.GetTextWhenNothingSus()[_indexWhenNothingSus], delayForSayingSomething));
                StartCoroutine(Disappear());
                break;
            }
            case State.Sus:
            {
                StartCoroutine(SayText(textData.GetTextWhenSus()[_indexWhenSus], delayForSayingSomething));
                StartCoroutine(Disappear());
                break;
            }
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayToDisappear);

        Debug.Log("<color=blue>disappear characters</color>");
        
        // lerp fade out
        // Destroy(this);
    }

    private IEnumerator SayText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log(text);
    }
}
