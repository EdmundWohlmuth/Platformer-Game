using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleprtScript : MonoBehaviour
{
    public Transform teleportPos;
    public GameObject playerCharacter;

    private void OnTriggerEnter(Collider other)
    {
        playerCharacter.transform.position = teleportPos.transform.position;
    }
}
