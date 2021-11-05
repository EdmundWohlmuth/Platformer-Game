using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupCode : MonoBehaviour
{
    public AudioClip coinPickUp;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0, 0, 90) * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(coinPickUp, transform.position);
    }
}
