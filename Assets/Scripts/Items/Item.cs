using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Organic,
        Recyclable,
        Hazardous
    }

    [SerializeField] private ItemType itemType;
    [SerializeField] private float maxLifetime = 10f;

    public ItemType Type => itemType;

    private SortingGate sortingGate;
    private Transform targetBin;

    private bool hasReachedGate;
    private bool hasChosenRoute;
    private bool hasReachedBinHeight;
    private float moveSpeed;

    private void Start()
    {
        sortingGate = FindFirstObjectByType<SortingGate>();

        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            moveSpeed = gameManager.CurrentItemSpeed;
        }

        Destroy(gameObject, maxLifetime);
    }

    public void SetType(ItemType newType)
    {
        itemType = newType;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        switch (itemType)
        {
            case ItemType.Organic:
                spriteRenderer.color = Color.green;
                break;

            case ItemType.Recyclable:
                spriteRenderer.color = Color.blue;
                break;

            case ItemType.Hazardous:
                spriteRenderer.color = Color.red;
                break;
        }
    }

    private void Update()
    {
        if (sortingGate == null)
            return;

        if (!hasReachedGate)
        {
            MoveTowardsGate();
        }
        else if (!hasChosenRoute)
        {
            ChooseRoute();
        }
        else if (!hasReachedBinHeight)
        {
            MoveTowardsBinHeight();
        }
        else
        {
            MoveTowardsBin();
        }
    }

    private void MoveTowardsGate()
    {
        transform.Translate(
            Vector2.right * moveSpeed * Time.deltaTime
        );

        if (transform.position.x >= sortingGate.transform.position.x)
        {
            hasReachedGate = true;
        }
    }

    private void ChooseRoute()
    {
        hasChosenRoute = true;

        if (sortingGate.RoutesToBinA())
        {
            targetBin = GameObject.Find("Bin_A").transform;
            Debug.Log("Item routed to Bin A");
        }
        else
        {
            targetBin = GameObject.Find("Bin_B").transform;
            Debug.Log("Item routed to Bin B");
        }
    }

    private void MoveTowardsBinHeight()
    {
        float targetY = targetBin.position.y;

        float newY = Mathf.MoveTowards(
            transform.position.y,
            targetY,
            moveSpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );

        if (Mathf.Approximately(transform.position.y, targetY))
        {
            hasReachedBinHeight = true;
        }
    }

    private void MoveTowardsBin()
    {
        transform.Translate(
            Vector2.right * moveSpeed * Time.deltaTime
        );
    }

    public void ShowResult(bool correct)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = correct
                ? Color.green
                : Color.red;
        }

        Invoke(nameof(DestroyItem), 0.15f);
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}