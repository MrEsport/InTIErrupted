using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public int compteurl, compteury, compteurf = 0;
    public bool lampe, yoga, fauteuil = false;

    public float timer = 0.0f;

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            CheckButtons();
            timer = 0;
        }
    }

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
