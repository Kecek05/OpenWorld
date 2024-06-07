
using UnityEngine;

public class RandomizeRecipeController : MonoBehaviour
{
    public static RandomizeRecipeController Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private void Awake()
    {
        Instance = this;
        recipeListSO.SetNewRandomRecipeList();
    }



}
