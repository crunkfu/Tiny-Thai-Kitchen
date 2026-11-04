
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    // ENCAPSULATION: Game settings are private but
    // configurable through the Inspector.
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private int completionBonus = 30;
    [SerializeField] private int wrongIngredientPenalty = 5;
    [SerializeField] private int ordersToWin = 5;
    [SerializeField] private float orderDelay = 2f;

    // UI references
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private TMP_Text ingredientsText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text finalScoreText;

    // ENCAPSULATION: Game state is managed internally.
    private Recipe currentRecipe;
    private HashSet<string> selectedIngredients =
        new HashSet<string>();

    private int score = 0;
    private int completedOrders = 0;
    private bool isAcceptingInput = true;

    public int Score => score;

    private void Start()
    {
        messageText.text = "";
        finalScoreText.text = "";

        UpdateScoreDisplay();
        StartNewOrder();
    }

    // ABSTRACTION: The input handler submits an ingredient.
    // It doesn't need to understand recipes or scoring.
    public void SelectIngredient(Ingredient ingredient)
    {
        if (!isAcceptingInput || currentRecipe == null)
        {
            return;
        }

        string ingredientName = ingredient.IngredientName;

        if (!currentRecipe.RequiresIngredient(ingredientName))
        {
            Debug.Log("Wrong ingredient: " + ingredientName);
            AddScore(-wrongIngredientPenalty);
            return;
        }

        if (!selectedIngredients.Add(ingredientName))
        {
            Debug.Log("Already selected: " + ingredientName);
            return;
        }

        Debug.Log("Correct ingredient: " + ingredientName);

        // ENCAPSULATION: Retrieve points through the
        // ingredient's public method.
        AddScore(ingredient.GetPointValue());

        UpdateOrderDisplay();

        if (selectedIngredients.Count ==
            currentRecipe.RequiredIngredientCount)
        {
            AddScore(completionBonus);
            completedOrders++;

            StartCoroutine(CompleteOrder());
        }
    }

    // ABSTRACTION: Handles the transition between orders.
    private IEnumerator CompleteOrder()
    {
        isAcceptingInput = false;

        messageText.text = "ORDER COMPLETE!\n+" +
                           completionBonus + " bonus points";

        yield return new WaitForSeconds(orderDelay);

        messageText.text = "";

        if (completedOrders >= ordersToWin)
        {
            EndGame();
        }
        else
        {
            StartNewOrder();
            isAcceptingInput = true;
        }
    }

    // ABSTRACTION: Handles the game's victory state.
    private void EndGame()
    {
        orderText.text = "";
        ingredientsText.text = "";

        finalScoreText.text =
            "KITCHEN CLOSED!\n" +
            "Orders completed: " + completedOrders + "\n" +
            "Final score: " + score;

        Debug.Log("Game completed! Final score: " + score);
    }

    private void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }

    // ABSTRACTION: Handles the creation of a new order.
    private void StartNewOrder()
    {
        if (recipes == null || recipes.Count == 0)
        {
            Debug.LogError("No recipes configured!");
            isAcceptingInput = false;
            return;
        }

        int randomIndex = Random.Range(0, recipes.Count);

        currentRecipe = recipes[randomIndex];
        selectedIngredients.Clear();

        Debug.Log("New order: " + currentRecipe.RecipeName);

        UpdateOrderDisplay();
    }

    // ABSTRACTION: Converts recipe data into readable UI.
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
