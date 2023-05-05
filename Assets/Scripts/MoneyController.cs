using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField]
    Text text;

    int amount;
    void Awake()
    {
        amount = PlayerPrefs.GetInt("Money");
    }

    void Update()
    {

        text.text = amount.ToString();
    }

    public void ChangeAmount(int n)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + n);
        amount = PlayerPrefs.GetInt("Money");
    }
}
