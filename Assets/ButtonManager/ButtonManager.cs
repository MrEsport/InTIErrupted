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
    [SerializeField] public IRLButton winButton;

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
          //  Debug.Log(b.group.ToString());
           // Debug.Log(b.GetButton().ToString());

          /*  if (i == 3)
            {
              
                  Debug.Log(b.GetButton().ToString());
            } */

            if (GetButton(b.group) && i!=3) // Incrementation des alibis sauf le book
            {              
                b.Increment();
                ++count;
            } else if (!GetButton(b.group) && i == 3) // Incrementation du book seulement
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
            if ((!buttons[i].GetButton() && i!=3) || (buttons[i].GetButton() && i == 3)) // on détecte quel bouton est appuyé et on passe si ce n'est pas le cas, avec prise en compte de l'ailibi book inversé
                continue;
            list.Add(new PushedButton { button = (EButton)i, count = buttons[i].count });
        }
        return list;
    }

    public bool GetWinButton() => winButton.GetButton();

    private bool GetButton(EButton button) => buttons[(int)button].GetButton();
}
