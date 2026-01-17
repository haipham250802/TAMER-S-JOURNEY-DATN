using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
public class UI_Shop : MonoBehaviour
{
    [FoldoutGroup("GAME OBJECT")]
    public EnapManager m_EnapManager;
    [FoldoutGroup("TOP")]
    public Text Quantity_Give_Coin_Beginer_Txt;
    [FoldoutGroup("TOP")]
    public Text Quatity_Catch_Beginer_Txt;
    [FoldoutGroup("TOP")]
    public Text Price_Txt;

    [FoldoutGroup("TOP")]
    public int Quantity_Give_Coin_Beginer;
    [FoldoutGroup("TOP")]
    public int Quatity_Catch_Beginer;
    [FoldoutGroup("TOP")]
    public float Price;

    [FoldoutGroup("UI")]
    public GameObject option;

    [FoldoutGroup("MONSTER PACK")]
    public Text Quantity_Coin_Element01_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Quantity_Coin_Element02_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Quantity_Coin_Element03_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Quantity_Coin_Element04_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Quantity_Coin_Element05_Txt;

    [FoldoutGroup("MONSTER PACK")]
    public Text Price_Element01_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Price_Element02_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Price_Element03_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Price_Element04_Txt;
    [FoldoutGroup("MONSTER PACK")]
    public Text Price_Element05_Txt;

    [FoldoutGroup("MONSTER PACK")]
    public Button Element01_btn;
    [FoldoutGroup("MONSTER PACK")]
    public Button Element02_btn;
    [FoldoutGroup("MONSTER PACK")]
    public Button Element03_btn;
    [FoldoutGroup("MONSTER PACK")]
    public Button Element04_btn;
    [FoldoutGroup("MONSTER PACK")]
    public Button Element05_btn;

    [FoldoutGroup("MONSTER PACK")]
    public int Quantity_Coin_Element01;
    [FoldoutGroup("MONSTER PACK")]
    public int Quantity_Coin_Element02;
    [FoldoutGroup("MONSTER PACK")]
    public int Quantity_Coin_Element03;
    [FoldoutGroup("MONSTER PACK")]
    public int Quantity_Coin_Element04;
    [FoldoutGroup("MONSTER PACK")]
    public int Quantity_Coin_Element05;

    [FoldoutGroup("MONSTER PACK")]
    public float Price_Element01;
    [FoldoutGroup("MONSTER PACK")]
    public float Price_Element02;
    [FoldoutGroup("MONSTER PACK")]
    public float Price_Element03;
    [FoldoutGroup("MONSTER PACK")]
    public float Price_Element04;
    [FoldoutGroup("MONSTER PACK")]
    public float Price_Element05;

    public Button outButton;
    public Button InfoChestButton;

    public GameObject Ads;
    public GameObject TimeAds;
    public GameObject CoinPrefab;

    public Transform DesPos;
    public Transform PosActiveCoin;

    public AnimationCurve animCurve;
    public GameObject infoChest;

    public CHEST_NORMAL chestNormal;
    public CHEST_EPIC chestEpic;
    public CHEST_LEGEND chestLegend;

    public Currentcy_Fly currentcy_Fly_Coin;
    public Currentcy_Fly currentcy_Fly_Gem;
    public GameObject content;

    int SumCoin;
    int StartCoin;
    public bool isMoveBGDone;
    public GameObject BG_Parent;
    public Transform EndPosBG;
    public Transform StartPosBG;


    public GameObject ContentZone;

    public float Offset;
    public float Offset2;
    private void OnEnable()
    {
        isMoveBGDone = false;
    }

#if UNITY_EDITOR
    [Button("LOAD")]
    void LOAD()
    {
        /* SetQuantity(Quantity_Give_Coin_Beginer_Txt, Quantity_Give_Coin_Beginer);
         SetQuantity(Quatity_Catch_Beginer_Txt, Quatity_Catch_Beginer);

         SetQuantity(Quantity_Coin_Element01_Txt, Quantity_Coin_Element01);
         SetQuantity(Quantity_Coin_Element02_Txt, Quantity_Coin_Element02);
         SetQuantity(Quantity_Coin_Element03_Txt, Quantity_Coin_Element03);
         SetQuantity(Quantity_Coin_Element04_Txt, Quantity_Coin_Element04);
         SetQuantity(Quantity_Coin_Element05_Txt, Quantity_Coin_Element05);

         SetPrice(Price_Txt, Price);
         SetPrice(Price_Element01_Txt, Price_Element01);
         SetPrice(Price_Element02_Txt, Price_Element02);
         SetPrice(Price_Element03_Txt, Price_Element03);
         SetPrice(Price_Element04_Txt, Price_Element04);
         SetPrice(Price_Element05_Txt, Price_Element05);*/
    }
#endif

