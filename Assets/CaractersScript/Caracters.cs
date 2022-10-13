using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// lerp appear and disappear (change a)

public class Caracters : MonoBehaviour
{
    [SerializeField] 
    private TextData textData;

    [Header("Event Delays")]
    [SerializeField]
    private float delay = 5;
    [SerializeField]
    private float delayToDisappear = 5;
    [SerializeField]
    private float delayForSayingSomething = 0.2f;

    [SerializeField] 
    private bool isCat;

    [Header("Family Members")]
    [SerializeField]
    private List<Sprite> familySprites = new List<Sprite>();

    [Header("Icons")]
    [SerializeField]
    private SpriteRenderer bubble;
    [SerializeField]
    private SpriteRenderer icon1;
    [SerializeField]
    private SpriteRenderer icon2;

    public enum State
    {
        Normal,
        Sus,
        Detect
    }

    [Header("Sprites")]
    [SerializeField]
    private List<Sprite> iconNormal;    
    [SerializeField]
    private List<Sprite> iconSus;    
    [SerializeField]
    private List<Sprite> iconDetect;

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

    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(State.Normal);

        bubble.gameObject.SetActive(false);
        icon1.gameObject.SetActive(false);
        icon2.gameObject.SetActive(false);
    }
    
    public void CallToCome()
    {
        StopAllCoroutines();
        
        _indexWhenCome = Random.Range(0, textData.GetTextWhenCome().Count);
        _indexWhenNothingSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenDetectSomething = Random.Range(0, textData.GetTextWhenDetectSomething().Count);

        SoundTransmitter.Instance.Play("Knock");
        
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
        
        ShowIcons();

        Debug.Log($"{textData.GetTextWhenCome()[_indexWhenCome]}");

        ButtonManager.Instance.CheckButtons();
        
        if (ButtonManager.Instance.NoButton || ButtonManager.Instance.IsTooMany)
            ChangeState(State.Detect);
        else
        {
            List<PushedButton> pushedButtons = ButtonManager.Instance.GetPushedButtons();

            for (int i = 0; i < pushedButtons.Count; i++)
            {
                Sprite stateSprite = (pushedButtons[i].count) switch
                {
                    1 => iconNormal[(int)pushedButtons[i].button],
                    2 => iconSus[(int)pushedButtons[i].button],
                    3 => iconDetect[(int)pushedButtons[i].button],
                    _ => throw new NotImplementedException()
                };

                if (i == 0)
                    icon1.sprite = stateSprite;
                else
                    icon2.sprite = stateSprite;
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

    private void ShowIcons()
    {
        bubble.gameObject.SetActive(true);
        icon1.gameObject.SetActive(true);
        icon2.gameObject.SetActive(true);
    }

    private void HiddeIcons()
    {
        bubble.gameObject.SetActive(false);
        icon1.gameObject.SetActive(false);
        icon2.gameObject.SetActive(false);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayToDisappear);

        Debug.Log("<color=blue>disappear characters</color>");
        
        HiddeIcons();

        StopAllCoroutines();
    }

    private IEnumerator SayText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log(text);
    }
}
