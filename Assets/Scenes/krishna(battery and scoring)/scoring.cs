﻿using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class BatteryController : MonoBehaviour
{

    // Create public variables for  for the Text UI game objects

    public Text countText;
    public Text winText;


    private int count;

    // At the start of the game..
    void Start()
    {


        // Set the count to zero
        count = 0;

        // Run the SetCountText function to update the UI (see below)
        SetCountText();

        // Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winText.text = "";
    }


    // When this game object intersects a collider with 'is trigger' checked,
    // store a reference to that collider in a variable named 'other'..
    void OnTriggerEnter(Collider other)
    {
        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetCountText()
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + count.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (count >= 12)
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!";
        }
    }
}