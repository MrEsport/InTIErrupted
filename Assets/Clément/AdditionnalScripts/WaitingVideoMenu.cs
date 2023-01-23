using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaitingVideoMenu : MonoBehaviour
{

    public GameObject playingVideo;
    public GameObject canvas;
    public GameObject SoundManager;
    public string[] guids2;
    public AudioClip[] moansAudio;

    public List<UnityEngine.Video.VideoClip> video;

    public bool hasWait = false;

    private bool MainMenuMode = false;  // false = main menu | true = video mode

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!hasWait && !MainMenuMode) {
            StartCoroutine(Waiting());
           // Debug.Log("Si ce message est envoyé plusieurs fois, alors ça bug.");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (MainMenuMode)
            {
                playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
                canvas.SetActive(true);
                SoundManager.SetActive(true);
                hasWait = false;
                MainMenuMode = !MainMenuMode;
            }
            else if (!MainMenuMode)
            {
                int nbAlea;
                nbAlea = Random.Range(0, video.Count);

                Debug.Log(nbAlea);
                Debug.Log(playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().length);
                

                playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip = video[nbAlea];
                    playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                    canvas.SetActive(false);
                SoundManager.SetActive(false);
                hasWait = true;
                MainMenuMode = !MainMenuMode;
                
            }

            
        }

        if (hasWait && !MainMenuMode)
        {
            int nbAlea;
            nbAlea = Random.Range(0, video.Count);

            Debug.Log(nbAlea);
            Debug.Log(playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().length);


            playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip = video[nbAlea];
            playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            canvas.SetActive(false);
            SoundManager.SetActive(false);

            MainMenuMode = !MainMenuMode;
        }

        if (playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().time >= playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().length-0.1)
        {
            // playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();

            int nbAlea;
            nbAlea = Random.Range(0, video.Count);

            Debug.Log(nbAlea);

            playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip = video[nbAlea];
            playingVideo.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        }

        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(120f);
            hasWait=true;
        }




    }


}
