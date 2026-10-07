using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI feedbackText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        feedbackText.text = "";
    }

    private void Update()
    {
        if (gameManager != null)
        {
            scoreText.text = "Score: " + gameManager.Score;
        }
    }

    public void ShowFeedback(bool correct)
    {
        feedbackText.text =
            correct ? "CORRECT! +10" : "WRONG! -5";

        CancelInvoke(nameof(ClearFeedback));
        Invoke(nameof(ClearFeedback), 0.5f);
    }

    private void ClearFeedback()
    {
        feedbackText.text = "";
    }
}