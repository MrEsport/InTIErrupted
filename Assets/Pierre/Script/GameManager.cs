using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer = 0.0f;

    private bool endingGame = false;
    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;
        
        // Derniers appels avant Fin : Animation ? Timer ?

        //if (endingGame) return;

        if (ButtonManager.Instance.GetWinButton())
            KeysWin();

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
        Debug.Log("Tu fous rien !\nGAME OVER");
        //SceneManager.LoadScene("GameOver");
        endingGame = true;
    }

    public void GameOverFaitTrop()
    {
        Debug.Log("Tu es encore là?\nGAME OVER");
        //SceneManager.LoadScene("GameOver");
        endingGame = true;
    }

    public void KeysWin()
    {
        Debug.Log("Clés trouvées.. Porte fermées... (¬ v ¬)\nGAME OVER");
        endingGame = true;
    }
}
