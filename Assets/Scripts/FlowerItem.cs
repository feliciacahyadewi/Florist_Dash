using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class FlowerItem : MonoBehaviour
{
    [SerializeField] private FlowerData flowerData;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Start()
    {
        if (flowerData != null)
        {
            spriteRenderer.sprite = flowerData.FlowerSprite;
            gameObject.name = flowerData.FlowerName;
        }
        else
        {
            Debug.Log("No Flower Data");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
