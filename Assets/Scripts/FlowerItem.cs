using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class FlowerItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FlowerData flowerData;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float targetSize = 1f;
    private BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        if (flowerData != null)
        {
            spriteRenderer.sprite = flowerData.FlowerSprite;
            gameObject.name = flowerData.FlowerName;
            AdjustScale();
            boxCollider.size = spriteRenderer.sprite.bounds.size;
        }
        else
        {
            Debug.Log("No Flower Data");
        }
    }

    void AdjustScale()
    {
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        float largestSide = Mathf.Max(spriteSize.x, spriteSize.y);
        float scaleFactor = targetSize / largestSide;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if (BouquetManager.Instance != null)
        {
            BouquetManager.Instance.AddFlower(this);
        }
        else
        {
            Debug.Log("No Bouquet Manager");
        }
    }
}
