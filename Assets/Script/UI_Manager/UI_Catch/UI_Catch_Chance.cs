using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;

public class UI_Catch_Chance : MonoBehaviour
{
    public Text Chance_Txt;
    public Slider Chance_Bar;
    public Image Fill_Chance_Img;
    public SkeletonGraphic Critter_Skeleton;
    public int NumCatch;

    public Button ExitButton;
    public Button Catch_Chance;

    public Transform Pos_Critter_Of_Catch;

    public RuleController m_RuleController;
    public UI_Battle m_UIBattle;
    public Character m_Character;

    public Color LowColor;
    public Color HightColor;

    public UI_Catch m_Uicatch;
    public GameObject Bottom;

    public int JumPower;
    public int JumCount;
    public float Duration;
    private void Awake()
    {
        ExitButton.onClick.AddListener(OnclickExitButton);
        Catch_Chance.onClick.AddListener(OnclickCatchButton);
    }
    public void HiddenPopUp()
    {
        this.gameObject.SetActive(false);
    }
    public void SetTextChance(string ChanceTxt)
    {
        Chance_Txt.text = ChanceTxt;
    }
    public void SetCritterSkeleton(SkeletonDataAsset skeletonData)
    {
        this.Critter_Skeleton.skeletonDataAsset = null;
        this.Critter_Skeleton.skeletonDataAsset = skeletonData;
        this.Critter_Skeleton.Initialize(true);
    }
  
    public void SetValueBar(int MaxValue, int Value)
    {
        Chance_Bar.maxValue = MaxValue;
        Chance_Bar.value = Value;

        Image img = Fill_Chance_Img.GetComponent<Image>();
        img.color = Color.Lerp(LowColor, HightColor, Chance_Bar.normalizedValue);
        var tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
    }
    public void ShowUICatch(UI_Catch_Chance uI_Catch_Chance)
    {
        uI_Catch_Chance.gameObject.SetActive(true);
    }
    public void OnclickExitButton()
    {
        UI_Home.Instance.ActiveUIHome();
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;


        if (m_UIBattle.m_character is Enemy)
        {
            Controller.Instance.m_Player.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            m_UIBattle.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            UI_Home.Instance.ActiveBag();
        }

        else if(m_UIBattle.m_character is BossPatrol)
        {
            if (m_UIBattle.isFocusSelectMap)
            {
                UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                m_UIBattle.isFocusSelectMap = false;
            }
            m_UIBattle.RemoveBoss();
            this.gameObject.SetActive(false);
            m_UIBattle.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            UI_Home.Instance.ActiveBag();
            if (m_UIBattle.isFocusSelectMap)
            {
                UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                m_UIBattle.isFocusSelectMap = false;
            }
        }
    }
    public void OnclickCatchButton()
    {
        this.HiddenPopUp();
        this.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(false);
        m_Uicatch.SetSkeletonCritter();
        m_Uicatch.gameObject.SetActive(true);
        m_Uicatch.MovePosCritterSuck();
    }
}
