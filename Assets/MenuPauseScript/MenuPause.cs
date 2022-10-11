using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] 
    private GameObject menuPause;
    [SerializeField] 
    private string creditSceneName;
    
    public void Play()
    {
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void CreditScene()
    {
        SceneManager.LoadScene(creditSceneName);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
