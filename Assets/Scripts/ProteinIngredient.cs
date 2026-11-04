
using UnityEngine;

// INHERITANCE: ProteinIngredient inherits from Ingredient.
public class ProteinIngredient : Ingredient
{
    // POLYMORPHISM: Override the inherited Interact() method
    // to give protein ingredients their own behaviour.
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Protein ingredient selected!");
    }
}
