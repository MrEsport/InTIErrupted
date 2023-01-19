using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.SceneManagement;

public class EndScenesTransitionBehaviour : MonoBehaviour
{

    public GameObject UI;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TransitionScene()
    {
        GameObject tempChara;
        tempChara = GameObject.Find("Interrupter");
       character.gameObject.GetComponent<SpriteRenderer>().sprite = tempChara.gameObject.GetComponent<SpriteRenderer>().sprite;
        tempChara.gameObject.SetActive(false);
        character.gameObject.SetActive(true);
        SoundTransmitter.Instance.Play("DoorClose");

    }

    public void EndTransition()
    {
        UI.gameObject.SetActive(true);
        SoundTransmitter.Instance.Play("Defeat");


    }


    public void ShakeCamera()
    {
        GameObject tempCam;
        tempCam = GameObject.Find("CameraHolder/Main Camera");

        ShakePreset tempPreset;
        tempPreset = GameObject.Find("Chara").gameObject.GetComponent<Caract>().ShakeSettings;

        tempCam.gameObject.GetComponent<Shaker>().Shake(tempPreset);
        SoundTransmitter.Instance.Play("DoorOpen");
    }


    public void EndExclamation()
    {
      //  SoundTransmitter.Instance.Play("Defeat");
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

}
