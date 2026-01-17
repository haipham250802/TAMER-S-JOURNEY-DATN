using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Coin : MonoBehaviour
{
    public Text CoinText;
    public Image ImageCoin;
    private void Start()
    {
        CoinText.text = DataPlayer.GetCoin().ToString();
    }
}
