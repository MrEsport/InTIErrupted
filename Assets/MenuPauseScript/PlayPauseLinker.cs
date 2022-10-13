using UnityEngine;
using UnityEngine.UI;

public class PlayPauseLinker : MonoBehaviour
{
    public Button resumeButton;
    public Button menuButton;

    private void Start()
    {
        GameManager.Instance.InitPauseButtons(resumeButton, menuButton);
    }
}
