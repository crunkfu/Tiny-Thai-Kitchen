
using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckForIngredient();
        }
    }

    private void CheckForIngredient()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Clicked on: " + hit.collider.gameObject.name);
        }
    }
}
