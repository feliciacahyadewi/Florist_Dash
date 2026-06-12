using UnityEngine;
using System.Collections.Generic;

public class BouquetManager : MonoBehaviour
{
    public static BouquetManager Instance { get; private set; }
    [SerializeField] private List<Transform> points;
    private int pointIndex = 0;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFlower(FlowerItem flower)
    {
        if (pointIndex < points.Count)
        {
            Transform targetPoint = points[pointIndex];
            flower.transform.position = targetPoint.position;
            flower.transform.SetParent(targetPoint);
            BoxCollider2D col = flower.GetComponent<BoxCollider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            pointIndex++;
            Debug.Log("Added Flower");
        }
        else
        {
            Debug.Log("Slot is Full");
        }
    }
}
