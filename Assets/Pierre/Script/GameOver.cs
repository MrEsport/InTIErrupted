using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int compteur = 0;
    private bool lampe, yoga, fauteuil = false;

    /*public float timer = 0.0f;

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            CheckButtons();
            timer = 0;
        }
    }*/

    public void CheckButtons()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            yoga = false;
            fauteuil = false;
            if (!lampe)
            {
                lampe = true;
                compteur = 0;
                compteur++;
            }
            else if (compteur < 2)
                compteur++;
            else
                GameOverFaitTrop();
        } 
        
        else if (Input.GetKey(KeyCode.S))
        {
            lampe = false;
            fauteuil = false;
            if (!yoga)
            {
                yoga = true;
                compteur = 0;
                compteur++;
            }
            else if (compteur < 2)
                compteur++;
            else
                GameOverFaitTrop();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            lampe = false;
            yoga = false;
            if (!fauteuil)
            {
                fauteuil = true;
                compteur = 0;
                compteur++;
            }
            else if (compteur < 2)
                compteur++;
            else
                GameOverFaitTrop();
        }

        else
        {
            GameOverFaitRien();
        }
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
