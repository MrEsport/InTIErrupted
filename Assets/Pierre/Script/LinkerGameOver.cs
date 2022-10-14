using UnityEngine;
using UnityEngine.UI;

public class LinkerGameOver : MonoBehaviour
{
    public Button retryButton;
    public Button quitButton;

    void Start()
    {
        GameManager.Instance.InitGameOverButtons(retryButton, quitButton);
    }
}
