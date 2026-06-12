using UnityEngine;

[CreateAssetMenu (fileName = "NewFlowerData", menuName = "Florist Sim/Flower Data")]
public class FlowerData : ScriptableObject
{
   [SerializeField] private string flowerID;
   [SerializeField] private string flowerName;
   [SerializeField] private Sprite flowerSprite;

   public string FlowerID => flowerID;
   public string FlowerName => flowerName;
   public Sprite FlowerSprite => flowerSprite;
}
