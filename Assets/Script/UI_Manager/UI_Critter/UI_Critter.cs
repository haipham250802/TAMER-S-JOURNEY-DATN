using DG.Tweening;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UI_Critter : MonoBehaviour
{
    [FoldoutGroup("LIST")]
    public List<ECharacterType> L_CharacterTypes = Enum.GetValues(typeof(ECharacterType))
                                    .Cast<ECharacterType>()
                                    .ToList();
    [FoldoutGroup("LIST")]
    public List<ECharacterType> L_CharacterTypesRender = new List<ECharacterType>();
    [FoldoutGroup("LIST")]
    public List<int> L_SumNumberOfStars = new List<int>();
    [FoldoutGroup("LIST")]
    public List<int> L_AmountOfStar = new List<int>();
    public List<CritterElement> L_CritterElements = new List<CritterElement>();

    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_01;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_02;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_03;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_04;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_05;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_06;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_07;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_08;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_09;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_10;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_11;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_12;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_13;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_14;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_15;
    [FoldoutGroup("TEXT AMOUNT")]
    public Text TextAmount_16;

    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_01 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_02 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_03 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_04 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_05 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_06 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_07 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_08 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_09 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_10 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_11 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_12 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_13 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_14 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_15 = 0;
    [FoldoutGroup("SUM NUMBER OF STARS")]
    public int Sum_Number_Of_Star_16 = 0;

    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_01 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_02 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_03 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_04 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_05 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_06 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_07 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_08 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_09 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_10 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_11 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_12 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_13 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_14 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_15 = 0;
    [FoldoutGroup("AMOUNT OF STARS")]
    public int Amount_Of_Stars_16 = 0;

    [FoldoutGroup("GAME OBJECT")]
    public GameObject critterElement;
    [FoldoutGroup("GAME OBJECT")]
    public GameObject Content;

    [FoldoutGroup("UI GAME OBJECT")]
    public Text NumCritterTitle_Txt;

    private int Num_CurrentAmountOfCritterTilte = 0;
    private int Num_SumAmountOfStarTilte = 0;
    public Button Exit;


    public GameObject BG_Parent;
    public GameObject BGMove;

    public Transform StartPosBG;
    public Transform EndPosBG;

    public bool isMoveBGDone = true;



    /// <summary>
    /// /
    /// endhanced
    /// </summary>
    /// 

    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellviewPrefabs;
    public int numberOfCellsPerRow = 4;
    private SmallList<CritterElement> _data;
    bool IsInitilize;
    public int size;
    public void Init()
    {
        if (!IsInitilize)
        {
            IsInitilize = true;
           // scroller.Delegate = this;
        }
    }
    private void LoadData()
    {
        _data = new SmallList<CritterElement>();
        for (var i = 0; i < size; i++)
        {
            CritterElement items = new CritterElement();
            _data.Add(items);
        }
        scroller.ReloadData();
    }
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        throw new NotImplementedException();
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 121f;
    }

   /* public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
       // CritterElement items = scroller.GetCellView(cellviewPrefabs) as CritterElement;

    }*/

#if UNITY_EDITOR
    [Button("Load List Character Type Render")]
    void LoadCritter()
    {
        for (int i = 0; i < L_CharacterTypes.Count; i++)
        {
            for (int j = i + 1; j > L_CharacterTypes.Count; j++)
            {
                EnemyStat enemyStat = Controller.Instance.GetStatEnemy(L_CharacterTypes[i]);
                EnemyStat enemyStat2 = Controller.Instance.GetStatEnemy(L_CharacterTypes[j]);

                if (enemyStat.Rarity > enemyStat2.Rarity)
                {
                    int Temp = enemyStat.Rarity;
                    enemyStat.Rarity = enemyStat2.Rarity;
                    enemyStat2.Rarity = Temp;
                }
            }
        }
        for (int i = 0; i < L_CharacterTypes.Count; i++)
        {
            L_CharacterTypesRender.Add(L_CharacterTypes[i]);
        }
    }
