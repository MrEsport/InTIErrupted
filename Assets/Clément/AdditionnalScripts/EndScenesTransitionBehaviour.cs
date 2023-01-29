using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EndScenesTransitionBehaviour : MonoBehaviour
{

    public GameObject UI;
    public GameObject character;

    private bool toggleVol = false;

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

    public void TransitionVictory()
    {
          character.gameObject.SetActive(false); 
        
        GameObject tempVol;
        tempVol = GameObject.Find("Chara").gameObject;
        tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Volume>().profile = tempVol.GetComponent<Caracters>().gamePPVolume;
        tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Animator>().SetBool("Flashing", false);
        tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Volume>().profile = tempVol.GetComponent<Caracters>().gamePPVolume;
        ChangePPVolume();
        
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);

        


    }

    public void ChangePPVolume()
    {
        GameObject tempVol;
        tempVol = GameObject.Find("Chara").gameObject;

        if (!toggleVol)
        {
            tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Volume>().profile = tempVol.GetComponent<Caracters>().orgasmPPVolume;
        }
        else if (toggleVol)
        {
            tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Volume>().profile = tempVol.GetComponent<Caracters>().gamePPVolume;
        }

        toggleVol = !toggleVol;
        Debug.Log("VOLUME CHANGED");
    }

    public void MoaningEnd()
    {
        GameObject tempSM;
        tempSM = GameObject.Find("SoundManager").gameObject;
        tempSM.GetComponent<SoundTransmitter>().ChangeMoanEnd();

        GameObject tempAH;
        tempAH = GameObject.Find("Canvas/AH").gameObject;

        // tempAH.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        tempAH.gameObject.GetComponent<Image>().enabled = true;
        tempAH.gameObject.GetComponent<Animator>().enabled = true;

        SoundTransmitter.Instance.Play("MoanEnd");
      //  UI.gameObject.SetActive(true);

    }

    IEnumerator Attente(float f)
    {
        yield return new WaitForSeconds(f);
    }


}