    private void Awake()
    {
        outButton.onClick.AddListener(onClickOutButton);
        InfoChestButton.onClick.AddListener(onclickInfoChest);
    }
    private void Start()
    {
        //  content.transform.localPosition = new Vector3(content.transform.localPosition.x, 0, 0);
    }
    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    Tweener tween;
    public void FollowPosCoin()
    {
        ContentZone.transform.DOLocalMoveY(Offset2, 0.5f);
    }
    public void FollowPosGem()
    {
        ContentZone.transform.DOLocalMoveY(Offset, 0.5f);
    }
    public void ResetPosShop()
    {
        ContentZone.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void MoveBackGround(Transform BG)
    {
        tween = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }
    void onclickInfoChest()
    {
        infoChest.SetActive(true);
    }
    public void onClickOutButton()
    {
        this.gameObject.SetActive(false);
        /* Ads.SetActive(true);
         TimeAds.SetActive(true);*/
        if (UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy || UI_Home.Instance.m_UIselectMap.gameObject.activeInHierarchy
            || UI_Home.Instance.m_UITeam.gameObject.activeInHierarchy || UI_Home.Instance.m_UIMerge.gameObject.activeInHierarchy)
        {
            return;
        }
        UI_Home.Instance.ActiveBag();
        UI_Home.Instance.m_UIScreen.gameObject.SetActive(true);
    }

    public void SetQuantity(Text _Text, int Quantity)
    {
        _Text.text = Quantity.ToString();
    }

    public void SetPrice(Text _Text, float Price)
    {
        _Text.text = Price.ToString() + "$";
    }

    public void MoveCoin()
    {
        StartCoin = DataPlayer.GetCoin();
        int Curcoin = DataPlayer.GetCoin();
        int Coin = SumCoin;
        int CoinPlus = SumCoin / 15;
        int SUMCOIN = DataPlayer.GetCoin() + SumCoin;

        for (int i = 0; i < 15; i++)
        {
            float offsetX = 2f;
            float offsetY = 1f;
            int index = i;
            GameObject obj = Instantiate(CoinPrefab, PosActiveCoin.transform.position, Quaternion.identity);
            obj.transform.localScale = Vector3.one;
            obj.transform.DOScale(3f, 0.3f).SetEase(animCurve);
            obj.transform.SetParent(PosActiveCoin.gameObject.transform);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            Vector3 beforeScale = Vector3.one;
            Vector3 randomPos = new Vector3(PosActiveCoin.transform.position.x + Random.Range(-offsetX, offsetX), PosActiveCoin.transform.position.y + Random.Range(-offsetY, offsetY), 0);
            obj.transform.DOMove(randomPos, 0.3f).SetEase(animCurve).OnComplete(() =>
            {
                float timeDelay = Random.Range(4, 7) * 0.05f;
                obj.transform.DOScale(2.2f, 0.2f).SetDelay(timeDelay + 0.2f);
                obj.transform.DOMove(DesPos.position, 320).SetEase(animCurve).SetSpeedBased().SetDelay(timeDelay).OnComplete(() =>
                {
                    if (this.gameObject.activeInHierarchy)
                    {
                        StartCoroutine(IE_AnimCoin(CoinPlus, StartCoin, SUMCOIN));
                    }
                    Tween tween = DesPos?.DOScale(beforeScale * 1.2f, 0.1f).OnComplete(() => { tween = DesPos?.DOScale(beforeScale, 0.05f); });
                    if (index + 1 == 15)
                    {
                        if (tween != null) tween.Kill();
                        DesPos?.DOScale(beforeScale, 0.05f);
                    }
                    Destroy(obj);
                });
            });
        }
    }

    IEnumerator IE_AnimCoin(int Coin, int StartCoin, int CurCoin)
    {
        while (StartCoin < CurCoin)
        {
            yield return null;
            StartCoin += Coin;
            DataPlayer.SetCoin(StartCoin);
            UI_Home.Instance.m_UICoinManager.SetTextCoin();
            if (StartCoin + 1 == CurCoin)
            {
                DataPlayer.SetCoin(StartCoin + SumCoin);
                UI_Home.Instance.m_UICoinManager.SetTextCoin();
            }
        }
    }
    public void ActiionFlyCoin()
    {
        currentcy_Fly_Coin.ActiveCurrency(0);
    }
    public void ActiionFlyGem()
    {
        currentcy_Fly_Gem.ActiveCurrency(0);
    }
}
