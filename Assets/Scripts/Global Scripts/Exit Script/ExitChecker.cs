using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecker : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();

        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timer != null)
        {
            timer.CanDie = false;
        }

        animator.SetTrigger("Start");

        StartCoroutine(LoadNextScene());   
    }

    private IEnumerator LoadNextScene()
    {

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
