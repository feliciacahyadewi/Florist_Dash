using UnityEngine;
using UnityEngine.EventSystems;

public class PaperItem : ShopItem
{
   [SerializeField] private PaperData paperData;
   public PaperData PData => paperData;
   
   protected override Sprite GetSprite() => paperData.PaperSprite;
   protected override string GetItemName() => paperData.PaperID;

   public override void OnPointerClick(PointerEventData eventData)
   {
       BouquetManager.Instance?.SelectPaper(this);
   }
   
}
