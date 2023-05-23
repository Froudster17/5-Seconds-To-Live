using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Timer timer = collision.gameObject.GetComponent<Timer>();
        if (timer != null)
        {
            timer.CanDie = false;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
