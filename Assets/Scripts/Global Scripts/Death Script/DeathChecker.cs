using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathChecker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
