using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using MilkShake;

    public class Caracters : MonoBehaviour
    {

        [SerializeField]
        private TextData textData;

        [Header("Camera")]
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private ShakePreset ShakeSettings;

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
        [SerializeField]
        private List<GameObject> doorsGO; //Needs to be Left, and then Right. The waited GO is the one stored in the tree in Gameplay -> ....Wall -> DoorXScaler -> The game object you have to select

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
      //  interrupterSprite.gameObject.GetComponent<Animator>().SetBool("Talked", true);
    }

        private void ShowIntruder()
        {
            //Open Door
            doorsGO[0].gameObject.GetComponent<Animator>().SetBool("IsKnocked", false);
            doorsGO[1].gameObject.GetComponent<Animator>().SetBool("IsKnocked", false);
            int randomMember = Random.Range(0, familySprites.Count);

            isCat = randomMember == 0;

            interrupterSprite.sprite = familySprites[randomMember];

          /*  if (interrupterSprite.sprite.name == "characters_3")
            {
                interrupterSprite.gameObject.transform.position = new Vector3(interrupterSprite.gameObject.transform.position.x, interrupterSprite.gameObject.transform.position.y + 218.6f, interrupterSprite.gameObject.transform.position.z);
            } */

            interrupterTransform.gameObject.SetActive(true);

        // Trigger l'effet de 'shaking' sur la caméra
           mainCamera.gameObject.GetComponent<Shaker>().Shake(ShakeSettings);
        }

        private void HideIntruder()
        {
            interrupterSprite.gameObject.transform.position = new Vector3(interrupterSprite.gameObject.transform.position.x, -220.6f, interrupterSprite.gameObject.transform.position.z);
            interrupterTransform.gameObject.SetActive(false);

            //Close door
            doorsGO[0].gameObject.GetComponent<Animator>().SetBool("IsOpened", true);
            doorsGO[1].gameObject.GetComponent<Animator>().SetBool("IsOpened", true);
        Debug.Log("Closing Doors. Now leaving the room.");
      //  interrupterSprite.gameObject.GetComponent<Animator>().SetBool("Talked", true);
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
        interrupterSprite.gameObject.GetComponent<Animator>().SetBool("Talked", true);
        yield return new WaitForSeconds(0.8f);
        HideIcons();
            HideIntruder();
            SoundTransmitter.Instance.Play("DoorClose");
      //  interrupterSprite.gameObject.GetComponent<Animator>().SetBool("Talked", false);
        StopAllCoroutines();
        }

        private IEnumerator SayText(string text, float delay)
        {
            yield return new WaitForSeconds(delay);
       // interrupterSprite.gameObject.GetComponent<Animator>().SetBool("Talked", true);
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

            yield return new WaitForSeconds(.2f);
        SoundTransmitter.Instance.Stop("Music");
        ParticleSystem ps = Instantiate(particles, door);
            ps.transform.forward = door.forward * (left ? -1 : 1);
            // Reset a parametters in door's animator
            doorsGO[0].gameObject.GetComponent<Animator>().SetBool("IsOpened", false);
            doorsGO[1].gameObject.GetComponent<Animator>().SetBool("IsOpened", false);
            // 280-287 : Remettre le knock FX à la bonne place. Code un peu dégueu de Clément sans doute à revoir pour rendre ça plus sympa si on doit apporter des modifications à la scène.
            if (left)
            {
                ps.transform.position = new Vector3(ps.transform.position.x + 125, ps.transform.position.y +134, ps.transform.position.z - 160); // Ok c'est hardcodé et jsp pourquoi mais le moteur inverse le x et le z je crois je bite R
                doorsGO[0].gameObject.GetComponent<Animator>().SetBool("IsKnocked", true);
            }
            else if (!left)
            {
                ps.transform.position = new Vector3(ps.transform.position.x - 125, ps.transform.position.y +134, ps.transform.position.z -160);
                doorsGO[1].gameObject.GetComponent<Animator>().SetBool("IsKnocked", true);
            }
            ps.textureSheetAnimation.SetSprite(0, partSprite[left ? 0 : 1]);

            for (int i = 0; i < 3; ++i)
            {
                ps.Play();
           // Debug.Log("1 knock");
                yield return new WaitForSeconds(0.5f);
            }

            Destroy(ps.gameObject);
        SoundTransmitter.Instance.Play("Music");
    }
    }
