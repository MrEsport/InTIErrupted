using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    private Transform interrupterTransform;
    [SerializeField]
    private SpriteRenderer interrupterSprite;
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

    [Header("Doors")]
    [SerializeField]
    private List<Transform> doors;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private List<Sprite> partSprite;

    private bool enteringDoorLeft = true;

    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(State.Normal);

        HideIntruder();
        HideIcons();
    }

    protected void Update()
    {
        //
    }

    public void CallToCome()
    {
        StopAllCoroutines();
        
        _indexWhenCome = Random.Range(0, textData.GetTextWhenCome().Count);
        _indexWhenNothingSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenSus = Random.Range(0, textData.GetTextWhenSus().Count);
        _indexWhenDetectSomething = Random.Range(0, textData.GetTextWhenDetectSomething().Count);
        enteringDoorLeft = Random.Range(-1f, 1f) <= 0f;

        // Knock Before Entering
        StartCoroutine(DoorKnockFeedback(enteringDoorLeft));
        
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
        // delay after knocking
        yield return new WaitForSeconds(delay);
        ShowIntruder();
        SoundTransmitter.Instance.Play("DoorOpen");

        yield return new WaitForSeconds(delayForSayingSomething);
        ShowIcons();

        Debug.Log($"{textData.GetTextWhenCome()[_indexWhenCome]}");

        ButtonManager.Instance.CheckButtons();

        bool noButton = ButtonManager.Instance.NoButton;
        bool tooMany = ButtonManager.Instance.IsTooMany;

        if (noButton || tooMany)
            ChangeState(State.Detect);

        switch (state)
        {
            case State.Detect:
                {
                    StartCoroutine(SayText(textData.GetTextWhenDetectSomething()[_indexWhenDetectSomething], delayForSayingSomething));
                    GameOver();
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
        };

        if (ButtonManager.Instance.NoButton)
            yield break;
        
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

            bool showIcon = !tooMany || (tooMany && pushedButtons[i].count > 2);
            if (i == 0)
            {
                icon1.sprite = stateSprite;
                icon1.gameObject.SetActive(showIcon);
            }
            else
            {
                icon2.sprite = stateSprite;
                icon2.gameObject.SetActive(showIcon);
            }
        }
    }

    private void ShowIntruder()
    {
        int randomMember = Random.Range(0, familySprites.Count);

        isCat = randomMember == 0;
        
        interrupterSprite.sprite = familySprites[randomMember];

        if (interrupterSprite.sprite.name == "characters_3")
        {
            interrupterSprite.gameObject.transform.position = new Vector3(interrupterSprite.gameObject.transform.position.x, interrupterSprite.gameObject.transform.position.y + 218.6f, interrupterSprite.gameObject.transform.position.z);
        }

        interrupterTransform.gameObject.SetActive(true);
    }

    private void HideIntruder()
    {
        interrupterSprite.gameObject.transform.position = new Vector3(interrupterSprite.gameObject.transform.position.x, - 220.6f, interrupterSprite.gameObject.transform.position.z);
        interrupterTransform.gameObject.SetActive(false);
    }

    private void ShowIcons()
    {
        if (isCat) return;
        
        bubble.gameObject.SetActive(true);
        icon1.gameObject.SetActive(true);
        icon2.gameObject.SetActive(true);
    }

    private void HideIcons()
    {
        bubble.gameObject.SetActive(false);
        icon1.gameObject.SetActive(false);
        icon2.gameObject.SetActive(false);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(delayToDisappear);

        HideIcons();
        HideIntruder();
        SoundTransmitter.Instance.Play("DoorClose");

        StopAllCoroutines();
    }

    private IEnumerator SayText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Debug.Log(text);
    }

    private void GameOver()
    {
        Debug.Log("<color=red>Game over</color>");
        GameManager.Instance.GameOver();
        Zawarudo.stop = true;
    }

    // Doors logic
    private IEnumerator DoorKnockFeedback(bool left)
    {
        SoundTransmitter.Instance.Play(left ? "KnockL" : "KnockR");

        Transform door = doors[left ? 0 : 1];

        yield return new WaitForSeconds(.15f);

        ParticleSystem ps = Instantiate(particles, door);
        ps.transform.forward = door.forward * (left ? -1 : 1);
        // 264-268 : Remettre le knock FX à la bonne place. Code un peu dégueu de Clément sans doute à revoir pour rendre ça plus sympa si on doit apporter des modifications à la scène.
        if (left){
            ps.transform.position = new Vector3(ps.transform.position.x + 180, ps.transform.position.y, ps.transform.position.z + 50);
        } else if (!left){
            ps.transform.position = new Vector3(ps.transform.position.x - 180, ps.transform.position.y, ps.transform.position.z + 50);
        }
        ps.textureSheetAnimation.SetSprite(0, partSprite[left ? 0 : 1]);

        for (int i = 0; i < 3; ++i)
        {
            ps.Play();
            yield return new WaitForSeconds(.4f);
        }

        Destroy(ps.gameObject);
    }
}
