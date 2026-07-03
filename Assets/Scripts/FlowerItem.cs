using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerItem : ShopItem
{
    [SerializeField] private FlowerData flowerData;
    public FlowerData Data=> flowerData;
    
    [SerializeField] private int stock = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override Sprite GetSprite() => flowerData?.FlowerSprite;
    protected override string GetItemName() => flowerData?.FlowerName;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (stock <= 0)
        {
            Debug.Log("No stock");
            return;
        }
        
        BouquetManager.Instance?.AddFlower(this);
    }

    public void ReduceStock()
    {
    }
    
    public void RestoreStock()
    {
    }
}
