using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caract : Caracters
{
    // Start is called before the first frame update
    /* private new void Start()
     {
         base.Start();
         ButtonManager.Instance.caract = this;
     }

     // Update is called once per frame
     private new void Update()
     {
         base.Update();
     } */

    private void Start()
    {
        base.Start();
        ButtonManager.Instance.caract = this;
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
    }
}
