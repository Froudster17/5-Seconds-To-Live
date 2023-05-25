using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Animator animator;

    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timer != null)
        {
            timer.CanDie = false;
        }

        animator.SetTrigger("Start");

        // Delay the scene change based on the animation length
        float animationLength = GetAnimationLength("Start");
        Invoke(nameof(LoadNextScene), animationLength);
    }

    private float GetAnimationLength(string triggerName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == triggerName)
            {
                return ac.animationClips[i].length;
            }
        }
        return 0f;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
