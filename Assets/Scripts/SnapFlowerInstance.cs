using UnityEngine;
using UnityEngine.EventSystems;

public class SnapFlowerInstance : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private float targetSize = 4f;
    private FlowerItem sourceFlower;
    private Transform snapPoint;
    
    public void Initialize(FlowerItem flower, Transform point, Sprite singleSprite)
    {
        
        sourceFlower = flower;
        snapPoint = point;
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = singleSprite;
        SpriteScaleUtility.AdjustScale(sr.transform, singleSprite, targetSize);
        var bc = GetComponent<BoxCollider2D>();
        bc.offset = Vector2.zero;
        bc.size = new Vector2(8f, 15f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sourceFlower.RestoreStock();
        BouquetManager.Instance.RemoveFromSnapPoint(snapPoint);
        Destroy(gameObject);
    }
    
    
}
