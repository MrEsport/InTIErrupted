using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }
    private bool endingGame = false;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitMenuButtons(Button playButton, Button quitButton)
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(LoadPlayScene);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(LoadMenuScene);
    }

    void Update()
    {
        if (gameEnded) return;
        
        // Derniers appels avant Fin : Animation ? Timer ?

        //if (endingGame) return;

        if (ButtonManager.Instance.GetWinButton())
            KeysWin();
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

    private void LoadPlayScene() => SceneManager.LoadScene("MainScene");
    private void LoadMenuScene() => SceneManager.LoadScene("MenuPlay");
}
