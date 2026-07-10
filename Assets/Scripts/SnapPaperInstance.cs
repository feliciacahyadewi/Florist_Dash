using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class SnapPaperInstance : MonoBehaviour, IPointerClickHandler
{
    public void Initialize(Sprite paperSprite)
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = paperSprite;
        SpriteScaleUtility.AdjustScale(transform, 0.012f,0.025f);
        
        var bc = GetComponent<BoxCollider2D>();
        bc.offset = Vector2.zero;
        bc.size = new Vector2(15f, 17f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BouquetManager.Instance.RemoveCurrentPaper();
        Destroy(gameObject);
    }
}
