using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Florist Sim/RecipeData")]
public class BouquetRecipeData : ScriptableObject
{
    [SerializeField] private PaperData requiredPaper;
    [SerializeField] private List<FlowerData> requiredFlowers;
    [SerializeField] private Sprite finalBouquetSprite;
    [TextArea] public string orderDescription;
    public string flowerName;
    public string paperColor;
    public PaperData RequiredPaper => requiredPaper;
    public List<FlowerData> RequiredFlowers => requiredFlowers;
    public Sprite FinalBouquetSprite => finalBouquetSprite;
}
