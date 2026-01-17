using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPack : MonoBehaviour
{
    public int ID;
    public PackShop m_PackShop;
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(onclickButton);
    }
    public void onclickButton()
    {
        m_PackShop.NextPack(ID);
        m_PackShop.ActiveSelectedButton(ID);
    }
}
