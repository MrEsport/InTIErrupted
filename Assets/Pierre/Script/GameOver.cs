using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void CheckButtons()
    {
        ButtonManager.Instance.CheckButtons();

        if (ButtonManager.Instance.NoButton)
            GameOverFaitRien();

        if (ButtonManager.Instance.IsTooMany)
            GameOverFaitTrop();
    }

    public void GameOverFaitRien()
    {
        Debug.Log("Tu fous rien");
        //SceneManager.LoadScene("GameOver");
    }

    public void GameOverFaitTrop()
    {
        Debug.Log("Tu es encore là?");
        //SceneManager.LoadScene("GameOver");
    }
}
