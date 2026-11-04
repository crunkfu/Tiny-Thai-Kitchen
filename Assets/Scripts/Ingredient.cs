
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // ENCAPSULATION: Private fields with controlled access.
    [SerializeField] private string ingredientName;
    [SerializeField] private int pointValue = 10;

    // ENCAPSULATION: Read-only public property.
    public string IngredientName => ingredientName;

    // ABSTRACTION: Other classes can call Interact()
    // without knowing how the interaction works internally.
    // POLYMORPHISM: Child classes can override this method.
    public virtual void Interact()
    {
        Debug.Log("Selected ingredient: " + ingredientName);
    }

    // ENCAPSULATION: Controlled access to a private field.
    public int GetPointValue()
    {
        return pointValue;
    }
}
