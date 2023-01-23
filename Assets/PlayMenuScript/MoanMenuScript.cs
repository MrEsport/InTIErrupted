using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class MoanMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;

    //private string[] guids2;
    //public AudioClip[] moansAudio;

    void Start()
    {
       

    }

    void Update()
    {
       /* if (mouse_over)
        {
            Debug.Log("Mouse Over");
        } */
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
       mouse_over = true;
          SoundTransmitter.Instance.Play("MoanMenu");
      
        //   Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        //SoundTransmitter.Instance.ChangeMoan();
        GameObject tempSM;
        tempSM = GameObject.Find("SoundManager").gameObject;
        tempSM.GetComponent<SoundTransmitter>().ChangeMoan();
        //   Debug.Log("Mouse exit");
    } 
}
