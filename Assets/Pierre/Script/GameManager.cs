using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

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

        if (endingGame) return;

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
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(LoadPlayScene);

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(LoadCredits);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(Quit);

        playingGame = false;
    }

    public void InitGameOverButtons(Button retryButton, Button quitButton)
    {
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(LoadMenuScene);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(Quit);

        playingGame = false;
    }

    public void InitPauseButtons(Button resumeButton, Button menuButton)
    {
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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene")
        {
            Debug.Log("Clés trouvées.. Porte fermées... (¬ v ¬)\nGAME OVER");
            endingGame = true;
            Zawarudo.stop = true;
            SoundTransmitter.Instance.Stop("Music");
            SoundTransmitter.Instance.Play("HeartBeat");
            GameObject tempVol;
            tempVol = GameObject.Find("Chara").gameObject;
            // tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Volume>().profile = tempVol.GetComponent<Caracters>().orgasmPPVolume;
            //      StartCoroutine(LaunchVictory(tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Animator>()));
            tempVol.GetComponent<Caracters>().mainCamera.GetComponent<Animator>().SetBool("Flashing", true);
            // SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
        }

    }

 /*   public IEnumerator LaunchVictory(Animator anim)
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("Flashing", true);
    } */

    public void LoadPlayScene()
    {
        for (int i = 0; i < 4; i++) //Longueur hardocdée, à modifier si un objet a été ajouté au pool des alibis
        {
            ButtonManager.Instance.buttons[i].count = 0;
        };
        endingGame = false;
        playingGame = true;
        paused = false;
        SceneManager.LoadScene("MainScene");
    }
    private void LoadMenuScene()
    {
        for (int i = 0; i < 4; i++) //Longueur hardocdée, à modifier si un objet a été ajouté au pool des alibis
        {
            ButtonManager.Instance.buttons[i].count = 0;
        };
        endingGame = false;
        playingGame = false;
        SoundTransmitter.Instance.Play("Music");
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

    public void GameOver()
    {

        SoundTransmitter.Instance.Play("Alert");
        SoundTransmitter.Instance.Stop("Music");
        SoundTransmitter.Instance.Stop("SusMusic");

        GameObject tempExcla;
        tempExcla = GameObject.Find("CameraHolder").gameObject.GetComponent<LampScript>().exclamation.gameObject;

        tempExcla.gameObject.SetActive(true);

    }
}
