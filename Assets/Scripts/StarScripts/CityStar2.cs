using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStar2 : MonoBehaviour
{
    static bool haveCityStar2 = false;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {

        if (haveCityStar2 == true)
        {
            star.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        haveCityStar2 = true;
    }
}
