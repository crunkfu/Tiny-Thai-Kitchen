
using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientInputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private RecipeManager recipeManager;

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
            if (hit.collider.TryGetComponent(out Ingredient ingredient))
            {
                ingredient.Interact();

                // ABSTRACTION: The input handler sends the ingredient
                // to the manager without knowing the recipe rules.
                recipeManager.SelectIngredient(ingredient);
            }
        }
    }

}
