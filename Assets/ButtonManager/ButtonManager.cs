using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager _instance = null;
    public static ButtonManager Instance { get { return _instance; } }

    [SerializeField] private List<IRLButton> buttons;



    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Merci Pierre  :)
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool GetButton(EButton button) => buttons[(int)button].GetButton();
}
