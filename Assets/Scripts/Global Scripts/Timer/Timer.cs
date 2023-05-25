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

    private Vector2 startPos;

    private float timeLeft; // current time left
    private bool isFlashing = false; // flag to indicate if the sprite is currently flashing
    private bool hasMoved = false; // flag to indicate if the player has moved
    private bool canDie = true; // flag to indicate if the player can die
    private bool canMove = false;
    private bool countdownStarted = false;
    [SerializeField] private float waitTime = 2f;

    private void Start()
    {
        timeLeft = maxTime;
        startPos = gameObject.transform.position;
    }

    private void Update()
    {
        if (canMove == false)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                canMove = true;
            }
            return;
        }

        if (startPos != (Vector2)transform.position)
        {
            hasMoved = true;
        }


        if (hasMoved && !countdownStarted)
        {
            countdownStarted = true;
            InvokeRepeating(nameof(Countdown), 1.0f, 1.0f);
        }
    }

    private void Countdown()
    {
        timeLeft -= 1.0f;
        if (timeLeft <= 0 && canDie == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (!isFlashing && timeLeft <= 10.0f) // check if time is less than or equal to 10 seconds
        {
            isFlashing = true;
            StartCoroutine(FlashCoroutine());
        }
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        float flashSpeed = 2.0f; // initial speed of color transition

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

    public bool CanDie { get => canDie; set => canDie = value; }
}
