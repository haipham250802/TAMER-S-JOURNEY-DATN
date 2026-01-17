using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Spine.Unity;
public class MergeElement : ElementBase
{
    [FoldoutGroup("$Type")] public string Name;
  //  [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")] public Image Avatar;
    [FoldoutGroup("$Type")] public Image MaskImg;
    [FoldoutGroup("$Type")] public GameObject Effect;

    public ElementData ThisElementData;
    public Text raritytxt;
    public Slider hpbar;
    int maxvalue;
    private void Start()
    {
        if(Type != ECharacterType.NONE)
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            Name = Type.ToString();
            Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
            Rarity = stat.Rarity;
            raritytxt.text = Rarity.ToString();
            HP = stat.HP;
            //    ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
            SetView();
        }
     
    }
    public void Init(ElementData elemendata = null)
    {
        if (elemendata != null)
        {
            Type = elemendata.Type;
            ThisElementData = elemendata;
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = elemendata.HP;
           /* hpbar.maxValue = HP;
            hpbar.value */
            Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
            Rarity = elemendata.Rarity;
         //   ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
        }
        else
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = Controller.Instance.enemyData.GetHPEmemy(Type);
            Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
            Rarity = stat.Rarity;
            //ICON = stat.ICON;
            Avatar.sprite = stat.Avatar;
        }
        maxvalue = Controller.Instance.enemyData.GetHPEmemy(Type);
        SetView();
    }
    public void SetView()
    {
        /*if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            hpbar.value = ThisElementData.HP;
            hpbar.maxValue = stat.HP;
        }
        EnemyStat stat2 = Controller.Instance.GetStatEnemy(Type);
        hpbar.value = HP;
        hpbar.maxValue = stat2.HP;

        TxtHP.text = HP.ToString();
        TxtDamage.text = Damage.ToString();*/

        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            HP = ThisElementData.HP;
          //  ID = ThisElementData.ID;
            Rarity = ThisElementData.Rarity;
            raritytxt.text = Rarity.ToString();
            setHP(maxvalue, HP);
        }
        else
        {
            HP = Controller.Instance.enemyData.GetHPEmemy(Type);
            TxtHP.text = HP.ToString();
        }
        TxtDamage.text = Damage.ToString();
    }
    public void SetIcon(Sprite Avatar)
    {
        this.Avatar.sprite = Avatar;
    }
    public void setHP(int maxvalue, int value)
    {
        hpbar.maxValue = maxvalue;
        hpbar.value = value;
    }
    public void SetViewMergeDone(ECharacterType type)
    {
        EnemyStat stat = Controller.Instance.GetStatEnemy(type);

        HP = Controller.Instance.enemyData.GetHPEmemy(type);
        Damage = Controller.Instance.enemyData.GetDamageEnemy(type);
        Rarity = stat.Rarity;
        Avatar.sprite = stat.Avatar;

        hpbar.maxValue = HP;
        hpbar.value = HP;

        TxtDamage.text = Damage.ToString();
        TxtHP.text = HP.ToString();
    }
}
