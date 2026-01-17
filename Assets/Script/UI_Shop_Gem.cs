using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Shop_Gem : MonoBehaviour
{
    public Button OutButton;


    private void Awake()
    {
        OutButton.onClick.AddListener(OnclickOutButton);
    }
    void OnclickOutButton()
    {
        gameObject.SetActive(false);
    }
}
