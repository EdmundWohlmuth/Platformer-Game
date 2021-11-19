using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStar1 : MonoBehaviour
{
    static bool haveCityStar = false;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {

        if (haveCityStar == true)
        {
            star.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        haveCityStar = true;
    }
}