#endif
    private void Awake()
    {
        Exit.onClick.AddListener(OnclickButtonExit);
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delayOnEnable());
    }
    IEnumerator IE_delayOnEnable()
    {
        yield return null;
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        ShowNewImg();
    }
    private void Start()
    {
        for (int i = 0; i < L_CharacterTypesRender.Count - 1; i++)
        {
            if (critterElement != null)
            {
                Num_SumAmountOfStarTilte++;

                GameObject temp = Instantiate(critterElement);
                EnemyStat enemyStat = Controller.Instance.GetStatEnemy(L_CharacterTypesRender[i]);

                temp.SetActive(false);
                temp.transform.SetParent(Content.transform);
                temp.GetComponent<CritterElement>().Type = L_CharacterTypesRender[i];
                temp.GetComponent<CritterElement>().NumStar.text = enemyStat.Rarity.ToString();
                temp.GetComponent<CritterElement>().SetData(temp.GetComponent<CritterElement>().Type);
                temp.GetComponent<CritterElement>().GetComponent<RectTransform>().sizeDelta = new Vector2(167, 190);
                temp.GetComponent<CritterElement>().GetComponent<RectTransform>().localScale = Vector3.one;
                temp.GetComponent<CritterElement>().ID = i;
                L_SumNumberOfStars.Add(enemyStat.Rarity);
                if (!L_CritterElements.Contains(temp.GetComponent<CritterElement>()))
                    L_CritterElements.Add(temp.GetComponent<CritterElement>());

                for (int j = 0; j < DataPlayer.GetListCritters().Count; j++)
                {
                    if (L_CritterElements[i].Type != DataPlayer.GetListCritters()[j])
                    {
                        // temp.GetComponent<CritterElement>().Avatar.color = Color.black;

                        temp.GetComponent<CritterElement>().Avatar.gameObject.SetActive(false);
                        temp.GetComponent<CritterElement>().LockSprite.gameObject.SetActive(true);
                        if (!temp.GetComponent<CritterElement>().Shadow.activeInHierarchy)
                        {
                            temp.GetComponent<CritterElement>().Shadow.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        StartCoroutine(IE_HIDDEN_CRITTER_AVATAR(temp.GetComponent<CritterElement>()));

                    }
                }
            }
        }

        for (int i = 0; i < L_CritterElements.Count; i++)
        {
            L_CritterElements[i].gameObject.SetActive(true);
        }
        LoadCritterInUI();
        //  StartCoroutine(IE_delayLoad2());
    }
    IEnumerator IE_delayLoad2()
    {
        yield return new WaitForSeconds(1);
        for (int i = 20; i < L_CritterElements.Count; i++)
        {
            L_CritterElements[i].gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    Tweener twenner;
    void KillTween()
    {
        twenner.Kill();
    }
    public void MoveBackGround(Transform BG)
    {
        twenner = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }

    public void OnclickButtonExit()
    {
        this.gameObject.SetActive(false);
        UI_Home.Instance.ActiveUIHome();
        UI_Home.Instance.ActiveBag();
    }
    public void ActiveCritter()
    {
        for (int i = 0; i < L_CritterElements.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListCritters().Count; j++)
            {
                if (L_CritterElements[i].Type == DataPlayer.GetListCritters()[j])
                {
                    StartCoroutine(IE_HIDDEN_CRITTER_AVATAR(L_CritterElements[i]));
                    L_CritterElements[i].GetComponent<CritterElement>().Shadow.gameObject.SetActive(false);

                }
            }
        }
    }
    public void Reset()
    {
        /*    Amount_Of_Stars_01 = 0;
            Amount_Of_Stars_02 = 0;
            Amount_Of_Stars_03 = 0;
            Amount_Of_Stars_04 = 0;
            Amount_Of_Stars_05 = 0;
            Amount_Of_Stars_06 = 0;
            Amount_Of_Stars_07 = 0;
            Amount_Of_Stars_08 = 0;
            Amount_Of_Stars_09 = 0;
            Amount_Of_Stars_10 = 0;
            Amount_Of_Stars_11 = 0;
            Amount_Of_Stars_12 = 0;
            Amount_Of_Stars_13 = 0;
            Amount_Of_Stars_14 = 0;
            Amount_Of_Stars_15 = 0;

    */
        Sum_Number_Of_Star_01 = 0;
        Sum_Number_Of_Star_02 = 0;
        Sum_Number_Of_Star_03 = 0;
        Sum_Number_Of_Star_04 = 0;
        Sum_Number_Of_Star_05 = 0;
        Sum_Number_Of_Star_06 = 0;
        Sum_Number_Of_Star_07 = 0;
        Sum_Number_Of_Star_08 = 0;
        Sum_Number_Of_Star_09 = 0;
        Sum_Number_Of_Star_10 = 0;
        Sum_Number_Of_Star_11 = 0;


    }
    public void LoadCritterInUI()
    {
        Reset();
        ResetAmountStar();
        LOAD_SUM_NUMBER_OF_STARS();
        LOAD_CURRENT_AMOUNT_STAR();
        SET_AMOUNT_OVER_SUM_Star();
        LOAD_AMOUNT_CRITTER_TILTE();
        SET_TEXT_TITLE();
    }
    public List<CritterElement> L_OutLined = new List<CritterElement>();
    public void SetOutLine(CritterElement critter)
    {
        L_OutLined.Add(critter);
        if (L_OutLined.Count > 1)
        {
            L_OutLined[L_OutLined.Count - 2].OutLineSelected.SetActive(false);
        }
    }
    private void SET_TEXT_AMOUNT(Text AmountText, string AmountStar, string SumAmountStar)
    {
        AmountText.text = AmountStar + "/" + SumAmountStar;
    }
    private void SET_TEXT_TITLE()
    {
        NumCritterTitle_Txt.text = Num_CurrentAmountOfCritterTilte + "/" + Num_SumAmountOfStarTilte;
    }
    private void SET_AMOUNT_OVER_SUM_Star()
    {
        SET_TEXT_AMOUNT(TextAmount_01, Amount_Of_Stars_01.ToString(), Sum_Number_Of_Star_01.ToString());
        SET_TEXT_AMOUNT(TextAmount_02, Amount_Of_Stars_02.ToString(), Sum_Number_Of_Star_02.ToString());
        SET_TEXT_AMOUNT(TextAmount_03, Amount_Of_Stars_03.ToString(), Sum_Number_Of_Star_03.ToString());
        SET_TEXT_AMOUNT(TextAmount_04, Amount_Of_Stars_04.ToString(), Sum_Number_Of_Star_04.ToString());
        SET_TEXT_AMOUNT(TextAmount_05, Amount_Of_Stars_05.ToString(), Sum_Number_Of_Star_05.ToString());
        SET_TEXT_AMOUNT(TextAmount_06, Amount_Of_Stars_06.ToString(), Sum_Number_Of_Star_06.ToString());
        SET_TEXT_AMOUNT(TextAmount_07, Amount_Of_Stars_07.ToString(), Sum_Number_Of_Star_07.ToString());
        SET_TEXT_AMOUNT(TextAmount_08, Amount_Of_Stars_08.ToString(), Sum_Number_Of_Star_08.ToString());
        SET_TEXT_AMOUNT(TextAmount_09, Amount_Of_Stars_09.ToString(), Sum_Number_Of_Star_09.ToString());
        SET_TEXT_AMOUNT(TextAmount_10, Amount_Of_Stars_10.ToString(), Sum_Number_Of_Star_10.ToString());
        SET_TEXT_AMOUNT(TextAmount_11, Amount_Of_Stars_11.ToString(), Sum_Number_Of_Star_11.ToString());
        SET_TEXT_AMOUNT(TextAmount_12, Amount_Of_Stars_12.ToString(), Sum_Number_Of_Star_12.ToString());
        SET_TEXT_AMOUNT(TextAmount_13, Amount_Of_Stars_13.ToString(), Sum_Number_Of_Star_13.ToString());
        SET_TEXT_AMOUNT(TextAmount_14, Amount_Of_Stars_14.ToString(), Sum_Number_Of_Star_14.ToString());
        SET_TEXT_AMOUNT(TextAmount_15, Amount_Of_Stars_15.ToString(), Sum_Number_Of_Star_15.ToString());
        SET_TEXT_AMOUNT(TextAmount_16, Amount_Of_Stars_16.ToString(), Sum_Number_Of_Star_15.ToString());
    }
    public void UpdateView(int Rarity)
    {
        switch (Rarity)
        {
            case 1:
                Amount_Of_Stars_01++;
                SET_TEXT_AMOUNT(TextAmount_01, Amount_Of_Stars_01.ToString(), Sum_Number_Of_Star_01.ToString());
                break;
            case 2:
                Amount_Of_Stars_02++;
                SET_TEXT_AMOUNT(TextAmount_02, Amount_Of_Stars_02.ToString(), Sum_Number_Of_Star_02.ToString());
                break;
            case 3:
                Amount_Of_Stars_03++;
                SET_TEXT_AMOUNT(TextAmount_03, Amount_Of_Stars_03.ToString(), Sum_Number_Of_Star_03.ToString());
                break;
            case 4:
                Amount_Of_Stars_04++;
                SET_TEXT_AMOUNT(TextAmount_04, Amount_Of_Stars_04.ToString(), Sum_Number_Of_Star_04.ToString());
                break;
            case 5:
                Amount_Of_Stars_05++;
                SET_TEXT_AMOUNT(TextAmount_05, Amount_Of_Stars_05.ToString(), Sum_Number_Of_Star_05.ToString());
                break;
            case 6:
                Amount_Of_Stars_06++;
                SET_TEXT_AMOUNT(TextAmount_06, Amount_Of_Stars_06.ToString(), Sum_Number_Of_Star_06.ToString());
                break;
            case 7:
                Amount_Of_Stars_07++;
                SET_TEXT_AMOUNT(TextAmount_07, Amount_Of_Stars_07.ToString(), Sum_Number_Of_Star_07.ToString());
                break;
            case 8:
                Amount_Of_Stars_08++;
                SET_TEXT_AMOUNT(TextAmount_08, Amount_Of_Stars_08.ToString(), Sum_Number_Of_Star_08.ToString());
                break;
            case 9:
                Amount_Of_Stars_09++;
                SET_TEXT_AMOUNT(TextAmount_09, Amount_Of_Stars_09.ToString(), Sum_Number_Of_Star_09.ToString());
                break;
            case 10:
                Amount_Of_Stars_10++;
                SET_TEXT_AMOUNT(TextAmount_10, Amount_Of_Stars_10.ToString(), Sum_Number_Of_Star_10.ToString());
                break;
            case 11:
                Amount_Of_Stars_11++;
                SET_TEXT_AMOUNT(TextAmount_11, Amount_Of_Stars_11.ToString(), Sum_Number_Of_Star_11.ToString());
                break;
            case 12:
                Amount_Of_Stars_12++;
                SET_TEXT_AMOUNT(TextAmount_12, Amount_Of_Stars_12.ToString(), Sum_Number_Of_Star_12.ToString());
                break;
            case 13:
                Amount_Of_Stars_13++;
                SET_TEXT_AMOUNT(TextAmount_13, Amount_Of_Stars_13.ToString(), Sum_Number_Of_Star_13.ToString());
                break;
            case 14:
                Amount_Of_Stars_14++;
                SET_TEXT_AMOUNT(TextAmount_14, Amount_Of_Stars_14.ToString(), Sum_Number_Of_Star_14.ToString());
                break;
            case 15:
                Amount_Of_Stars_15++;
                SET_TEXT_AMOUNT(TextAmount_15, Amount_Of_Stars_15.ToString(), Sum_Number_Of_Star_15.ToString());
                break;
            case 16:
                Amount_Of_Stars_16++;
                SET_TEXT_AMOUNT(TextAmount_16, Amount_Of_Stars_16.ToString(), Sum_Number_Of_Star_16.ToString());
                break;
        }
        Num_CurrentAmountOfCritterTilte++;
    }
    public void LOAD_SUM_NUMBER_OF_STARS()
    {
        for (int i = 0; i < L_SumNumberOfStars.Count; i++)
        {
            switch (L_SumNumberOfStars[i])
            {
                case 1:
                    Sum_Number_Of_Star_01 += 1;
                    break;
                case 2:
                    Sum_Number_Of_Star_02 += 1;
                    break;
                case 3:
                    Sum_Number_Of_Star_03 += 1;
                    break;
                case 4:
                    Sum_Number_Of_Star_04 += 1;
                    break;
                case 5:
                    Sum_Number_Of_Star_05 += 1;
                    break;
                case 6:
                    Sum_Number_Of_Star_06 += 1;
                    break;
                case 7:
                    Sum_Number_Of_Star_07 += 1;
                    break;
                case 8:
                    Sum_Number_Of_Star_08 += 1;
                    break;
                case 9:
                    Sum_Number_Of_Star_09 += 1;
                    break;
                case 10:
                    Sum_Number_Of_Star_10 += 1;
                    break;
                case 11:
                    Sum_Number_Of_Star_11 += 1;
                    break;
                case 12:
                    Sum_Number_Of_Star_12 += 1;
                    break;
                case 13:
                    Sum_Number_Of_Star_13 += 1;
                    break;
                case 14:
                    Sum_Number_Of_Star_14 += 1;
                    break;
                case 15:
                    Sum_Number_Of_Star_15 += 1;
                    break;
            }
        }
    }
    public void LOAD_AMOUNT_CRITTER_TILTE()
    {
        Num_CurrentAmountOfCritterTilte = DataPlayer.GetListCritters().Count;
    }
    void ResetAmountStar()
    {
        Amount_Of_Stars_01 = 0;
        Amount_Of_Stars_02 = 0;
        Amount_Of_Stars_03 = 0;
        Amount_Of_Stars_04 = 0;
        Amount_Of_Stars_05 = 0;
        Amount_Of_Stars_06 = 0;
        Amount_Of_Stars_07 = 0;
        Amount_Of_Stars_08 = 0;
        Amount_Of_Stars_09 = 0;
        Amount_Of_Stars_10 = 0;
        Amount_Of_Stars_11 = 0;
    }
    public void LOAD_CURRENT_AMOUNT_STAR()
    {
        ResetAmountStar();
        for (int i = 0; i < DataPlayer.GetListCritters().Count; i++)
        {
            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListCritters()[i]);
            switch (enemyStat.Rarity)
            {
                case 1:
                    Amount_Of_Stars_01++;
                    break;
                case 2:
                    Amount_Of_Stars_02++;
                    break;
                case 3:
                    Amount_Of_Stars_03++;
                    break;
                case 4:
                    Amount_Of_Stars_04++;
                    break;
                case 5:
                    Amount_Of_Stars_05++;
                    break;
                case 6:
                    Amount_Of_Stars_06++;
                    break;
                case 7:
                    Amount_Of_Stars_07++;
                    break;
                case 8:
                    Amount_Of_Stars_08++;
                    break;
                case 9:
                    Amount_Of_Stars_09++;
                    break;
                case 10:
                    Amount_Of_Stars_10++;
                    break;
                case 11:
                    Amount_Of_Stars_11++;
                    break;
                case 12:
                    Amount_Of_Stars_12++;
                    break;
                case 13:
                    Amount_Of_Stars_13++;
                    break;
                case 14:
                    Amount_Of_Stars_14++;
                    break;
                case 15:
                    Amount_Of_Stars_15++;
                    break;
                case 16:
                    Amount_Of_Stars_16++;
                    break;
            }
        }
    }
    public void ShowNewImg()
    {


        for (int i = 0; i < L_CritterElements.Count; i++)
        {
            for (int j = 0; j < Controller.Instance.L_TypeNewUICritter.Count; j++)
            {
                if (L_CritterElements[i].Type == Controller.Instance.L_TypeNewUICritter[j])
                {
                    Debug.LogError("merge ra: " + L_CritterElements[i].Type);
                    L_CritterElements[i].NewImage.SetActive(true);
                }
            }
        }
    }
    IEnumerator IE_HIDDEN_CRITTER_AVATAR(CritterElement critterElement)
    {
        yield return null;
        critterElement.Avatar.color = Color.white;

        critterElement.Avatar.gameObject.SetActive(true);
        critterElement.LockSprite.gameObject.SetActive(false);
        critterElement.Shadow.SetActive(false);
    }
    private void OnDisable()
    {
        DataPlayer.SetNewCritter(false);

        if (L_CharacterTypes != null)
        {
            Controller.Instance.L_TypeNewUICritter.Clear();
            for (int i = 0; i < L_CritterElements.Count; i++)
            {
                L_CritterElements[i].NewImage.SetActive(false);
            }
        }
    }

   
}
