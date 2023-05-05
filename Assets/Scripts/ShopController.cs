using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    Transform cinemaShine;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform inventory;
    [SerializeField]
    Transform shop;
    [SerializeField]
    float distanceToClose = 3;

    bool enteredShop;

    void Start()
    {
        cinemaShine.gameObject.SetActive(true);
        shop.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        enteredShop = false;
    }

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < distanceToClose && Input.GetKeyDown(KeyCode.E))
        {
            cinemaShine.gameObject.SetActive(false);
            shop.gameObject.SetActive(true);
            inventory.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            enteredShop = true;
        }
        if (distanceToPlayer > distanceToClose || Input.GetKeyDown(KeyCode.Escape))
        {
            if (enteredShop)
            {
                cinemaShine.gameObject.SetActive(true);
                shop.gameObject.SetActive(false);
                inventory.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                enteredShop = false;
            }
        }
    }
}
