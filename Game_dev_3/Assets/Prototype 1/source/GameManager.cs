using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 3;

    public TextMeshProUGUI livesText;

    void Start()
    {
        livesText.text = "Lives: " + lives;
    }

    public void AddLives(int value)
    {
        lives += value;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
            lives = 0;
            SceneManager.LoadScene("p2_game_over");
        }

        livesText.text = "Lives: " + lives;
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score = " + score);
    }
}
