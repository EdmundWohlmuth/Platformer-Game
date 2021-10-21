using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
