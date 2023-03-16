using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using UnityEngine.SceneManagement;

public class WaitingVideoMenu : MonoBehaviour
{
    public GameObject playingVideo;
    public GameObject canvas;
    public GameObject SoundManager;
    public string[] guids2;
    public AudioClip[] moansAudio;

    public List<VideoClip> video;

    public float waitTimeAmount = 120f;
    public bool hasWait = false;
    private bool isVideoPlaying = false;  // false = main menu | true = video mode

    private Coroutine waitRoutine = null;

    private VideoPlayer videoPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = playingVideo.gameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWait && !isVideoPlaying && waitRoutine == null)
        {
            waitRoutine = StartCoroutine(Waiting());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isVideoPlaying)
            {
                SoundTransmitter.Instance.Play("Music");
                videoPlayer.Stop();
                canvas.SetActive(true);
               // SoundManager.SetActive(true);
                hasWait = false;
                isVideoPlaying = !isVideoPlaying;
                StopAllCoroutines();
               // SceneManager.LoadScene("MenuPlay");
            }
            else
            {
                SoundTransmitter.Instance.StopAll();
                int nbAlea;
                nbAlea = Random.Range(0, video.Count);

                //Debug.Log(nbAlea);
                //Debug.Log(videoPlayer.length);

                videoPlayer.clip = video[nbAlea];
                videoPlayer.Play();
                canvas.SetActive(false);
                // SoundManager.SetActive(false);
                
                hasWait = true;
                isVideoPlaying = !isVideoPlaying;
            }
        }

        if (hasWait && !isVideoPlaying)
        {
            SoundTransmitter.Instance.StopAll();
            int nbAlea;
            nbAlea = Random.Range(0, video.Count);

            //Debug.Log(nbAlea);
            //Debug.Log(videoPlayer.length);

            videoPlayer.clip = video[nbAlea];
            videoPlayer.Play();
            canvas.SetActive(false);
           // SoundManager.SetActive(false);

            isVideoPlaying = !isVideoPlaying;
        }

        if (videoPlayer.time >= videoPlayer.length - 0.1)
        {
            // videoPlayer.Pause();

            int nbAlea;
            nbAlea = Random.Range(0, video.Count);

            //Debug.Log(nbAlea);

            videoPlayer.clip = video[nbAlea];
            videoPlayer.Play();
        }

        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(waitTimeAmount);
            hasWait = true;
            waitRoutine = null;
        } 
    }
}
