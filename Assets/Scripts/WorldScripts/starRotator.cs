using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starRotator : MonoBehaviour
{
    public AudioClip TadaSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (90, 0, 0) * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(TadaSound, transform.position);
    }
}
