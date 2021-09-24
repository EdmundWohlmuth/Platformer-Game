using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class uiController : MonoBehaviour
{
    private int coinCount;
    private int starCount;
    private int livesCount;


    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI livesText;


    void Start()
    {
        coinCount = 0;
        starCount = 0;
        livesCount = 3;
        SetCountText();
        SetStarText();
        SetLivesText();
        winTextObject.SetActive(false);
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
        }

        if (other.gameObject.CompareTag("TeleportTrigger"))
        {
            livesCount = livesCount - 1;
            SetLivesText();
        }
    }
}


