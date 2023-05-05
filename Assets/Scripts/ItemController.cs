using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Transform buyParent;
    Transform sellParent;
    Transform inventoryParent;

    MoneyController moneyController;


    Transform parent;
    string parentName;

    void Awake()
    {
        moneyController = GameObject.Find("Money").GetComponent<MoneyController>();

        parent = transform.parent.parent.parent.GetComponent<Transform>();
        parentName = parent.name;

        buyParent = GameObject.Find("Buy").transform.GetChild(0).GetChild(0);
        sellParent = GameObject.Find("Sell").transform.GetChild(0).GetChild(0);
        inventoryParent = GameObject.Find("Inventory").transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        
    }

    public void MoveItem()
    {
        if (parentName == "Inventory" && sellParent.parent.parent.gameObject.activeSelf)
        {

            transform.SetParent(sellParent);
            moneyController.ChangeAmount(500);

        }
        if (parentName == "Sell")
        {
            transform.SetParent(inventoryParent);
            moneyController.ChangeAmount(-500);
        }


        if (parentName == "Buy") 
        {
            transform.SetParent(inventoryParent);
            moneyController.ChangeAmount(-500);
        }
        parent = transform.parent.parent.parent.GetComponent<Transform>();
        parentName = parent.name;
    }
}
