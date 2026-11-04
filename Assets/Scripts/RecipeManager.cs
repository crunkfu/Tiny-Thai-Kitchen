
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    // ENCAPSULATION: Recipes and game progress are managed
    // internally, rather than being controlled by other scripts.
    [SerializeField] private List<Recipe> recipes;

    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text ingredientsText;

    private Recipe currentRecipe;
    private HashSet<string> selectedIngredients = new HashSet<string>();

    private void Start()
    {
        StartNewOrder();
    }

    // ABSTRACTION: Other classes submit an ingredient without
    // needing to understand how recipe validation works.
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

            StartNewOrder();
        }
    }

    // ABSTRACTION: This method handles everything required
    // to begin a new order.
    private void StartNewOrder()
    {
        if (recipes == null || recipes.Count == 0)
        {
            Debug.LogError("No recipes configured!");
            return;
        }

        int randomIndex = Random.Range(0, recipes.Count);

        currentRecipe = recipes[randomIndex];

        selectedIngredients.Clear();

        Debug.Log("New order: " + currentRecipe.RecipeName);

        UpdateOrderDisplay();
    }

    // ABSTRACTION: Converts recipe progress into UI text.
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
