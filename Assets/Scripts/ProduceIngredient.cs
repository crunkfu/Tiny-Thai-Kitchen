
using UnityEngine;

// INHERITANCE: ProduceIngredient inherits from Ingredient.
public class ProduceIngredient : Ingredient
{
    // POLYMORPHISM: Override the method inherited
    // from Ingredient with specialized behaviour.
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Fresh produce selected!");
    }
}
