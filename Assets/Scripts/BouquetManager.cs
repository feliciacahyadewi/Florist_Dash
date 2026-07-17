using UnityEngine;
using System.Collections.Generic;

public class BouquetManager : MonoBehaviour
{
    //FLOWER n PAPERS
    public static BouquetManager Instance { get; private set; }
    [SerializeField] private GameObject craftingTable;
    [SerializeField] private SpriteRenderer finalBouquetDisplay;
    [SerializeField] private List<BouquetRecipeData> bouquetRecipes;
    [SerializeField] private List<Transform> flowerSnapPoints;
    [SerializeField] private Transform paperSnapPoint;
    [SerializeField] private GameObject snapFlowerPrefab;
    [SerializeField] private GameObject snapPaperPrefab;
    private GameObject currentPaper;
    private Dictionary<Transform, SnapFlowerInstance> occupiedSnaps = new();
    //dictionary memasang antara snapflowerinstance dengan titiknya (transform)
    private int pointIndex = 0;
    
    public PaperData CurrentSelectedPaper { get; private set; }
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFlower(FlowerItem flower)
    {
        Transform freePoint = null;
        foreach (var point in flowerSnapPoints)
        {
            if (!occupiedSnaps.ContainsKey(point))
            {
                freePoint = point;
                break;
            }
        }

        if (freePoint == null)
        {
            Debug.Log("Slot is full");
            return;
        }
        
        GameObject instance = Instantiate(snapFlowerPrefab, freePoint.position, Quaternion.identity, freePoint);
        SnapFlowerInstance snapFlower = instance.GetComponent<SnapFlowerInstance>();
        
        Sprite singleSprite = flower.Data.SingleFlowerSprite != null 
            ? flower.Data.SingleFlowerSprite : flower.Data.FlowerSprite;
        
        snapFlower.Initialize(flower, freePoint, singleSprite);
        occupiedSnaps[freePoint] = snapFlower;
    }

    public void RemoveFromSnapPoint(Transform point)
    {
        if (occupiedSnaps.ContainsKey(point))
        {
            occupiedSnaps.Remove(point);
        }
    }

    public void SelectPaper(PaperItem paper)
    {
        if (currentPaper != null)
        {
            Destroy(currentPaper);
        }
        
        currentPaper = Instantiate(snapPaperPrefab, paperSnapPoint.position, Quaternion.identity, paperSnapPoint);
        SnapPaperInstance snapPaper = currentPaper.GetComponent<SnapPaperInstance>();
        snapPaper.Initialize(paper.PData.SnapSprite);
        CurrentSelectedPaper = paper.PData;
    }

    public void RemoveCurrentPaper()
    {
        if (currentPaper != null) Destroy(currentPaper);
        currentPaper = null;
        CurrentSelectedPaper = null;
    }

    public void ConvertToFinalAsset()
    {
        BouquetRecipeData matchedRecipe = FindMatchingRecipe();
        if (matchedRecipe != null)
        {
            craftingTable.SetActive(false);
            finalBouquetDisplay.sprite = matchedRecipe.FinalBouquetSprite;
            SpriteScaleUtility.AdjustScale(finalBouquetDisplay.transform, matchedRecipe.FinalBouquetSprite, 6f);
            finalBouquetDisplay.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No matching recipe found");
        }
    }

    private BouquetRecipeData FindMatchingRecipe()
    {
        if (CurrentSelectedPaper == null) return null;
        List<FlowerData> placedFlowers = new List<FlowerData>();
        foreach (var snap in occupiedSnaps.Values)
        {
            placedFlowers.Add(snap.FData);
        }

        foreach (BouquetRecipeData recipe in bouquetRecipes)
        {
            if (recipe.RequiredPaper != CurrentSelectedPaper) continue;
            if (recipe.RequiredFlowers.Count != placedFlowers.Count) continue;
            
            List<FlowerData> tempRequired = new List<FlowerData>(recipe.RequiredFlowers);
            bool isMatch = true;

            foreach (FlowerData placed in placedFlowers)
            {
                if (!tempRequired.Contains(placed))
                {
                    tempRequired.Remove(placed);
                }
                else
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch && tempRequired.Count == 0)
            {
                return recipe;
            }
        }
        
        return null;
    }
}
