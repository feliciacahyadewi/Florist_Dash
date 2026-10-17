using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomerData", menuName = "Florist Sim/Customer Data")]
public class CustomerData : ScriptableObject
{
    [SerializeField] private string customerId;
    [SerializeField] private string customerName;
    
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Sprite sadSprite;
    
    [TextArea] public string orderDialogue;
    [TextArea] public string correctDialogue;
    [TextArea] public string wrongDialogue;
    
    public string CustomerId => customerId;
    public string CustomerName => customerName;
    public Sprite NormalSprite => normalSprite;
    public Sprite HappySprite => happySprite;
    public Sprite SadSprite => sadSprite;
}
