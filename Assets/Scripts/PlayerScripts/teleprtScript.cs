using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleprtScript : MonoBehaviour
{
    public Transform teleportPos;
    public GameObject ThePlayer;

    private void OnTriggerEnter(Collider other)
    {
            ThePlayer.transform.position = teleportPos.transform.position;        
    }
}
