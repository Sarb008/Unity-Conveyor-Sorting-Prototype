using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 3f;
    private GameManager gameManager;

    private float timer;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (gameManager == null || !gameManager.GameStarted)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    private void SpawnItem()
    {
        Item newItem = Instantiate(
            itemPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Item.ItemType randomType =
            (Item.ItemType)Random.Range(0, 3);

        newItem.SetType(randomType);
    }
}