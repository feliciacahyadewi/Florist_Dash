using UnityEngine;

[CreateAssetMenu(fileName = "NewPaperData", menuName = "Florist Sim/Paper Data")]
public class PaperData : ScriptableObject
{
    [SerializeField] private string paperID;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite snapSprite;
    
    public string PaperID => paperID;
    public Sprite PaperSprite => paperSprite;
    public Sprite SnapSprite => snapSprite != null ? snapSprite : paperSprite;
}
