using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Timer timer = collision.gameObject.GetComponent<Timer>();
        if (timer != null)
        {
            timer.CanDie = false;
        }

        animator.SetTrigger("Start");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
