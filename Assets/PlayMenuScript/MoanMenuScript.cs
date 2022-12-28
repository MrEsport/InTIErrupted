using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoanMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");
        }
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
     //   Debug.Log("Mouse exit");
    } 
}
