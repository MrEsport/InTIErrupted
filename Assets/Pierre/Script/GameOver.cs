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
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S))
        {
            fauteuil = false;
            if (!lampe && !yoga)
            {
                lampe = true;
                yoga = true;
                compteurl = 0;
                compteurl++;
                compteury = 0;
                compteury++;
            }
            else if (!lampe)
            {
                lampe = true;
                compteurl = 0;
                compteurl++;
                compteury++;
            }
            else if (!yoga)
            {
                yoga = true;
                compteury = 0;
                compteurl++;
                compteury++;
            }
            else if (compteurl < 2 && compteury < 2)
            {
                compteurl++;
                compteury++;
            }
            else
                GameOverFaitTrop();
        } 
        
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            lampe = false;
            if (!yoga && !fauteuil)
            {
                fauteuil = true;
                yoga = true;
                compteurf = 0;
                compteurf++;
                compteury = 0;
                compteury++;
            }
            else if (!fauteuil)
            {
                fauteuil = true;
                compteurf = 0;
                compteurf++;
                compteury++;
            }
            else if (!yoga)
            {
                yoga = true;
                compteury = 0;
                compteurf++;
                compteury++;
            }
            else if (compteurf < 2 && compteury < 2)
            {
                compteurf++;
                compteury++;
            }
            else
                GameOverFaitTrop();
        }

        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Q))
        {
            yoga = false;
            if (!lampe && !fauteuil)
            {
                fauteuil = true;
                lampe = true;
                compteurf = 0;
                compteurf++;
                compteurl = 0;
                compteurl++;
            }
            else if (!fauteuil)
            {
                fauteuil = true;
                compteurf = 0;
                compteurf++;
                compteurl++;
            }
            else if (!lampe)
            {
                lampe = true;
                compteurl = 0;
                compteurf++;
                compteurl++;
            }
            else if (compteurf < 2 && compteurl < 2)
            {
                compteurf++;
                compteurl++;
            }
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
