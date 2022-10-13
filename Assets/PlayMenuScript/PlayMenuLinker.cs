using UnityEngine;
using UnityEngine.UI;

public class PlayMenuLinker : MonoBehaviour
{
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;

    private void Start()
    {
        GameManager.Instance.InitMenuButtons(playButton, creditsButton, quitButton);
    }
}
