
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    // ENCAPSULATION: Recipe data is stored in private fields.
    [SerializeField] private string recipeName;
    [SerializeField] private List<string> requiredIngredients;

    // ENCAPSULATION: Other classes can read recipe data
    // without directly modifying the private fields.
    public string RecipeName => recipeName;

    // ENCAPSULATION: Exposes read-only access to the ingredient list.
    // The underlying list remains private.
    public IReadOnlyList<string> RequiredIngredients =>
        requiredIngredients;

    // ABSTRACTION: Other classes can ask whether an ingredient
    // belongs to this recipe without accessing the list directly.
    public bool RequiresIngredient(string ingredientName)
    {
        return requiredIngredients != null &&
               requiredIngredients.Contains(ingredientName);
    }

    public int RequiredIngredientCount =>
        requiredIngredients?.Count ?? 0;
}
