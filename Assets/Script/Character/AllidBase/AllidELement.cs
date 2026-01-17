using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Spine.Unity;
public class AllidELement : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public E_RangeAttack RangeAttack;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int MaxHP;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;
    [FoldoutGroup("$Type")] public int Speed;
    [FoldoutGroup("$Type")] public int ID;
    [FoldoutGroup("$Type")] public Text StarTxt;
    [FoldoutGroup("$Type")] public GameObject DeadImage;
    [FoldoutGroup("$Type")] public GameObject Container;


    public SkeletonDataAsset ICON;

    public Button PurchaseBtn;
    public Slider HP_Bar;
    public Image Avatar;
    //public Image Effect;

    public GameObject Effect;
    public ElementData ThisElementData;
    AllidBase m_allidBase;
    EnemyBased m_enemy;
    Enemy enemy;

    public Animator Anim;

    private void Start()
    {
        Effect.SetActive(false);
    }
    public void SetHP(int Value, int maxValue)
    {
        HP_Bar.maxValue = maxValue;
        HP_Bar.value = Value;
    }
    public void SetVibrate()
    {
        Anim.SetTrigger("Vibrate");

    }
    private void Update()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(iEDelay());
    }
    IEnumerator iEDelay()
    {
        yield return null;
        PurchaseBtn.GetComponent<Canvas>().sortingLayerName = "UI";
        PurchaseBtn.GetComponent<Canvas>().sortingOrder = 1000;
    }
    public void Init(ElementData elemendata = null)
    {
        if (elemendata != null)
        {
            ID = elemendata.ID;
            HP = elemendata.HP;
            ThisElementData = elemendata;
            Type = elemendata.Type;
            RangeAttack = elemendata.RangeAttack;
            Rarity = elemendata.Rarity;
        }
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
        Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
        Rarity = stat.Rarity;
        ICON = stat.ICON;
        Avatar.sprite = stat.Avatar;
        RangeAttack = stat.RangeAttack;
        SetView();
        SetHP(HP, stat.HP);
    }

    public void SetView()
    {
        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
            HP_Bar.value = ThisElementData.HP;
        }
        else
        {
            TxtHP.text = HP.ToString();
            HP_Bar.value = ThisElementData.HP;
        }
        TxtDamage.text = Damage.ToString();
        StarTxt.text = Rarity.ToString();
    }
    /* public void SpawnAllidBased()
     {
         PurchaseBtn.enabled = false;
         *//* if (UI_Home.Instance.m_UIBattle != null)
          {
              UI_Home.Instance.m_UIBattle.SpawnAllidBased(this.ICON.sprite);
          }*//*
         if (enemy.UIBattle != null)
         {
             enemy.UIBattle.GetComponent<UI_Battle>().SpawnAllidBased(this.ICON.sprite);
         }
         m_allidBase = FindObjectOfType<AllidBase>();
         m_allidBase.init(this, this.ThisElementData);

         m_allidBase.Attack(Speed, PurchaseBtn);
         //    TakeDamage(1, ThisElementData);
         CharacterManager.Instance.initListAllid(m_allidBase);
     }*/
    public void ActiveButton()
    {
        PurchaseBtn.enabled = true;
    }
}
