using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StarPref : MonoBehaviour
{
    static bool haveStar = false;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {

        if (haveStar == true)
        {
            star.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        haveStar = true;
    }
}
