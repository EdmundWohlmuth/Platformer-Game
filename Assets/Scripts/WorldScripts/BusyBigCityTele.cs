using UnityEngine.SceneManagement;
using UnityEngine;

public class BusyBigCityTele : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
    }
}
