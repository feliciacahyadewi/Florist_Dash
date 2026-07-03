using UnityEngine;

[CreateAssetMenu (fileName = "NewFlowerData", menuName = "Florist Sim/Flower Data")]
public class FlowerData : ScriptableObject
{
   [SerializeField] private string flowerID;
   [SerializeField] private string flowerName;
   [SerializeField] private Sprite flowerSprite;
   [SerializeField] private Sprite singleFlowerSprite;
   //[SerializeField] private int maxStock = 5;

   public string FlowerID => flowerID;
   public string FlowerName => flowerName;
   public Sprite FlowerSprite => flowerSprite;
   public Sprite SingleFlowerSprite => singleFlowerSprite;
   //public int MaxStock => maxStock;
}
