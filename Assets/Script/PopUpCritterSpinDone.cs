using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class PopUpCritterSpinDone : MonoBehaviour
{
    public SkeletonGraphic skeletonChest;
    public Button exit;

    public ItemCritter itemCritter;
    public ItemCurrentcy itemCurrency;

    public Sprite SP_Coin_Card;
    public Sprite SP_Gem;

    public Transform EndPosCard;

    ChestReward chestRw;
    public List<GameObject> ContentList = new List<GameObject>();

    public AnimationCurve curveAnimMove;

    private void Awake()
    {
        exit.onClick.AddListener(OnclickButtonExit);
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delayActive());
        skeletonChest.gameObject.SetActive(true);
    }
    void ResetPopUp()
    {
        if(ContentList.Count > 0)
        {
            Destroy(ContentList[0].gameObject);
            ContentList.Clear();
        }
      
    }
    IEnumerator IE_delayActive()
    {
        yield return null;
    }
    public void Open()
    {
        skeletonChest.AnimationState.SetAnimation(0, "Appear", false);
        skeletonChest.AnimationState.Complete += _ =>
        {
            if(_.Animation.Name == "Appear")
            {
                skeletonChest.AnimationState.SetAnimation(0, "Open", false);
                skeletonChest.AnimationState.Complete += _ =>
                {
                    if (_.Animation.Name == "Open")
                    {
                        LoadItem();
                    }
                };
            }
        };
    }


    
    public void LoadItem()
    {
        RandItem();
        if (chestRw.typeReward == TypeReward.Coin || chestRw.typeReward == TypeReward.Gem)
        {
            var obj = Instantiate(itemCurrency);
            ItemCurrentcy item = obj.GetComponent<ItemCurrentcy>();
            if (chestRw.typeReward == TypeReward.Coin)
            {
                item.icon.sprite = SP_Coin_Card;
                int coin = chestRw.QuantityOrStar;
                UI_Home.Instance.m_UICoinManager.SetTextCoin(coin);
            }
            else
            {
                item.icon.sprite = SP_Gem;
                int Gem = chestRw.QuantityOrStar;
                UI_Home.Instance.m_UIGemManager.SetTextGem(Gem);
            }
            item.description.text = "x" + chestRw.QuantityOrStar.ToString();
            ContentList.Add(obj.gameObject);
            obj.transform.position = skeletonChest.transform.position + new Vector3(0, 70, 0);
            obj.transform.SetParent(transform);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 170);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            obj.transform.DOMove(EndPosCard.transform.position + new Vector3(0, -80, 0), 0.2f).OnStart(() =>
            {
                obj.transform.DOScale(1.5f, 0.3f);
            }).SetEase(curveAnimMove);
        }
    }
    public void RandItem()
    {
        chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestNormal);
    }
    public void OnclickButtonExit()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        ResetPopUp();
    }
}
public enum TypeSpinWheel
{
    Critter,
    Chest
}
