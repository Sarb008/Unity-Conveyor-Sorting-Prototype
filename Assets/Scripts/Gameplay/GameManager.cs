using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool gameStarted;

    [SerializeField] private float baseItemSpeed = 2f;
    [SerializeField] private float speedIncrease = 0.5f;
    [SerializeField] private int scorePerDifficultyIncrease = 30;
    [SerializeField] private GameObject startScreen;

    public int Score => score;
    public bool GameStarted => gameStarted;

    public float CurrentItemSpeed
    {
        get
        {
            int difficultyLevel =
                score / scorePerDifficultyIncrease;

            return baseItemSpeed +
                   difficultyLevel * speedIncrease;
        }
    }

    private void Start()
    {
        gameStarted = false;
        startScreen.SetActive(true);
    }

    public void StartGame()
    {
        gameStarted = true;
        startScreen.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (!gameStarted)
            return;

        score += amount;

        Debug.Log("Score: " + score);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        Application.Quit();
    }
}