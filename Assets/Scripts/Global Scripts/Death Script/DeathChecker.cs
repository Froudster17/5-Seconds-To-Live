using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
