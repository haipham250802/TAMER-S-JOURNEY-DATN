using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterElement : MonoBehaviour
{
    public ECharacterType Type;
    public int ID;
    public Text Hp_txt;
    public Text Damage_txt;
    public Text NumStar;
    public Image Avatar;
    public Slider Hp_Bar;
    public GameObject Shadow;
    public GameObject OutLineSelected;
    public GameObject NewImage;
    public Button SelectButton;
    public Image LockSprite;
    public Image avatar2;

    private void Awake()
    {
        if (SelectButton)
            SelectButton.onClick.AddListener(onCLickSelectButton);
        if (OutLineSelected)
            OutLineSelected.SetActive(false);
    }
    public void SetData(ECharacterType type)
    {
        this.Type = type;
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(type);
        LockSprite.sprite = enemyStat.Avatar;
        avatar2.sprite = enemyStat.Avatar;
        Avatar.sprite = enemyStat.Avatar;
        NumStar.text = enemyStat.Rarity.ToString();
        Hp_txt.text = Controller.Instance.enemyData.GetHPEmemy(Type).ToString();
        Damage_txt.text = Controller.Instance.enemyData.GetDamageEnemy(Type).ToString();
    }
    public void onCLickSelectButton()
    {
        if (OutLineSelected)
            OutLineSelected.SetActive(true);
        UI_Home.Instance.m_UiCritter.SetOutLine(this);
        PopUpInfoCritter m_POp = popUpManager.Instance.m_PopUpInfoCritter;
        m_POp.SetData(Type);
        popUpManager.Instance.m_PopUpInfoCritter.gameObject.SetActive(true);
    }
}
