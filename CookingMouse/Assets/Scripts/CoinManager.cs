using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int totalCoins;
    public TextMeshProUGUI coinText;

    public void Awake()
    {
        if(instance == null )instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        totalCoins = 0;
        UpdateCoinDisPlay();
    }

    public void AddCoins(int coinCount)
    {
        totalCoins += coinCount;
        UpdateCoinDisPlay();
    }

    public void UpdateCoinDisPlay()
    {
        coinText.text = totalCoins.ToString();
    }
}
