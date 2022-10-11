using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextData : MonoBehaviour
{
    [SerializeField] private List<string> textWhenCome;
    [SerializeField] private List<string> textWhenNothingSus;
    [SerializeField] private List<string> textWhenSus;
    [SerializeField] private List<string> textWhenDetectSomething;

    public List<string> GetTextWhenCome()
    {
        return textWhenCome;
    }

    public List<string> GetTextWhenNothingSus()
    {
        return textWhenNothingSus;
    }

    public List<string> GetTextWhenSus()
    {
        return textWhenSus;
    }

    public List<string> GetTextWhenDetectSomething()
    {
        return textWhenDetectSomething;
    }
}
