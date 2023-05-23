using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float maxTime = 60.0f; // maximum time in seconds
    public SpriteRenderer playerSpriteRenderer; // reference to the player's sprite renderer
    public Color originalColor = Color.white; // original color of the sprite
    public Color flashColor = Color.red; // color to flash when time gets less

    private float timeLeft; // current time left
    public float flashSpeed = 2.0f; // speed of color transition
    private bool isFlashing = false; // flag to indicate if the sprite is currently flashing

    void Start()
    {
        timeLeft = maxTime;
        InvokeRepeating("Countdown", 1.0f, 1.0f);
    }

    void Countdown()
    {
        timeLeft -= 1.0f;
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!isFlashing && timeLeft <= 10.0f) // check if time is less than or equal to 10 seconds
        {
            isFlashing = true;
            StartFlash();
        }
    }

    void StartFlash()
    {
        StartCoroutine(FlashCoroutine());
    }

    System.Collections.IEnumerator FlashCoroutine()
    {
        while (timeLeft > 0 && isFlashing)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1.0f); // calculate ping pong value between 0 and 1
            Color lerpedColor = Color.Lerp(originalColor, flashColor, t);
            playerSpriteRenderer.color = lerpedColor;

            // Increase the flash speed as time goes down
            flashSpeed = Mathf.Lerp(2.0f, 10.0f, 1 - (timeLeft / 10.0f));

            yield return null;
        }

        isFlashing = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Time Left: " + Mathf.RoundToInt(timeLeft));
    }
}
