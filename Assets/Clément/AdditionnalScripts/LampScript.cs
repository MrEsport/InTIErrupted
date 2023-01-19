using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{

    public GameObject lamp;
    public GameObject exclamation;
    // Start is called before the first frame update
    void Start()
    {
        lamp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            lamp.SetActive(true);
        }
        else
        {
            lamp.SetActive(false);
        }
    }
}
