using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class uiController : MonoBehaviour
{
    private int coinCount;
    static int starCount = 0;
    static int livesCount = 3;   

    public InputActionReference movementControl;
    public InputActionReference jumpControl;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI livesText;

    public GameObject portalTwo;
    public GameObject coinStar;

    public PlayableDirector timeline;

    private bool hasntCollected;

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

    void Start()
    {
        hasntCollected = true;
        coinCount = 0;
        SetCountText();
        SetStarText();
        SetLivesText();
        winTextObject.SetActive(false);
        // Enable Portals
        if (starCount >= 1)
        {
            portalTwo.SetActive(true);
        }
    }

    //Sets the coin count
    void SetCountText()
    {
        countText.text = "Coins: " + coinCount.ToString();
    }
    void SetStarText()
    {
        starText.text = "Stars: " + starCount.ToString();
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + livesCount.ToString();
    }

    private void Update()
    {
        if (livesCount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    //Interations with coins/stars
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            coinCount = coinCount + 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("FinnishLine"))
        {
            winTextObject.SetActive(true);
            other.gameObject.SetActive(false);

            starCount = starCount + 1;

            SetStarText();
            StartCoroutine(WaitBeforeEnd());
        }

        if (other.gameObject.CompareTag("OneUp"))
        {
            other.gameObject.SetActive(false);
            livesCount = livesCount + 1;
            SetLivesText();
        }

        IEnumerator WaitBeforeEnd()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(1);
        }

        if (other.gameObject.CompareTag("TeleportTrigger"))
        {
            livesCount = livesCount - 1;          
            SetLivesText();
        }

        if (coinCount == 50 && hasntCollected)
        {
            hasntCollected = false;
            timeline.Play();
            StartCoroutine(starCutScene());
        }

        IEnumerator starCutScene()
        {
            OnDisable();
            Debug.Log("The WORLD!");
            yield return new WaitForSeconds(5);
            Debug.Log("Time Moves again...");
            OnEnable();
        }


    }
}


