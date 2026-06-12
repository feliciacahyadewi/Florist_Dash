using UnityEngine;

public static class SpriteScaleUtility
{
   public static void AdjustScale(Transform target, Sprite sprite, float targetSize)
   {
      float largestSide = Mathf.Max(sprite.bounds.size.x, sprite.bounds.size.y);
      float scaleFactor =targetSize / largestSide;
      target.localScale = Vector3.one * scaleFactor;
   }

   public static void AdjustScale(Transform target, float scaleX, float scaleY)
   {
      target.localScale = new Vector3(scaleX, scaleY, 1f);
   }
   
}
