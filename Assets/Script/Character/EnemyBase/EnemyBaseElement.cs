using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;
public class EnemyBaseElement : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public TypeEnemy TypeEnemy;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;
    [FoldoutGroup("$Type")] public int ID;
    [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")] public Image Avatar;
    [FoldoutGroup("$Type")] public Image DeadImg;
    [FoldoutGroup("$Type")] public Slider HP_bar;
    [FoldoutGroup("$Type")] public GameObject Effect;
    [FoldoutGroup("$Type")] public Text StarTxt;

    public ElementData ThisElementData;

    private void Start()
    {
        Init();
        Effect.SetActive(false);
    }
    public void SetHP(int value, int maxValue)
    {
        HP_bar.value = value;
        HP_bar.maxValue = maxValue;
    }
    public void Init(ElementData elemendata = null)
    {
        if (!DataPlayer.GetIsCheckDoneTutorial() && TypeEnemy == TypeEnemy.Boss)
        {
            HP = 5000;
            Damage = 1000;
            SetHP(5000, 5000);
            TxtDamage.text = Damage.ToString();
            TxtHP.text = HP.ToString();
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            ICON = stat.ICON;
            Rarity = stat.Rarity;
            StarTxt.text = Rarity.ToString();
            Avatar.sprite = stat.Avatar;

        }
        else
        {
            if (elemendata != null)
            {
                ID = elemendata.ID;
                HP = elemendata.HP;
                ThisElementData = elemendata;
                Type = elemendata.Type;
            }
            if (TypeEnemy == TypeEnemy.Soldier)
            {
                EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
                Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
                Rarity = stat.Rarity;
                ICON = stat.ICON;
                Avatar.sprite = stat.Avatar;
                HP = Controller.Instance.enemyData.GetHPEmemy(Type);
            }
            else if (TypeEnemy == TypeEnemy.Boss)
            {
                EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
                Damage = stat.EnocunterATK;
                Rarity = stat.Rarity;
                ICON = stat.ICON;
                Avatar.sprite = stat.Avatar;
                HP = Controller.Instance.enemyData.GetCounterHP(Type);
            }

            SetView();
        }

    }
    public void SetView()
    {
        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            HP_bar.value = ThisElementData.HP;

            if (ThisElementData.HP < 0)
            {
                HP_bar.value = 0;
                TxtHP.text = 0.ToString();
            }
        }
        else
        {
            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
            TxtHP.text = HP.ToString();
            if (TypeEnemy == TypeEnemy.Soldier)
            {
                SetHP(HP, Controller.Instance.enemyData.GetHPEmemy(Type));
            }
            else if (TypeEnemy == TypeEnemy.Boss)
            {
                SetHP(HP, Controller.Instance.enemyData.GetCounterHP(Type));
            }
            /*            Debug.Log(Type + ": " + Controller.Instance.enemyData.GetHPEmemy(Type));
            */
        }
        DeadImg.gameObject.SetActive(false);
        TxtDamage.text = Damage.ToString();
        StarTxt.text = Rarity.ToString();
    }
    public void UpdateView(EnemyBased enemyBased)
    {
        this.TxtHP.text = enemyBased.HP.ToString();
    }
}
