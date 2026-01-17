using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RateStarButton : MonoBehaviour
{
    public int id;
    public PopUpRate m_PopUprate;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(OnclickSetId);
    }
    public void OnclickSetId()
    {
        m_PopUprate.ID = id;
        m_PopUprate.SetSelectStar();
    }
   
}
