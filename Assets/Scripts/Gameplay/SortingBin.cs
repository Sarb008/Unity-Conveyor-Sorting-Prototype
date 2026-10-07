using UnityEngine;

public class SortingBin : MonoBehaviour
{
    [SerializeField] private bool isBinA;

    private GameManager gameManager;
    private UIManager uiManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        uiManager = FindFirstObjectByType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();

        if (item == null)
            return;

        bool correct = IsCorrectBin(item.Type);

        if (correct)
        {
            Debug.Log(
                item.Type + " sorted correctly into " +
                (isBinA ? "Bin A" : "Bin B")
            );

            gameManager.AddScore(10);
        }
        else
        {
            Debug.Log(
                item.Type + " sorted incorrectly into " +
                (isBinA ? "Bin A" : "Bin B")
            );

            gameManager.AddScore(-5);
        }

        item.ShowResult(correct);
        if (uiManager != null)
        {
            uiManager.ShowFeedback(correct);
        }
    }

    private bool IsCorrectBin(Item.ItemType itemType)
    {
        if (isBinA)
        {
            return itemType == Item.ItemType.Organic;
        }

        return itemType == Item.ItemType.Recyclable ||
               itemType == Item.ItemType.Hazardous;
    }
}