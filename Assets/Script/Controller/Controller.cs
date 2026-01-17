using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Dragon.SDK;
public class Controller : MonoBehaviour
{
    private static Controller instance;
    public static Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Controller();
            }
            return instance;
        }
    }

    public EnemyData enemyData;
    public BossData bossData;
    public DataInapp enapData;
    public MergeElementData mergeElementData;
    public HealingData healingData;
    public DataCoinCatchance dataCoinCatchance;
    public player m_Player;
    public DataChest dataChest;
    public DataPack dataPack;
    public DataPackOnline dataPackOnline;
    public DataPackZone dataPackZone;
    public DataPickerWheel dataPickerWheel;
    public DataCoinDropBattle dataCoinDrop;
    public DataRewardsFree dataRewardsFree;
    public DataEnemyQuantityOfZone dataquantityOfZone;
    public DataStoryInfo dataStoryInfo;
    public GameObject PopUpNewUser;
    public ExampleStoryTut exampObj;

    public SkeletonGraphic skeleton;

    public List<ECharacterType> L_TypeNewUICritter;

    public string CurrentLaguage;

    public bool isMoveBoss;
    public bool isMoveDoor;

    public float CountTime;
    [SerializeField] private float maxTime;

    public GameObject ApppearFx;
    private void Awake()
    {
        instance = this;
        /*  if (!DataPlayer.GetIsNewUser())
          {
              PopUpNewUser.SetActive(true);
          }
          else
          {
              PopUpNewUser.SetActive(false);
          }*/
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            Controller.Instance.L_TypeNewUICritter.Add(ECharacterType.Tuber);
            Controller.Instance.L_TypeNewUICritter.Add(ECharacterType.Mishmash);
        }
        CurrentLaguage = DataPlayer.GetCurrentLanguage();
        SetLanguage(CurrentLaguage);
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            popUpManager.Instance.m_PopUpSeting.gameObject.SetActive(true);
            popUpManager.Instance.m_PopUpSeting.m_PopUpChangeLanguage.gameObject.SetActive(true);
        }
    }


    private void Start()
    {
        isShowInter = false;
        CountTime = 0;
        maxTime = FireBaseRemoteConfig.GetFloatConfig("time_post_inter", 30);
        Application.targetFrameRate = 250;
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            DataPlayer.ResetDataWithTurotial();
            /*  PlayerPrefs.DeleteAll();
              PlayerPrefs.Save();
              Debug.LogError("da den 0");*/
            PopUpNewUser.SetActive(true);

        }
        /*  if (!DataPlayer.GetIsNewUser())
          {
          }
          else
          {
              PopUpNewUser.SetActive(false);
          }*/
    }
    public bool isShowInter;
    private void Update()
    {
        if (CountTime < maxTime)
        {
            CountTime += Time.deltaTime;
            isShowInter = false;
        }
        else if (CountTime >= maxTime )
        {
            isShowInter = true;
        }
        
    }
    public void ShowInterstitial()
    {
        if (DataPlayer.GetEnemyCatched() >= 3 && !UI_Home.Instance.uI_Battle.gameObject.activeSelf)
        {
            Debug.Log("da show intern");
            AdStatus adStatus = SDKDGManager.Instance.AdsManager.ShowInterAdsStatus(() =>
            {
                DebugCustom.LogColor("Close Ads");
                CountTime = 0;
                isShowInter = false;
            //REsettime
            }, "Test");
            //REsettime
            CountTime = 0;
            isShowInter = false;
            switch (adStatus)
            {
                case AdStatus.NoInternet:
                    {
                        popUpManager.Instance.m_PopUpNointernet.gameObject.SetActive(true);
                        break;
                    }
            }
        }
    }
    public EnemyStat GetStatEnemy(ECharacterType enemyType)
    {
        if (enemyType != ECharacterType.NONE)
        {
            EnemyStat tmp = enemyData.EnemyStatIndex(enemyType);
            return tmp;
        }
        return null;
    }
    public StoryInfo GetStatStoryInfo(ECharacterType enemyType)
    {
        if (enemyType != ECharacterType.NONE)
        {
            StoryInfo tmp = dataStoryInfo.GetStoryInfo(enemyType);
            return tmp;
        }
        return null;
    }
    public ChestReward GetChestReward(TypeChest typechest)
    {
        if (typechest != TypeChest.NONE)
        {
            ChestReward chestRwd = dataChest.ChestRewardIndex(typechest);
            return chestRwd;
        }
        return null;
    }
    public CatchChanceStat GetCatchanceStat(int Star)
    {
        if (Star > 0)
        {
            CatchChanceStat tmp = dataCoinCatchance.CatchanceStat(Star);
            return tmp;
        }
        return null;
    }
    public void GetListPickerWheel()
    {

    }
    public BossStat GetStatBoss(EBossType bossType)
    {
        BossStat tmp = bossData.BossStatIndex(bossType);
        return tmp;
    }
    public ECharacterType GetTypeIndex(int index)
    {
        return enemyData.enemies[index].Type;
    }
    public void SetLanguage(string language)
    {
        switch (language)
        {
            case "en":
                I2.Loc.LocalizationManager.CurrentLanguage = "English";
                break;
            case "vi":
                I2.Loc.LocalizationManager.CurrentLanguage = "Vietnamese";
                break;
            case "fr":
                I2.Loc.LocalizationManager.CurrentLanguage = "French (France)";
                break;
            case "ita":
                I2.Loc.LocalizationManager.CurrentLanguage = "Italian";
                break;
            case "ger":
                I2.Loc.LocalizationManager.CurrentLanguage = "German (Germany)";
                break;
            case "por":
                I2.Loc.LocalizationManager.CurrentLanguage = "Portuguese (Portugal)";
                break;
            case "ru":
                I2.Loc.LocalizationManager.CurrentLanguage = "Russian";
                break;
            case "ja":
                I2.Loc.LocalizationManager.CurrentLanguage = "Japanese";
                break;
            case "kore":
                I2.Loc.LocalizationManager.CurrentLanguage = "Korean";
                break;
            case "es":
                I2.Loc.LocalizationManager.CurrentLanguage = "Español";
                break;
            default:
                I2.Loc.LocalizationManager.CurrentLanguage = "English";
                break;
        }
        /* CPlayerPrefs.SetString(PREF_LANGUAGE, I2.Loc.LocalizationManager.CurrentLanguage);
         CPlayerPrefs.SetString(PREF_KEY_LANGUAGE, language);*/
        // OnChangeLanguage?.Invoke();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

