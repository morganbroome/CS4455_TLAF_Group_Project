using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    public Canvas HUDCanvas;
    private TextMeshProUGUI coinCounter;

    private int numCoins;

    public AudioSource audioSource;
    public AudioClip clip;

    private void OnEnable()
    {
        // Subscribe to collectable event
        Collectable.onCollectablePickup += UpdateInventory;
    }

    private void OnDisable()
    {
        // Unsubscribe to collectable event
        Collectable.onCollectablePickup -= UpdateInventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find the canvas element and its children
        HUDCanvas = GameObject.Find("HUDCanvas").GetComponent<Canvas>();
        coinCounter = HUDCanvas.transform.Find("Inventory").Find("CoinCounter").GetComponent<TextMeshProUGUI>();

        // Default values
        numCoins = 0;
        coinCounter.text = "COINS: " + numCoins.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInventory()
    {
        numCoins++;
        coinCounter.text = "COINS: " + numCoins.ToString();
        Debug.Log("Coin picked up! Updating UI...");
        audioSource.PlayOneShot(clip);

        if (numCoins > 10)
        {
            coinCounter.text = "You have collected all the coins! You win!";
        }
    }
}
