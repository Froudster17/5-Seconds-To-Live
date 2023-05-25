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

        // Delay the scene change based on the animation length
        float animationLength = GetAnimationLength("Start");
        if (animationLength > 0)
        {
            Invoke(nameof(LoadNextScene), animationLength);
        }
        else
        {
            Debug.LogWarning("Animation clip not found for trigger 'Start'.");
            LoadNextScene();
        }
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
