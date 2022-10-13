using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenuLinker : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        GameManager.Instance.InitMenuButtons(playButton, quitButton);
    }
}
