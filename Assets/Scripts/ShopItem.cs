using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public abstract class ShopItem : MonoBehaviour, IPointerClickHandler
//abstract, tdk bisa digunakan scr langsung, base dr class lain
{
    [SerializeField] private float targetSize = 1f;
    protected SpriteRenderer sr;
    protected BoxCollider2D bc;
    private Vector3 originalScale;
    //variable
    protected abstract Sprite GetSprite();
    protected abstract string GetItemName();
    public abstract void OnPointerClick(PointerEventData eventData);

    //method
    protected virtual void Awake()
    {
        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    protected virtual void Start()
    {
        if  (GetSprite() != null)
        {
            sr.sprite = GetSprite();
            gameObject.name = GetItemName();
            SpriteScaleUtility.AdjustScale(transform,sr.sprite,targetSize);
            bc.offset = Vector2.zero;
            bc.size = new Vector2(10f,10f);
        }
        else
        {
            Debug.Log("No Sprite");
        }
    }
}