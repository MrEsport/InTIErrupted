using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager _instance = null;
    public static ButtonManager Instance { get { return _instance; } }

    public Caract caract;

    public bool NoButton { get; set; }
    public bool IsTooMany
    {
        get
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].count < 3)
                    continue;
                return true;
            }
            return false;
        }
    }

    [SerializeField] public List<IRLButton> buttons;
    [SerializeField] private IRLButton winButton;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckButtons()
    {
        if (caract.IsCat()) return;
        
        int count = 0;
        for (int i = 0; i < buttons.Count; ++i)
        {
            IRLButton b = buttons[i];
            if (GetButton(b.group))
            {
                b.Increment();
                ++count;
            }
            else
                b.ResetCount();
        }
        NoButton = count < 2;
    }

    public List<PushedButton> GetPushedButtons()
    {
        List<PushedButton> list = new();
        for (int i = 0; i < buttons.Count; ++i)
        {
            if (!buttons[i].GetButton())
                continue;
            list.Add(new PushedButton { button = (EButton)i, count = buttons[i].count });
        }
        return list;
    }

    public bool GetWinButton() => winButton.GetButton();

    private bool GetButton(EButton button) => buttons[(int)button].GetButton();
}
