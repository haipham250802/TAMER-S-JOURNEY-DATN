using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;

public class Element : ElementBase
{
    public int ID;
    public Button PurchaseBtn;
    public SkeletonDataAsset ICON;
    public Image Avatar;
    public ElementData ThisElementData;
    public Image NewImg;
    public GameObject Efect;

    public Slider HP_Bar;
    public Text NumStar_Txt;
    int maxvalue;

    bool isActiveBtnTeam, isActiveBtnMerge;

    private void OnEnable()
    {
        if (DataPlayer.GetIsCheckAddToSlot())
        {
            Destroy(PurchaseBtn.GetComponent<GraphicRaycaster>());
            Destroy(PurchaseBtn.GetComponent<Canvas>());
        }
        else
        {
            PurchaseBtn.GetComponent<Canvas>().sortingOrder = 10002;
        }
    }

    public void activeBtnTeam()
    {
        isActiveBtnTeam = true;
        isActiveBtnMerge = false;
    }
    public void activeBtnMerge()
    {
        isActiveBtnTeam = false;
        isActiveBtnMerge = true;
    }
    public void Init(ElementData elemendata = null)
    {
        if (PurchaseBtn != null)
        {
            if (UI_Home.Instance.m_UICollection != null)
            {
                if (UI_Home.Instance.m_UICollection.IsUIteam)
                {
                    PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);
                }
                else if (UI_Home.Instance.m_UICollection.IsUIMerge)
                {
                    PurchaseBtn.onClick.AddListener(LoadDataButton);
                }
            }
        }

        if (elemendata != null)
        {
            ID = elemendata.ID;
            ThisElementData = elemendata;
            Type = elemendata.Type;
            Rarity = elemendata.Rarity;
            HP = elemendata.HP;

        }
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);

        Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
        Rarity = stat.Rarity;
        ICON = stat.ICON;
        Avatar.sprite = stat.Avatar;
        maxvalue = Controller.Instance.enemyData.GetHPEmemy(Type);
        SetView();
    }
    public void setHP(int maxvalue, int value)
    {
        HP_Bar.maxValue = maxvalue;
        HP_Bar.value = value;
    }
    public void SetView()
    {
        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            HP = ThisElementData.HP;
            ID = ThisElementData.ID;
            Rarity = ThisElementData.Rarity;
            NumStar_Txt.text = Rarity.ToString();
            setHP(maxvalue, HP);
        }
        else
        {
            TxtHP.text = HP.ToString();
        }
        TxtDamage.text = Damage.ToString();
    }
    void LoadDataButton()
    {
        if (!DataPlayer.GetIsCheckButtonOnMerge())
            UI_Home.Instance.m_UIMerge.Eventory.transform.GetChild(1).GetComponent<Element>().PurchaseBtn.interactable = true;

        if (NewImg.gameObject.activeInHierarchy)
        {
            NewImg.gameObject.SetActive(false);
            for (int i = 0; i < UI_Home.Instance.m_UIMerge.L_newElement.Count; i++)
            {
                if (UI_Home.Instance.m_UIMerge.L_newElement[i].Type == Type &&
                    UI_Home.Instance.m_UIMerge.L_newElement[i].ID == ID)
                {
                    UI_Home.Instance.m_UIMerge.L_newElement.RemoveAt(i);
                }
            }
        }
        UI_Home.Instance.m_UIMerge.ADD_SLOT(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }

    public void Load_Data_UI_Team()
    {
        Debug.Log("da ban");
        /* if(UI_Home.Instance.m_UIMerge.slot1 != null)
         {
             UI_Home.Instance.m_UIMerge.Eventory.transform.GetChild(1).GetComponent<Element>().PurchaseBtn.interactable = true;
         }*/
        if (NewImg.gameObject.activeInHierarchy)
        {
            NewImg.gameObject.SetActive(false);
            for (int i = 0; i < UI_Home.Instance.m_UIMerge.L_newElement.Count; i++)
            {
                if (UI_Home.Instance.m_UIMerge.L_newElement[i].Type == Type &&
                    UI_Home.Instance.m_UIMerge.L_newElement[i].ID == ID)
                {
                    UI_Home.Instance.m_UIMerge.L_newElement.RemoveAt(i);
                }
            }
        }
        UI_Home.Instance.m_UITeam.ADD_SLOT_ELEMENT_TEAM(this, success =>
        {
            this.gameObject.SetActive(!success);
            NewImg.gameObject.SetActive(false);
        //    TutorialManager.Instance.IsTapToAllidOnSlotTeam = true;
        });
    }
    public void setIcon(Sprite Avatar)
    {
        this.Avatar.sprite = Avatar;
    }
    private void OnDisable()
    {
        if (NewImg.gameObject.activeInHierarchy)
        {
            NewImg.gameObject.SetActive(false);
            for (int i = 0; i < UI_Home.Instance.m_UIMerge.L_newElement.Count; i++)
            {
                if (UI_Home.Instance.m_UIMerge.L_newElement[i].Type == Type &&
                    UI_Home.Instance.m_UIMerge.L_newElement[i].ID == ID)
                {
                    UI_Home.Instance.m_UIMerge.L_newElement.RemoveAt(i);
                }
            }
        }
    }
}
