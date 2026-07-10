using UnityEngine;

public static class SpriteScaleUtility
{
   public static void AdjustScale(Transform target, Sprite sprite, float targetSize)
   {
      float largestSide = Mathf.Max(sprite.bounds.size.x, sprite.bounds.size.y);
      float scaleFactor =targetSize / largestSide;
      Vector3 parentScale = target.parent != null ? target.parent.lossyScale : Vector3.one;
      target.localScale = new Vector3(
         scaleFactor/parentScale.x,
         scaleFactor/parentScale.y,
         scaleFactor/parentScale.z);
   }

   public static void AdjustScale(Transform target, float scaleX, float scaleY)
   {
      target.localScale = new Vector3(scaleX, scaleY, 1f);
   }
   
}
