using UnityEngine;
using UnityEngine.InputSystem;

public class SortingGate : MonoBehaviour
{
    private bool routeToBinA = true;

    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateVisual();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckForClick();
        }
    }

    private void CheckForClick()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        Collider2D hit =
            Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
            return;

        if (hit.gameObject != gameObject)
            return;

        routeToBinA = !routeToBinA;

        Debug.Log(
            "Gate route: " +
            (routeToBinA ? "Bin A" : "Bin B")
        );

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null)
            return;

        spriteRenderer.color =
            routeToBinA ? Color.green : Color.yellow;
    }

    public bool RoutesToBinA()
    {
        return routeToBinA;
    }
}