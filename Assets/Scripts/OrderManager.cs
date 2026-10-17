using UnityEngine;
using UnityEngine.UI;
using TMPro; //Text Mesh Pro 
using System.Collections;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }
    
    [SerializeField] private List<BouquetRecipeData> availableMenu;
    [SerializeField] private List<CustomerData> customerDatabase;
    
    private CustomerData currentCustomer;
    
    [SerializeField] private RectTransform customerRect;
    [SerializeField] private Image customerImage;
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TextMeshProUGUI orderText;
    
    [SerializeField] private Vector2 offScreenposition = new Vector2(1000f, 100f);
    [SerializeField] private Vector2 onScreenposition = new Vector2(0f, 100f);
    [SerializeField] private Vector2 offScreenLeftPosition = new Vector2(-1000f, 100f);
    [SerializeField] private float moveSpeed = 800f;
    [SerializeField] private Transform customerHandPosition;
    [SerializeField] private float bouquetFlySpeed =50f;
    
    private BouquetRecipeData currentOrder;
    private bool isCustomermoving = false;
    private BouquetRecipeData lastOrderedRecipe;
    private CustomerData lastCustomer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speechBubble.SetActive(false);
        customerRect.anchoredPosition = offScreenposition;
        Invoke(nameof(SpawnCustomer), 2f);
    }

    void SpawnCustomer() //mengacak RESEP BUKET BUNGA
    {
        if (availableMenu.Count == 0 || customerDatabase.Count == 0) return;

        int recipeAttempts = 0;
        do
        {
            currentOrder = availableMenu[Random.Range(0, availableMenu.Count)];
            recipeAttempts++;
        } while (availableMenu.Count > 1 && currentOrder == lastOrderedRecipe);
        
        lastOrderedRecipe = currentOrder;

        int customerAttempts = 0;
        do
        {
            currentCustomer = customerDatabase[Random.Range(0, customerDatabase.Count)];
            customerAttempts++;
        } while (customerDatabase.Count > 1 && currentCustomer == lastCustomer && customerAttempts < 20);
        
        lastCustomer = currentCustomer;

        customerImage.sprite = currentCustomer.NormalSprite;
        speechBubble.SetActive(false);
        
        customerRect.anchoredPosition = offScreenposition;
        
        StartCoroutine(MoveCustomer (onScreenposition, true));
    }

    private IEnumerator MoveCustomer(Vector2 targetPos, bool showBubbleAfter)
    {
        isCustomermoving = true;

        while (Vector2.Distance(customerRect.anchoredPosition, targetPos) > 5f)
        {
            customerRect.anchoredPosition = 
                Vector2.MoveTowards(customerRect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        customerRect.anchoredPosition = targetPos;
        isCustomermoving = false;

        if (showBubbleAfter)
        {
            orderText.text = ParseDialogue(currentCustomer.orderDialogue, currentOrder);
            speechBubble.SetActive(true);
        }
    }

    public void SubmitOrder(BouquetRecipeData submittedRecipe, Transform bouquetTransform, 
            System.Action onCorrectAccepted, System.Action onWrongReturned)
    {
        if (currentOrder == null || isCustomermoving) return;
        StartCoroutine(ProcessTransaction(submittedRecipe, bouquetTransform, onCorrectAccepted, onWrongReturned));
        
    }

    private IEnumerator ProcessTransaction(BouquetRecipeData submittedRecipe, Transform bouquetTransform, 
        System.Action onCorrectAccepted, System.Action onWrongReturned)
    {
        speechBubble.SetActive(false);
        Vector3 originalBouquetPos = bouquetTransform.position;

        while (Vector2.Distance(bouquetTransform.position, customerHandPosition.position) > 0.1f)
        {
            bouquetTransform.position = Vector3.MoveTowards(bouquetTransform.position, 
                customerHandPosition.position,  bouquetFlySpeed * Time.deltaTime);
            yield return null;
            
        }

        if (submittedRecipe == currentOrder)
        {
            orderText.text = ParseDialogue(currentCustomer.correctDialogue, currentOrder);
            customerImage.sprite = currentCustomer.HappySprite;
            speechBubble.SetActive(true);
            bouquetTransform.gameObject.SetActive(false);
            
            onCorrectAccepted?.Invoke();
            yield return new WaitForSeconds(2.5f);
            yield return StartCoroutine(MoveCustomer(offScreenLeftPosition, false));
            currentOrder = null;
            Invoke(nameof(SpawnCustomer), Random.Range(2f, 5f));
            
        } else {
            orderText.text = ParseDialogue(currentCustomer.wrongDialogue, currentOrder);
            if(currentCustomer.SadSprite != null) customerImage.sprite = currentCustomer.SadSprite;
            speechBubble.SetActive(true);
            
            yield return new WaitForSeconds(2.5f);
            
            orderText.text = ParseDialogue(currentCustomer.orderDialogue, currentOrder);
            if (currentCustomer.NormalSprite != null) customerImage.sprite = currentCustomer.NormalSprite;

            while (Vector3.Distance(bouquetTransform.position, originalBouquetPos) > 0.1f)
            {
                bouquetTransform.position = Vector3.MoveTowards(bouquetTransform.position, 
                    originalBouquetPos, bouquetFlySpeed * Time.deltaTime);
                yield return null;
            }
            
            bouquetTransform.position = originalBouquetPos;
            onWrongReturned?.Invoke();
        }
    }

    private IEnumerator CustomerLeaveRoutine()
    {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MoveCustomer(offScreenposition, false));
        currentOrder = null;
        Invoke(nameof(SpawnCustomer), 1.5f);
    }

    private string ParseDialogue(string template, BouquetRecipeData recipe)
    {
        string parsed = template;
        parsed = parsed.Replace("[Flowername]", recipe.flowerName);
        parsed = parsed.Replace("[Papercolor]", recipe.paperColor);
        return parsed;
    }
}
