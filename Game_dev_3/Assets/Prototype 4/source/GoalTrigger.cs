using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Goal reached! Restarting level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
