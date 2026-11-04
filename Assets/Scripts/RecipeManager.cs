
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private Recipe currentRecipe;

    // ENCAPSULATION: Only this class can modify the
    // collection of ingredients selected by the player.
    private HashSet<string> selectedIngredients = new HashSet<string>();

    // ABSTRACTION: Other classes simply submit an ingredient.
    // They don't need to know how recipe validation works.
    public void SelectIngredient(Ingredient ingredient)
    {
        string ingredientName = ingredient.IngredientName;

        if (!currentRecipe.RequiresIngredient(ingredientName))
        {
            Debug.Log("Wrong ingredient: " + ingredientName);
            return;
        }

        if (!selectedIngredients.Add(ingredientName))
        {
            Debug.Log("Already selected: " + ingredientName);
            return;
        }

        Debug.Log("Correct ingredient: " + ingredientName);

        if (selectedIngredients.Count ==
            currentRecipe.RequiredIngredientCount)
        {
            Debug.Log("Recipe completed: " +
                      currentRecipe.RecipeName);

            selectedIngredients.Clear();
        }
    }
}
