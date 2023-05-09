using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider slider;
    public float maxTime = 5f;
    public float timeLeft;

    private void Start()
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
        timeLeft = maxTime;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft;
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
