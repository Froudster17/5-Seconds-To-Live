using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isActive = false;

    private void Start()
    {
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        while (true)
        {
            isActive = true;
            boxCollider2D.enabled = true;
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(maxTime);

            isActive = false;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(maxTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
