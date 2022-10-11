using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    [SerializeField]
    private int indexGameScene;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(indexGameScene);
    }
}
