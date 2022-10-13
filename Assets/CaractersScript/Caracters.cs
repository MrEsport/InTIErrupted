using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private List<GameObject> iconNormal;    
    [SerializeField]
    private List<GameObject> iconSus;    
    [SerializeField]
    private List<GameObject> iconDetect;

    public int IndexWhenCome
    {
        get => _indexWhenCome;
        set => _indexWhenCome = value;
    }

    [SerializeField]
    private State state;

    private int _indexWhenCome;
    private int _indexWhenNothingSus;
    private int _indexWhenSus;
    private int _indexWhenDetectSomething;

    private List<GameObject> _icons;

    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(State.Normal);
        
        _noise = GetComponent<AudioSource>();
        CallToCome();
    }
    
    public void CallToCome()
    {
        StopAllCoroutines();
        
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

        ButtonManager.Instance.CheckButtons();
        
        if (ButtonManager.Instance.NoButton || ButtonManager.Instance.IsTooMany)
            ChangeState(State.Detect);
        else
        {
            List<PushedButton> pushedButtons = ButtonManager.Instance.GetPushedButtons();

            for (int i = 0; i < pushedButtons.Count; i++)
            {
                switch (pushedButtons[i].count)
                {
                    case 1:
                        iconNormal[(int)pushedButtons[i].button].SetActive(true);
                        _icons.Add(iconNormal[(int)pushedButtons[i].button]);
                        break;
                    case 2:
                        ChangeState(State.Sus);
                        iconSus[(int)pushedButtons[i].button].SetActive(true);
                        _icons.Add(iconSus[(int)pushedButtons[i].button]);
                        break;
                    case 3:
                        iconDetect[(int)pushedButtons[i].button].SetActive(true);
                        _icons.Add(iconDetect[(int)pushedButtons[i].button]);
                        ChangeState(State.Detect);
                        break;
                    default:
                        break;
                }
            }
        }
        
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

    private void HiddeIcon()
    {
        foreach (var icon in _icons)
        {
            icon.SetActive(false);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayToDisappear);

        Debug.Log("<color=blue>disappear characters</color>");
        
        HiddeIcon();

        StopAllCoroutines();
    }

    private IEnumerator SayText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log(text);
    }
}
