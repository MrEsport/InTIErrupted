using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    private bool endingGame = false;
    private bool gameEnded = false;
    private bool playingGame = false;
    private bool paused = false;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (gameEnded) return;

        // Derniers appels avant Fin : Animation ? Timer ?

        //if (endingGame) return;

        if (ButtonManager.Instance.GetWinButton())
            KeysWin();

        if(playingGame && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) Resume();
            else Pause();
        }
    }

    public void InitMenuButtons(Button playButton, Button creditsButton, Button quitButton)
    {
        Debug.Log("Link Menu !");
        playButton.onClick.RemoveAllListeners();
        Debug.Log($"//Play Button events Count: {playButton.onClick.GetPersistentEventCount()}");
        playButton.onClick.AddListener(LoadPlayScene);
        Debug.Log($"//Play Button events Count: {playButton.onClick.GetPersistentEventCount()}");

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(LoadCredits);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(Quit);

        playingGame = false;
    }

    public void InitPauseButtons(Button resumeButton, Button menuButton)
    {
        Debug.Log("Link Pause !");
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(Resume);

        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(LoadMenuScene);
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

    private void LoadPlayScene()
    {
        Debug.Log("Load Scene !!!");
        playingGame = true;
        paused = false;
        SceneManager.LoadScene("MainScene");
    }
    private void LoadMenuScene()
    {
        playingGame = false;
        SceneManager.LoadScene("MenuPlay");
    }
    private void LoadCredits() {}
    private void Pause()
    {
        paused = true;
        SceneManager.LoadScene("MenuPause", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
    private void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("MenuPause");
    }
    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
