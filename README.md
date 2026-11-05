# Tiny Thai Kitchen 🍳

A small 3D cooking game developed in Unity as the final project for the **Unity Learn Junior Programmer Pathway**.

## About the Game

In *Tiny Thai Kitchen*, players prepare traditional Thai dishes by selecting the correct ingredients from a kitchen countertop.

Each completed order awards points, while incorrect ingredient selections result in a penalty. After completing five orders, the player receives a final score and can restart or return to the main menu.

### Available Recipes

| Dish | Ingredients |
|---|---|
| Pad Kra Pao | Chicken, Chili, Holy Basil |
| Som Tam | Papaya, Chili, Lime |
| Mango Sticky Rice | Mango, Sticky Rice, Coconut Milk |

Orders are selected randomly. Ingredients can be selected in any order, but each required ingredient counts only once per recipe.

## Controls

- **Left Mouse Button:** Select ingredients and interact with menu buttons.
- **Play:** Start the game from the main menu.
- **Restart:** Start a new game after completing five orders.
- **Main Menu:** Return to the title screen.
- **Quit:** Exit the application from the title screen.

The game uses Unity's **New Input System**, with camera raycasting to detect ingredient selection.

## Object-Oriented Programming

The project demonstrates the four pillars of object-oriented programming.

### 1. Encapsulation

**Scripts:** `Ingredient.cs`, `Recipe.cs`, `RecipeManager.cs`

Ingredient properties, recipe data, scores, and game progress are stored in private fields.

Public properties and methods provide controlled access to this information.

For example, `Ingredient.GetPointValue()` allows other classes to retrieve an ingredient's point value without directly accessing its private field.

### 2. Inheritance

**Scripts:** `ProduceIngredient.cs`, `ProteinIngredient.cs`

Both classes inherit from the `Ingredient` base class.

They reuse shared functionality while introducing specialized interaction behavior.

### 3. Polymorphism

**Scripts:** `Ingredient.cs`, `ProduceIngredient.cs`, `ProteinIngredient.cs`

The base class defines a virtual `Interact()` method, which the derived ingredient classes override.

The input handler interacts with ingredients through the common `Ingredient` type. C# executes the appropriate overridden method depending on the actual ingredient object.

### 4. Abstraction

**Scripts:** `IngredientInputHandler.cs`, `RecipeManager.cs`, `Recipe.cs`

The input handler detects an ingredient and invokes its interaction method without knowing how that ingredient implements its behavior.

Similarly, `RecipeManager.SelectIngredient()` manages recipe validation and scoring through a single public method.

Other classes do not need to know the details of recipe checking, score calculation, or game progression.

## Gameplay Features

- Three Thai recipes with randomly generated orders
- Clickable 3D ingredients
- Visual order tracking using TextMeshPro
- Duplicate ingredient detection
- Scoring and penalties
- Completion bonuses
- Five-order victory condition
- Restart and main-menu navigation
- Simple custom 3D ingredient models

### Scoring

| Action | Points |
|---|---:|
| Correct ingredient | +10 (configurable per ingredient) |
| Incorrect ingredient | −5 |
| Completed recipe | +30 bonus |

## Version Control

Development was tracked using **Git and GitHub Desktop**.

The workflow included:

1. Creating a Git repository and making the initial commit.
2. Creating the `feature/title-screen` branch.
3. Developing and committing the main-menu functionality.
4. Merging the feature branch into `main`.
5. Making additional commits during gameplay development.

Git was also used to recover an accidentally deleted Unity scene, demonstrating the practical value of version control.

## Performance Profiling

Performance was examined using the **Unity Profiler** in both the Unity Editor and a standalone Windows development build.

### Selected Profiling Results

| Measurement | Unity Editor | Standalone Development Build |
|---|---:|---:|
| CPU frame time | 9.16 ms | 2.22 ms |
| Total committed memory | 3.94 GB | 0.61 GB |
| Tracked memory in use | 3.38 GB | 335.9 MB |
| Managed heap in use | 0.82 GB | 3.2 MB |

These measurements represent individual profiling captures, not average performance benchmarks. The Editor and development build include different profiling and runtime overhead.

### Findings

- No obvious sustained CPU bottleneck was observed.
- Physics and gameplay scripting showed relatively low CPU usage in the inspected frames.
- The standalone build used substantially less memory than the Unity Editor session.
- The selected standalone memory frame reported 0 B of garbage-collection allocations.

### Performance-Conscious Design

The input handler checks for mouse-button presses each frame but only performs a physics raycast when the player clicks.

This avoids unnecessary raycasts during idle frames.

Based on the profiling results, no major code optimization was considered necessary for the current scope of the prototype.

## Technologies

- Unity 6.3
- C#
- Universal Render Pipeline (URP)
- Unity New Input System
- TextMeshPro
- Unity Profiler
- Git and GitHub Desktop

## Project Purpose

*Tiny Thai Kitchen* was created to demonstrate practical understanding of C# programming, object-oriented design, user interaction, game-state management, version control, and performance profiling.

It also serves as an early experiment toward a larger concept for an interactive Thai-language learning game.

## Development Status

**Playable prototype — Junior Programmer final project.**
