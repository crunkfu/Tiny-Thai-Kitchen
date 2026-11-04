
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    // ENCAPSULATION: Recipe data and selected ingredients
    // are managed internally by RecipeManager.
    [SerializeField] private Recipe currentRecipe;

    // UI references
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text ingredientsText;

    private HashSet<string> selectedIngredients = new HashSet<string>();

    private void Start()
    {
        UpdateOrderDisplay();
    }

    // ABSTRACTION: Other classes only need to submit
    // an ingredient. They don't need to know the recipe rules.
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

        UpdateOrderDisplay();

        if (selectedIngredients.Count ==
            currentRecipe.RequiredIngredientCount)
        {
            Debug.Log("Recipe completed: " +
                      currentRecipe.RecipeName);

            selectedIngredients.Clear();
            UpdateOrderDisplay();
        }
    }

    // ABSTRACTION: This method handles all the details
    // of converting recipe progress into UI text.
    private void UpdateOrderDisplay()
    {
        orderText.text = "CURRENT ORDER\n" +
                         currentRecipe.RecipeName;

        StringBuilder builder = new StringBuilder();

        foreach (string ingredientName in
                 currentRecipe.RequiredIngredients)
        {
            bool isSelected =
                selectedIngredients.Contains(ingredientName);

            builder.Append(isSelected ? "[X] " : "[ ] ");
            builder.AppendLine(ingredientName);
        }

        ingredientsText.text = builder.ToString();
    }
}
