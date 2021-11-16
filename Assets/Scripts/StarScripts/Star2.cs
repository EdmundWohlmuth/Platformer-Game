using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star2 : MonoBehaviour
{
    static bool haveStar2 = false;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {

        if (haveStar2 == true)
        {
            star.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        haveStar2 = true;
    }
}
