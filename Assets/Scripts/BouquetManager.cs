using UnityEngine;
using System.Collections.Generic;

public class BouquetManager : MonoBehaviour
{
    public static BouquetManager Instance { get; private set; }
    [SerializeField] private List<Transform> flowerSnapPoints;
    [SerializeField] private GameObject snapFlowerPrefab;
    private Dictionary<Transform, SnapFlowerInstance> occupiedSnaps = new();
    //dictionary memasang antara snapflowerinstance dengan titiknya (transform)
    private int pointIndex = 0;
    
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
}
