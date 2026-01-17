using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System;
using Newtonsoft.Json;

public static class DataPlayer
{
    private const string ALL_DATA = "ALL_DATA";
    private const string ALL_DATA_ALLID = "ALL_DATA_ALLID";
    private const string ALL_DATA_ALLID_MERGE = "ALL_DATA_ALLID_MERGE";
    private const string ALL_DATA_COIN = "ALL_DATA_COIN";
    private const string ALL_DATA_GEM = "ALL_DATA_GEM";
    private const string ALL_DATA_POINT = "ALL_DATA_POINT";
    private const string ALL_DATA_CRITTERS = "ALL_DATA_CRITTERS";
    private const string ALL_DATA_LEVEL = "ALL_DATA_LEVEL";
    private const string ALL_DATA_SOUND = "ALL_DATA_SOUND";
    private const string ALL_DATA_TUTORIAL = "ALL_DATA_TUTORIAL";
    private const string ALL_DATA_BUY_CHEST = "ALL_DATA_BUY_CHEST";
    private const string ALL_DATA_RATED = "ALL_DATA_RATED";
    private const string ALL_DATA_PACK_SHOP = "ALL_DATA_PACK";
    private const string ALL_DATA_PACK_LOGIN = "ALL_DATA_PACK_LOGIN";
    private const string ALL_DATA_PACK_ZONE = "ALL_DATA_PACK_ZONE";
    private const string ALL_DATA_SPIN_WHEEL = "ALL_DATA_SPIN_WHEEL";
    private const string ALL_DATA_QUANTITY_CRITTER_CATCHED = "ALL_DATA_QUANTITY_CATCHED";
    private const string ALL_DATA_UNLOCK_FETURES = "ALL_DATA_UNLOCK_FETURES";
    private const string ALL_DATA_LANGUAGE = "ALL_DATA_LANGUAGE";
    private const string ALL_DATA_MOVE_DOOR = "ALL_DATA_MOVE_DOOR";
    private const string ALL_DATA_NO_ADS = "ALL_DATA_NO_ADS";

    private static DataInfoPlayer dataInfoPlayer;
    private static DataAllid dataAllid;
    private static DataAllidMerge dataAllidMerge;
    private static DataCoin dataCoin;
    private static DataPoint dataPoint;
    private static DataCritters dataCritters;
    private static DataLevel dataLevel;
    private static DataSound dataSound;
    private static DataTutorial dataTutorial;
    private static DataBuyChest dataBuyChest;
    private static DataGem dataGem;
    private static DataRate dataRate;
    private static DataSpinWheel dataSpinWheel;
    private static DataPackShop dataPackShop;
    private static DataPackLogin dataPackLogin;
    private static DataPackLevel dataPackZone;
    private static DataPickeWheel dataPickeWheel;
    private static DataQuantitCritterCatched dataCatched;
    private static DataUnlockFeture dataUnlockFeture;
    private static DataLanguage dataLanguage;
    private static DataMoveDoorOfLevel dataMoveDoorOfLevel;
    private static UnLockNoAds dataUnLockNoAds;

    static DataPlayer()
    {
        dataInfoPlayer = JsonConvert.DeserializeObject<DataInfoPlayer>(PlayerPrefs.GetString(ALL_DATA));
        if (dataInfoPlayer == null)
        {
            dataInfoPlayer = new DataInfoPlayer();
            /*dataInfoPlayer.Add(ECharacterType.Usaseal);
            dataInfoPlayer.Add(ECharacterType.Sucker);*//*
            dataInfoPlayer.Add(ECharacterType.Phenux);*/
        }
        dataAllid = JsonConvert.DeserializeObject<DataAllid>(PlayerPrefs.GetString(ALL_DATA_ALLID));

        if (dataAllid == null)
        {
            dataAllid = new DataAllid(4, false);
        }
        dataAllidMerge = JsonConvert.DeserializeObject<DataAllidMerge>(PlayerPrefs.GetString(ALL_DATA_ALLID_MERGE));
        if (dataAllidMerge == null)
        {
            dataAllidMerge = new DataAllidMerge(2);
        }
        dataCoin = JsonConvert.DeserializeObject<DataCoin>(PlayerPrefs.GetString(ALL_DATA_COIN));
        if (dataCoin == null)
        {
            dataCoin = new DataCoin(200);
        }
        dataGem = JsonConvert.DeserializeObject<DataGem>(PlayerPrefs.GetString(ALL_DATA_GEM));
        if (dataGem == null)
        {
            dataGem = new DataGem();
            dataGem.Gem = 0;
        }
        dataPoint = JsonConvert.DeserializeObject<DataPoint>(PlayerPrefs.GetString(ALL_DATA_POINT));
        if (dataPoint == null)
        {
            dataPoint = new DataPoint();
            dataPoint.StartPoint = new Vector3(16.6f, -42.7f, 0);
        }
        dataCritters = JsonConvert.DeserializeObject<DataCritters>(PlayerPrefs.GetString(ALL_DATA_CRITTERS));
        if (dataCritters == null)
        {
            dataCritters = new DataCritters();

            /*dataCritters.AddListType(ECharacterType.Usaseal);
            dataCritters.AddListType(ECharacterType.Sucker);*/

            dataCritters.AddListType(ECharacterType.Mishmash);
            dataCritters.AddListType(ECharacterType.Tuber);
        }
        dataLevel = JsonConvert.DeserializeObject<DataLevel>(PlayerPrefs.GetString(ALL_DATA_LEVEL));
        if (dataLevel == null)
        {
            dataLevel = new DataLevel();
            dataLevel.AddListLevel(0);
            dataLevel.CurLevel = 0;
            dataLevel.isFirstLevelUp = false;
        }
        dataSound = JsonConvert.DeserializeObject<DataSound>(PlayerPrefs.GetString(ALL_DATA_SOUND));
        if (dataSound == null)
        {
            dataSound = new DataSound();
            dataSound.value = 100;
        }
        dataTutorial = JsonConvert.DeserializeObject<DataTutorial>(PlayerPrefs.GetString(ALL_DATA_TUTORIAL, string.Empty));

        if (dataTutorial == null)
        {
            dataTutorial = new DataTutorial();
            dataTutorial.IsCheckDoneTutorial = false;
            dataTutorial.IsCheckAddToSlotInUITeam = false;
            dataTutorial.IsCheckAddToSlotInUITeam2 = false;
            dataTutorial.IsSlideToMove = false;
            dataTutorial.IsTapToBackUITeam = false;
            dataTutorial.IsTriggerEnemy = false;
            dataTutorial.IsTapToMove = false;

            dataTutorial.IsCheckTapToAllid = false;
            dataTutorial.IsCheckWin = false;
            dataTutorial.IsChecktapButtonCatchFree = false;
            dataTutorial.isCheckTapButtonSkip = false;
        }
        dataBuyChest = JsonConvert.DeserializeObject<DataBuyChest>(PlayerPrefs.GetString(ALL_DATA_BUY_CHEST));
        {
            if (dataBuyChest == null)
            {
                dataBuyChest = new DataBuyChest();
                dataBuyChest.timeOutAdsChestNormal = new DateTime();

                dataBuyChest.timeOutAdsChestEpic = DateTime.Now.AddDays(3);

                dataBuyChest.timOutAdsChestLegend = DateTime.Now.AddDays(7);

                dataBuyChest.NextTime = 0;
                dataBuyChest.CountAdsNormal = 0;
                dataBuyChest.CountAdsEpic = 0;
                dataBuyChest.CountAdsLegend = 0;
                dataBuyChest.isCheckNewUserWithChestEpic = false;
                dataBuyChest.isCheckNewUserWithChestLegend = false;
            }
        }
        dataRate = JsonConvert.DeserializeObject<DataRate>(PlayerPrefs.GetString(ALL_DATA_RATED));
        {
            if (dataRate == null)
            {
                dataRate = new DataRate();
                dataRate.isRated = false;

            }
        }
        dataPackShop = JsonConvert.DeserializeObject<DataPackShop>(PlayerPrefs.GetString(ALL_DATA_PACK_SHOP));
        {
            if (dataPackShop == null)
            {
                dataPackShop = new DataPackShop();
                dataPackShop.quantityChestNormal = 0;
                dataPackShop.quantityChestEpic = 0;
                dataPackShop.quantityChestLegend = 0;
                dataPackShop.L_ID = new List<int>();
            }
        }
        dataPackLogin = JsonConvert.DeserializeObject<DataPackLogin>(PlayerPrefs.GetString(ALL_DATA_PACK_LOGIN));
        {
            if (dataPackLogin == null)
            {
                dataPackLogin = new DataPackLogin();
                dataPackLogin.CountDay = -1;
                dataPackLogin.dateTime = new DateTime();
                dataPackLogin.UnlockGemWithDolar = false;
                dataPackLogin.L_IDPurchasebuttonDoneGemFree = new List<int>(30);
                dataPackLogin.L_IDPurchasebuttonDoneGemNoFree = new List<int>(30);
            }
        }
        dataPackZone = JsonConvert.DeserializeObject<DataPackLevel>(PlayerPrefs.GetString(ALL_DATA_PACK_ZONE));
        {
            if (dataPackZone == null)
            {
                dataPackZone = new DataPackLevel();
                dataPackZone.UnLockRewardsWithDolar = false;
                dataPackZone.CurMap = 0;

                dataPackZone.L_IDPurchasebuttonDoneGemFree = new List<int>(30);
                dataPackZone.L_IDPurchasebuttonDoneGemNoFree1 = new List<int>(30);
                dataPackZone.L_IDPurchasebuttonDoneGemNoFree2 = new List<int>(30);
            }
        }
        dataPickeWheel = JsonConvert.DeserializeObject<DataPickeWheel>(PlayerPrefs.GetString(ALL_DATA_SPIN_WHEEL));
        {
            if (dataPickeWheel == null)
            {
                dataPickeWheel = new DataPickeWheel();
                dataPickeWheel.timeFree = new DateTime();
                dataPickeWheel.timeCoolNextDay = new DateTime();
                dataPickeWheel.timeAds = new DateTime();
                dataPickeWheel.CountFree = 0;
            }
        }
        dataCatched = JsonConvert.DeserializeObject<DataQuantitCritterCatched>(PlayerPrefs.GetString(ALL_DATA_QUANTITY_CRITTER_CATCHED));
        {
            if (dataCatched == null)
            {
                dataCatched = new DataQuantitCritterCatched();
                dataCatched.QuantityMap1 = 0;
                dataCatched.QuantityMap2 = 0;
                dataCatched.QuantityMap3 = 0;
                dataCatched.QuantityMap4 = 0;
                dataCatched.QuantityMap5 = 0;
                dataCatched.QuantityMap6 = 0;
                dataCatched.QuantityMap7 = 0;
                dataCatched.QuantityMap8 = 0;
                dataCatched.QuantityMap9 = 0;
                dataCatched.QuantityMap10 = 0;
            }
        }
        dataUnlockFeture = JsonConvert.DeserializeObject<DataUnlockFeture>(PlayerPrefs.GetString(ALL_DATA_UNLOCK_FETURES));
        {
            if (dataUnlockFeture == null)
            {
                dataUnlockFeture = new DataUnlockFeture();
                dataUnlockFeture.isUnlockLogin = false;
                dataUnlockFeture.isUnlockNoAds = false;
                dataUnlockFeture.isUnlockPickerWheel = false;
                dataUnlockFeture.isUnlockUIMonster = false;
                dataUnlockFeture.isUnlockZone = false;
                dataUnlockFeture.isOutPopUpRate = false;
                dataUnlockFeture.isOutPopUpRate = false;

                dataUnlockFeture.CountEnemyCatched = 0;
                dataUnlockFeture.CountBossCatched = 0;
            }
        }
        dataLanguage = JsonConvert.DeserializeObject<DataLanguage>(PlayerPrefs.GetString(ALL_DATA_LANGUAGE));
        {
            if (dataLanguage == null)
            {
                dataLanguage = new DataLanguage();
                dataLanguage.CurrentLanguage = "en";
            }
        }
        dataMoveDoorOfLevel = JsonConvert.DeserializeObject<DataMoveDoorOfLevel>(PlayerPrefs.GetString(ALL_DATA_MOVE_DOOR));
        {
            if (dataMoveDoorOfLevel == null)
            {
                dataMoveDoorOfLevel = new DataMoveDoorOfLevel();
                dataMoveDoorOfLevel.IsDoneMoveMap1 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap2 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap3 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap4 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap5 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap6 = false;
                dataMoveDoorOfLevel.IsDoneMoveMap7 = false;
            }
        }
        dataUnLockNoAds = JsonConvert.DeserializeObject<UnLockNoAds>(PlayerPrefs.GetString(ALL_DATA_NO_ADS));
        {
            if(dataUnLockNoAds == null)
            {
                dataUnLockNoAds = new UnLockNoAds();
                dataUnLockNoAds.isUnlockDone = false;
            }
        }
        SaveData();
        SaveDataAllied();
        SaveDataAlliedMerge();
        SaveDataCoin();
        SaveDataGem();
        SaveDataPoint();
        SaveDataCritters();
        SaveDataLevel();
        SaveDataTutorial();
        SaveDataRated();
        SaveDataBuyChest();
        SaveDataPackShop();
        SaveDataPackLogin();
        SaveDataPackZone();
        SaveDataPickerWheel();
        SaveDataCatched();
        SaveDataUnLockFeture();
        SaveDataLanguage();
        SaveDataMoveDoor();
        SaveDataNoAds();
    }
    public static void SetNoAds(bool value)
    {
        dataUnLockNoAds.isUnlockDone = value;
        SaveDataNoAds();
    }
    public static bool GetNoAds()
    {
        return dataUnLockNoAds.isUnlockDone;
    }
    public static void SetBossCatched()
    {
        if (BagManager.Instance.m_RuleController.CurLevel == 1)
        {
            dataUnlockFeture.CountBossCatched++;
            SaveDataUnLockFeture();
        }
    }
    public static int GetBossCatched()
    {
        return dataUnlockFeture.CountBossCatched;
    }
    public static void SetEnemyCatched()
    {
        if (BagManager.Instance.m_RuleController.CurLevel >= 1)
        {
            dataUnlockFeture.CountEnemyCatched++;
            SaveDataUnLockFeture();
        }
    }
    public static int GetEnemyCatched()
    {
        return dataUnlockFeture.CountEnemyCatched;
    }
    public static bool GetOutpopUPRate()
    {
        return dataUnlockFeture.isOutPopUpRate;
    }
    public static void SetOutPopUpRate(bool value)
    {
        dataUnlockFeture.isOutPopUpRate = value;
        SaveDataUnLockFeture();
    }
    public static void SetOffUPRate(bool value)
    {
        dataUnlockFeture.isOffPopUpRate = value;
        SaveDataUnLockFeture();
    }
    public static bool GetOffPopUpRate()
    {
        return dataUnlockFeture.isOffPopUpRate;
    }
    public static void SetUnLockMonster(bool value)
    {
        dataUnlockFeture.isUnlockUIMonster = value;
        SaveDataUnLockFeture();
    }
    public static bool GetUnlockNonster()
    {
        return dataUnlockFeture.isUnlockUIMonster;
    }
    public static void SetUnLockNoAds(bool value)
    {
        dataUnlockFeture.isUnlockNoAds = value;
        SaveDataUnLockFeture();
    }
    public static bool GetUnLockNoAds()
    {
        return dataUnlockFeture.isUnlockNoAds;
    }
    public static void SetUnLockLogin(bool value)
    {
        dataUnlockFeture.isUnlockLogin = value;
        SaveDataUnLockFeture();
    }
    public static bool GetUnLockLogin()
    {
        return dataUnlockFeture.isUnlockLogin;
    }
    public static void SetUnLockZone(bool value)
    {
        dataUnlockFeture.isUnlockZone = value;
        SaveDataUnLockFeture();
    }
    public static bool GetUnLockZone()
    {
        return dataUnlockFeture.isUnlockZone;
    }
    public static void SetUnLockPickerWheel(bool value)
    {
        dataUnlockFeture.isUnlockPickerWheel = value;
        SaveDataUnLockFeture();
    }
    public static bool GetUnLockPikcerWheel()
    {
        return dataUnlockFeture.isUnlockPickerWheel;
    }
    public static void ResetDataWithTurotial()
    {
        dataInfoPlayer = new DataInfoPlayer();

        /*dataInfoPlayer.Add(ECharacterType.Usaseal);
        dataInfoPlayer.Add(ECharacterType.Sucker);*/
        dataInfoPlayer.Add(ECharacterType.Tuber);
        dataInfoPlayer.Add(ECharacterType.Mishmash);

        dataAllid = new DataAllid(4, false);

        dataAllidMerge = new DataAllidMerge(2);

        dataCoin = new DataCoin(200);

        dataPoint = new DataPoint();
        dataPoint.StartPoint = new Vector3(16.6f, -42.7f, 0);

        dataPoint = new DataPoint();
        dataPoint.StartPoint = new Vector3(16.6f, -42.7f, 0);

        dataCritters = new DataCritters();

        /*dataCritters.AddListType(ECharacterType.Usaseal);
        dataCritters.AddListType(ECharacterType.Sucker);*/

        dataCritters.AddListType(ECharacterType.Tuber);
        dataCritters.AddListType(ECharacterType.Mishmash);

        dataLevel = new DataLevel();
        dataLevel.AddListLevel(0);
        dataLevel.CurLevel = 0;
        dataLevel.isFirstLevelUp = false;

        dataSound = new DataSound();
        dataSound.value = 100;

        dataTutorial = new DataTutorial();
        dataTutorial.IsCheckDoneTutorial = false;
        dataTutorial.IsCheckAddToSlotInUITeam = false;
        dataTutorial.IsCheckAddToSlotInUITeam2 = false;
        dataTutorial.IsSlideToMove = false;
        dataTutorial.IsTapToBackUITeam = false;
        dataTutorial.IsTriggerEnemy = false;
        dataTutorial.IsTapToMove = false;

        dataTutorial.IsCheckTapToAllid = false;
        dataTutorial.IsCheckWin = false;
        dataTutorial.IsChecktapButtonCatchFree = false;
        dataTutorial.isCheckTapButtonSkip = false;

        dataBuyChest = new DataBuyChest();
        dataBuyChest.timeOutAdsChestNormal = new DateTime();
        dataBuyChest.timeOutAdsChestEpic = new DateTime();

        dataBuyChest.NextTime = 0;
        dataBuyChest.CountAdsNormal = 0;
        dataBuyChest.CountAdsEpic = 0;
    }



    private static void SaveData()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
        PlayerPrefs.SetString(ALL_DATA, json);
        PlayerPrefs.Save();

    }

    private static void SaveDataAllied()
    {
        string json = JsonConvert.SerializeObject(dataAllid);
        PlayerPrefs.SetString(ALL_DATA_ALLID, json);
        PlayerPrefs.Save();

    }
    private static void SaveDataAlliedMerge()
    {
        string json = JsonConvert.SerializeObject(dataAllidMerge);
        PlayerPrefs.SetString(ALL_DATA_ALLID_MERGE, json);
        PlayerPrefs.Save();

    }

    private static void SaveDataCoin()
    {
        string json = JsonConvert.SerializeObject(dataCoin);
        PlayerPrefs.SetString(ALL_DATA_COIN, json);
        PlayerPrefs.Save();

    }
    private static void SaveDataGem()
    {
        string json = JsonConvert.SerializeObject(dataGem);
        PlayerPrefs.SetString(ALL_DATA_GEM, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataPoint()
    {
        string json = JsonConvert.SerializeObject(dataPoint, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
        PlayerPrefs.SetString(ALL_DATA_POINT, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataCritters()
    {
        string json = JsonConvert.SerializeObject(dataCritters);
        PlayerPrefs.SetString(ALL_DATA_CRITTERS, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataLevel()
    {
        string json = JsonConvert.SerializeObject(dataLevel);
        PlayerPrefs.SetString(ALL_DATA_LEVEL, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataVolume()
    {
        string json = JsonConvert.SerializeObject(dataSound);
        PlayerPrefs.SetString(ALL_DATA_SOUND, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataTutorial()
    {
        string json = JsonConvert.SerializeObject(dataTutorial);
        PlayerPrefs.SetString(ALL_DATA_TUTORIAL, json);
        PlayerPrefs.Save();
    }
    public static void SaveDataBuyChest()
    {
        string json = JsonConvert.SerializeObject(dataBuyChest);
        PlayerPrefs.SetString(ALL_DATA_BUY_CHEST, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataRated()
    {
        string json = JsonConvert.SerializeObject(dataRate);
        PlayerPrefs.SetString(ALL_DATA_RATED, json);
        PlayerPrefs.Save();

    }

    public static void SaveDataPackShop()
    {
        string json = JsonConvert.SerializeObject(dataPackShop);
        PlayerPrefs.SetString(ALL_DATA_PACK_SHOP, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataPackLogin()
    {
        string json = JsonConvert.SerializeObject(dataPackLogin);
        PlayerPrefs.SetString(ALL_DATA_PACK_LOGIN, json);
        PlayerPrefs.Save();

    }

    public static void SaveDataPackZone()
    {
        string json = JsonConvert.SerializeObject(dataPackZone);
        PlayerPrefs.SetString(ALL_DATA_PACK_ZONE, json);
        PlayerPrefs.Save();

    }

    public static void SaveDataPickerWheel()
    {
        string json = JsonConvert.SerializeObject(dataPickeWheel);
        PlayerPrefs.SetString(ALL_DATA_SPIN_WHEEL, json);
        PlayerPrefs.Save();

    }

    public static void SaveDataCatched()
    {
        string json = JsonConvert.SerializeObject(dataCatched);
        PlayerPrefs.SetString(ALL_DATA_QUANTITY_CRITTER_CATCHED, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataUnLockFeture()
    {
        string json = JsonConvert.SerializeObject(dataUnlockFeture);
        PlayerPrefs.SetString(ALL_DATA_UNLOCK_FETURES, json);
        PlayerPrefs.Save();

    }
    public static void SaveDataLanguage()
    {
        string json = JsonConvert.SerializeObject(dataLanguage);
        PlayerPrefs.SetString(ALL_DATA_LANGUAGE, json);
        PlayerPrefs.Save();
    }
    public static void SaveDataMoveDoor()
    {
        string json = JsonConvert.SerializeObject(dataMoveDoorOfLevel);
        PlayerPrefs.SetString(ALL_DATA_MOVE_DOOR, json);
        PlayerPrefs.Save();
    }
    public static void SaveDataNoAds()
    {
        string json = JsonConvert.SerializeObject(dataUnLockNoAds);
        PlayerPrefs.SetString(ALL_DATA_NO_ADS, json);
        PlayerPrefs.Save();
    }
    public static void Log()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
    }
    public static void SetRate(bool value)
    {
        dataRate.isRated = value;
        SaveDataRated();
    }
    public static bool GetRate()
    {
        return dataRate.isRated;
    }
    public static void Add(ECharacterType Key)
    {
        dataInfoPlayer.Add(Key);
        SaveData();
    }
    public static void SetIsNewUser(bool _value)
    {
        dataInfoPlayer.isCheckNewUser = _value;
        SaveData();
    }
    public static bool GetIsNewUser()
    {
        return dataInfoPlayer.isCheckNewUser;
    }
    public static void Add(ElementData elementData)
    {
        dataInfoPlayer.Add(elementData);
        SaveData();
    }
    public static void Remove(ECharacterType Key, int ID)
    {
        dataInfoPlayer.Remove(Key, ID);
        SaveData();
    }
    public static bool Checkey(ECharacterType Key)
    {
        return dataInfoPlayer.CheckKey(Key);
    }
    public static Dictionary<ECharacterType, List<ElementData>> GetDictionary()
    {
        return dataInfoPlayer.keyValuePairs;
    }

    public static void SetIsActiveSlot4(bool _value)
    {
        dataAllid.SetSlot4(_value);
        SaveDataAllied();
    }
    public static bool GetIsActiveSLot4()
    {
        return dataAllid.GetSlot4();
    }

    public static List<ElementData> GetListAllid()
    {
        return dataAllid.keyValueAllid;
    }
    public static List<ElementData> GetListAllidMerge()
    {
        return dataAllidMerge.keyValueAllid;
    }
    public static void AddAlliedIteam(ElementData elementData)
    {
        dataAllid.AddAllidItem(elementData);
        SaveDataAllied();
    }
    public static void RemoveItem(ElementData elementData)
    {
        dataAllid.RemoveItem(elementData);
        SaveDataAllied();
    }
    public static void AddAlliedIteamMerge(ElementData elementData)
    {
        dataAllidMerge.AddAllidItemMerge(elementData);
        SaveDataAlliedMerge();
    }
    public static void RemoveItemMerge(ElementData elementData)
    {
        dataAllidMerge.RemoveItemMerge(elementData);
        SaveDataAlliedMerge();
    }
    public static void ResetItemMerge()
    {
        dataAllidMerge.ResetItemMerge();
        SaveDataAlliedMerge();
    }
    public static void SetHP(int HP, ElementData elementData)
    {
        dataAllid.SetHP(HP, elementData);
        SaveDataAllied();
    }
    public static void SetHP(ECharacterType key, int ID, int HP)
    {
        dataInfoPlayer.SetHP(key, ID, HP);
        SaveData();
    }
    public static int GetCoin()
    {
        return dataCoin.GetCoin();
    }
    public static void SetCoin(int Coin)
    {
        dataCoin.SetCoin(Coin);
        SaveDataCoin();
    }
    public static void SetGem(int value)
    {
        dataGem.Gem = value;
        SaveDataGem();
    }
    public static int GetGem()
    {
        return dataGem.Gem;
    }
    public static void AddCritter(ECharacterType type)
    {
        dataCritters.AddListType(type);
        SaveDataCritters();
    }
    public static List<ECharacterType> GetListCritters()
    {
        return dataCritters.GetListCritters();
    }
    public static List<int> GetListLevel()
    {
        return dataLevel.GetListCurrentLevel();
    }
    public static void AddListLevel(int value)
    {
        dataLevel.AddListLevel(value);
        SaveDataLevel();
    }
    public static void SetIsFistLevelUp()
    {

    }

    public static void SetCurLevel(int value)
    {
        dataLevel.SetCurLevel(value);
        SaveDataLevel();
    }
    public static int getCurLevel()
    {
        return dataLevel.GetCurLevel();
    }
    public static void SetPlayerPosWhenExit(Vector3 Pos)
    {
        dataPoint.SetPlayerPosWhenExit(Pos);
        SaveDataPoint();
    }
    public static Vector3 GetPlayerPosWhenExit()
    {
        return dataPoint.GetPlayerPosWhenExit();
    }
    public static void AddCheckPointPosToList(Transform Pos)
    {
        dataPoint.AddCheckPointPosToList(Pos);
        SaveDataPoint();
    }
    public static List<Vector3> GetListCheckPointPos()
    {
        return dataPoint.GetListCheckPointPos();
    }
    public static Vector3 GetStartPosNewUser()
    {
        return dataPoint.StartPoint;
    }
    public static void SetValueVolume(int value)
    {
        dataSound.SetValueVolume(value);
    }
    public static int GetValueVolume()
    {
        return dataSound.GetValueVolume();
    }
    public static bool GetIsCheckDoneTutorial()
    {
        return dataTutorial.IsCheckDoneTutorial;
    }

    public static bool GetIsCheckAddToSlot()
    {
        return dataTutorial.IsCheckAddToSlotInUITeam;
    }
    public static bool GetIsTapToBackUITeam()
    {
        return dataTutorial.IsTapToBackUITeam;
    }
    public static bool GetIsTapToMove()
    {
        return dataTutorial.IsTapToMove;
    }
    public static bool GetIsSlideToMove()
    {
        return dataTutorial.IsSlideToMove;
    }
    public static bool GetIsTriggerEnemy()
    {
        return dataTutorial.IsTriggerEnemy;
    }
    public static bool GetIsCheckTapToAdd()
    {
        return dataTutorial.GetIsCheckTapAddSlot();
    }
    public static bool GetIsCheckTapToAdd2UITeam()
    {
        return dataTutorial.IsCheckAddToSlotInUITeam2;
    }

    public static void SetIsCheckDoneTutorial(bool value)
    {
        dataTutorial.SetIsCheckDoneTutorial(value);
        SaveDataTutorial();

    }
    public static void SetIsCheckAddToSlot(bool value)
    {
        dataTutorial.SetIsCheckAddToSlot(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckAddToSlot2(bool value)
    {
        dataTutorial.IsCheckAddToSlotInUITeam2 = value;
        SaveDataTutorial();
    }
    public static void SetIsTapToBackUITeam(bool value)
    {
        dataTutorial.SetIsTapToBackUITeam(value);
        SaveDataTutorial();
    }
    public static void SetIsTapToMove(bool value)
    {
        dataTutorial.SetIsTapToMove(value);
        SaveDataTutorial();
    }
    public static void SetIsSlideToMove(bool value)
    {
        dataTutorial.SetIsSlideToMove(value);
        SaveDataTutorial();
    }
    public static void SetIsTriggerEnemy(bool value)
    {
        dataTutorial.SetIsTriggerEnemy(value);
        SaveDataTutorial();
    }
    public static void SetIsTapToAddSlot(bool value)
    {
        dataTutorial.SetIsCheckTapAddSlot(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckTapToAllid(bool value)
    {
        dataTutorial.SetIsCheckTapToAllid(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckWin(bool value)
    {
        dataTutorial.SetIsCheckWin(value);
        SaveDataTutorial();
    }
    public static void SetIsChecktapButtonCatchFree(bool value)
    {
        dataTutorial.SetIsChecktapButtonCatchFree(value);
        SaveDataTutorial();
    }
    public static void SetisCheckTapButtonSkip(bool value)
    {
        dataTutorial.SetisCheckTapButtonSkip(value);
        SaveDataTutorial();
    }

    public static void SetIsTapGotIt(bool value)
    {
        dataTutorial.SetIsCheckTapGotIt(value);
        SaveDataTutorial();
    }
    public static void SetIsTapButtonClaim(bool value)
    {
        dataTutorial.SetIsCheckTapButtonClaim(value);
        SaveDataTutorial();
    }
    public static void ReloadDataTutorial()
    {
        dataTutorial = null;
        dataAllid = null;
        dataCritters = null;
        dataInfoPlayer = null;
        dataPoint = null;
        dataCoin = new DataCoin(200);
        dataLevel = new DataLevel();
        dataLevel.SetCurLevel(0);
    }

    public static bool GetIsTapButtonClaim()
    {
        return dataTutorial.GetIsCheckTapButtonClaim();
    }
    public static bool GetIsTapGotIt()
    {
        return dataTutorial.GetIsCheckTapGotIt();
    }
    public static bool getIsCheckTapToAllid()
    {
        return dataTutorial.getIsCheckTapToAllid();
    }
    public static bool getIsCheckWin()
    {
        return dataTutorial.getIsCheckWin();
    }
    public static bool GetIsChecktapButtonCatchFree()
    {
        return dataTutorial.GetIsChecktapButtonCatchFree();
    }
    public static bool GetIsCheckTapButtonSkip()
    {
        return dataTutorial.GetIsCheckTapButtonSkip();
    }

    public static bool GetIsCheckTapBag()
    {
        return dataTutorial.GetIsCheckTapBag();
    }
    public static bool GetisCheckTapMergeBtn()
    {
        return dataTutorial.GetisCheckTapMergeBtn();
    }
    public static bool getisCheckTapSLotMerge()
    {
        return dataTutorial.getisCheckTapSLotMerge();
    }
    public static bool GetIsCheckSkipPopUpMerge()
    {
        return dataTutorial.GetIsCheckSkipPopUpMerge();
    }
    public static bool GetIsCheckOutUiMerge()
    {
        return dataTutorial.GetIsCheckOutUiMerge();
    }

    public static void SetIsCheckTapBag(bool Value)
    {
        dataTutorial.SetIsCheckTapBag(Value);
        SaveDataTutorial();
    }
    public static void SetIsCheckTapMergeBtn(bool Value)
    {
        dataTutorial.SetIsCheckTapMergeBtn(Value);
        SaveDataTutorial();
    }
    public static void SetIsCheckTapSlotMerge(bool value)
    {
        dataTutorial.SetIsCheckTapSlotMerge(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckTapButtonMerge(bool value)
    {
        dataTutorial.SetIsCheckTapButtonMerge(value);
        SaveDataTutorial();
    }
    public static void SetIsChecktapSKipPopUpMerge(bool value)
    {
        dataTutorial.SetIsChecktapSKipPopUpMerge(value);
        SaveDataTutorial();
    }
    public static void SetisCheckOutUIMerge(bool value)
    {
        dataTutorial.SetisCheckOutUIMerge(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckButtonOnMerge(bool value)
    {
        dataTutorial.SetIsCheckButtonOnMerge(value);
        SaveDataTutorial();
    }

    public static void SetIsCheckTapButtonteam(bool value)
    {
        dataTutorial.SetIsCheckTapButtonTeam(value);
        SaveDataTutorial();
    }
    public static bool GetIsCheckTapButtonTeam()
    {
        return dataTutorial.GetIsCheckTapButtonteam();
    }
    public static bool GetIsCheckButtonOnMerge()
    {
        return dataTutorial.GetIsCheckButtonOnMerge();
    }
    public static bool GetIsHealHP()
    {
        return dataTutorial.GetIsHealHp();
    }
    public static void SetIsHealHP(bool value)
    {
        dataTutorial.SetIsTapToHealHp(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckTapCloseBag(bool Value)
    {
        dataTutorial.SetIsCheckTapCloseBag(Value);
        SaveDataTutorial();
    }
    public static bool GetIsCheckTapCloseBag()
    {
        return dataTutorial.GetIsCheckTapCloseBag();
    }
    public static bool GetIsCheckActivetext()
    {
        return dataTutorial.GetIsCheckActivetext();
    }
    public static void SetIsCheckActivetext(bool value)
    {
        dataTutorial.SetIsCheckActivetext(value);
        SaveDataTutorial();
    }
    public static void SetIsCheckSelectMap1(bool value)
    {
        dataTutorial.IsCheckSelectMap1 = value;
        SaveDataTutorial();
    }
    public static bool GetIsCheckSelectMap1()
    {
        return dataTutorial.GetIsCheckSelectMap1();
    }
    public static void ClearAlldata()
    {
        dataInfoPlayer = new DataInfoPlayer();
        dataInfoPlayer.ClearAllData();
        SaveData();
    }
    public static void ClearDataAllid()
    {
        dataAllid = new DataAllid(4, false);
        dataAllid.Clear();
        SaveDataAllied();
    }
    public static void SetTimeOutChestNormal(DateTime value)
    {
        dataBuyChest.timeOutAdsChestNormal = value;
        SaveDataBuyChest();
    }
    public static DateTime GetTimeOutChestNormal()
    {
        return dataBuyChest.timeOutAdsChestNormal;
    }
    public static void SetTimeNextDayChestNormal(DateTime value)
    {
        dataBuyChest.TimeNextDay = value;
        SaveDataBuyChest();
    }
    public static DateTime GetTimeNextDayChestNormal()
    {
        return dataBuyChest.TimeNextDay;
    }
    public static void SetTimeOutChestEpic(DateTime value)
    {
        dataBuyChest.timeOutAdsChestEpic = value;
        SaveDataBuyChest();
    }
    public static DateTime GetTimeOutChestEpic()
    {
        return dataBuyChest.timeOutAdsChestEpic;
    }
    public static void SetTimeOutChestLegend(DateTime value)
    {
        dataBuyChest.timOutAdsChestLegend = value;
        SaveDataBuyChest();
    }
    public static DateTime GetTimeOutChestLegend()
    {
        return dataBuyChest.timOutAdsChestLegend;
    }

    public static void SetEndTime(long value)
    {
        dataBuyChest.NextTime = value;
        SaveDataBuyChest();
    }
    public static long GetEndTime()
    {
        return dataBuyChest.NextTime;
    }
    public static void ReLoadDataCritter()
    {
        dataCritters = new DataCritters();
    }
    public static void SetCountAdsNormal(int value)
    {
        dataBuyChest.CountAdsNormal = value;
        SaveDataBuyChest();
    }
    public static int GetCountAdsNormal()
    {
        return dataBuyChest.CountAdsNormal;
    }

    public static void SetCountAdsEpic(int value)
    {
        dataBuyChest.CountAdsEpic = value;
        SaveDataBuyChest();
    }
    public static int GetCountAdsEpic()
    {
        return dataBuyChest.CountAdsEpic;
    }
    public static void SetCountAdsLegend(int value)
    {
        dataBuyChest.CountAdsLegend = value;
        SaveDataBuyChest();
    }
    public static void SetIsCheckNewUerChestEpic(bool value)
    {
        dataBuyChest.isCheckNewUserWithChestEpic = value;
        SaveDataBuyChest();
    }
    public static bool GetIsNewDataBuyChestEpic()
    {
        return dataBuyChest.isCheckNewUserWithChestEpic;
    }
    public static void SetIsCheckNewUerChestLegend(bool value)
    {
        dataBuyChest.isCheckNewUserWithChestLegend = value;
        SaveDataBuyChest();
    }
    public static bool GetIsNewDataBuyChestLegend()
    {
        return dataBuyChest.isCheckNewUserWithChestLegend;
    }
    public static int GetCountAdsLegend()
    {
        return dataBuyChest.CountAdsEpic;
    }

    public static DateTime GetTimeSpin()
    {
        return dataSpinWheel.time;
    }


    public static int GetCountSpin()
    {
        return dataPickeWheel.CountFree;
    }
    public static void SetQuantityChestNormalPack(int value)
    {
        dataPackShop.quantityChestNormal = value;
        SaveDataPackShop();
    }
    public static int GetQuantityChestNormalPack()
    {
        return dataPackShop.quantityChestNormal;
    }
    public static void SetQuantityChestEpicPack(int value)
    {
        dataPackShop.quantityChestEpic = value;
        SaveDataPackShop();
    }
    public static int GetQuantityChestEpicPack()
    {
        return dataPackShop.quantityChestEpic;
    }
    public static void SetQuantityChestLegendPack(int value)
    {
        dataPackShop.quantityChestLegend = value;
        SaveDataPackShop();
    }
    public static int GetQuantityChestLegendPack()
    {
        return dataPackShop.quantityChestLegend;
    }
    public static void addlistIDPack(int value)
    {
        dataPackShop.L_ID.Add(value);
        SaveDataPackShop();
    }
    public static List<int> ListPack()
    {
        return dataPackShop.L_ID;
    }
    public static void SetTimePackLogin(DateTime time)
    {
        dataPackLogin.dateTime = time;
        SaveDataPackLogin();
    }
    public static DateTime GetTimePackLogin()
    {
        return dataPackLogin.dateTime;
    }
    public static void SetCountDayPackLogin(int count)
    {
        dataPackLogin.CountDay = count;
        SaveDataPackLogin();
    }
    public static int GetCountDayPackLogin()
    {
        return dataPackLogin.CountDay;
    }
    public static void SetUnLockGemPackLogin(bool value)
    {
        dataPackLogin.UnlockGemWithDolar = value;
        SaveDataPackLogin();
    }
    public static bool GetUnLockGemPackLogin()
    {
        return dataPackLogin.UnlockGemWithDolar;
    }
    public static void AddListDoneGemFree(int ID)
    {
        dataPackLogin.L_IDPurchasebuttonDoneGemFree.Add(ID);
        SaveDataPackLogin();
    }
    public static List<int> GetListDoneGemFree()
    {
        return dataPackLogin.L_IDPurchasebuttonDoneGemFree;
    }
    public static void AddListDoneGemNoFree(int ID)
    {
        dataPackLogin.L_IDPurchasebuttonDoneGemNoFree.Add(ID);
        SaveDataPackLogin();
    }
    public static List<int> GetListDoneGemNoFree()
    {
        return dataPackLogin.L_IDPurchasebuttonDoneGemNoFree;
    }
    public static bool GetUnLockRewardNoFreePackZone()
    {
        return dataPackZone.UnLockRewardsWithDolar;
    }
    public static void SetUnLockRewardsFreePackZone(bool value)
    {
        dataPackZone.UnLockRewardsWithDolar = value;
        SaveDataPackZone();
    }
    public static void AddListDoneGemfreePackZone(int ID)
    {
        dataPackZone.L_IDPurchasebuttonDoneGemFree.Add(ID);
        SaveDataPackZone();
    }
    public static List<int> GetListDoneGemFreePackZone()
    {
        return dataPackZone.L_IDPurchasebuttonDoneGemFree;
    }
    public static void AddListDoneGemNofree1PackZone(int ID)
    {
        dataPackZone.L_IDPurchasebuttonDoneGemNoFree1.Add(ID);
        SaveDataPackZone();
    }
    public static List<int> GetListDoneGemNoFree1PackZone()
    {
        return dataPackZone.L_IDPurchasebuttonDoneGemNoFree1;
    }
    public static void AddListDoneGemNofree2PackZone(int ID)
    {
        dataPackZone.L_IDPurchasebuttonDoneGemNoFree2.Add(ID);
        SaveDataPackZone();
    }
    public static List<int> GetListDoneGemNoFree2PackZone()
    {
        return dataPackZone.L_IDPurchasebuttonDoneGemNoFree2;
    }

    public static void SetCurZonePackZone(int value)
    {
        dataPackZone.CurMap = value;
        SaveDataPackZone();
    }
    public static int GetCurZonepackZone()
    {
        return dataPackZone.CurMap;
    }
    public static void SetTimeFreePickerWheel(DateTime dateTime)
    {
        dataPickeWheel.timeFree = dateTime;
        SaveDataPickerWheel();
    }
    public static DateTime GetTimeFreePickerWheel()
    {
        return dataPickeWheel.timeFree;
    }
    public static void SetTimeFreeWithAdsPickerWheel(DateTime dateTime)
    {
        dataPickeWheel.timeAds = dateTime;
        SaveDataPickerWheel();
    }
    public static DateTime GetTimeFreeWithAdsPickerWheel()
    {
        return dataPickeWheel.timeAds;
    }
    public static void SetTimeNextDayPickerWheel(DateTime dateTime)
    {
        dataPickeWheel.timeCoolNextDay = dateTime;
        SaveDataPickerWheel();
    }
    public static DateTime GetTimeFreeNextDayPickerWheel()
    {
        return dataPickeWheel.timeCoolNextDay;
    }
    public static void SetCountSpinFree(int value)
    {
        dataPickeWheel.CountFree = value;
        SaveDataPickerWheel();
    }
    public static int GetCountSpinFree()
    {
        return dataPickeWheel.CountFree;
    }
    public static void ReloaddataTutorial()
    {
        if (!dataTutorial.IsCheckDoneTutorial)
        {
            dataTutorial = new DataTutorial();
            dataTutorial.IsCheckDoneTutorial = false;
            dataTutorial.IsCheckAddToSlotInUITeam = false;
            dataTutorial.IsCheckAddToSlotInUITeam2 = false;
            dataTutorial.IsSlideToMove = false;
            dataTutorial.IsTapToBackUITeam = false;
            dataTutorial.IsTriggerEnemy = false;
            dataTutorial.IsTapToMove = false;

            dataTutorial.IsCheckTapToAllid = false;
            dataTutorial.IsCheckWin = false;
            dataTutorial.IsChecktapButtonCatchFree = false;
            dataTutorial.isCheckTapButtonSkip = false;
            SaveDataTutorial();
        }

    }
    public static void SetQuantiyCatched(int value, int CurrentLevel)
    {
        switch (CurrentLevel)
        {
            case 1:
                dataCatched.QuantityMap1 = value;
                break;
            case 2:
                dataCatched.QuantityMap2 = value;
                break;
            case 3:
                dataCatched.QuantityMap3 = value;
                break;
            case 4:
                dataCatched.QuantityMap4 = value;
                break;
            case 5:
                dataCatched.QuantityMap5 = value;
                break;
            case 6:
                dataCatched.QuantityMap6 = value;
                break;
            case 7:
                dataCatched.QuantityMap7 = value;
                break;
            case 8:
                dataCatched.QuantityMap8 = value;
                break;
            case 9:
                dataCatched.QuantityMap9 = value;
                break;
            case 10:
                dataCatched.QuantityMap10 = value;
                break;
        }
        SaveDataCatched();
    }
    public static int GetQuantityCatchedMap(int curLevel)
    {
        switch (curLevel)
        {
            case 1:
                return dataCatched.QuantityMap1;
            case 2:
                return dataCatched.QuantityMap2;
            case 3:
                return dataCatched.QuantityMap3;
            case 4:
                return dataCatched.QuantityMap4;
            case 5:
                return dataCatched.QuantityMap5;
            case 6:
                return dataCatched.QuantityMap6;
            case 7:
                return dataCatched.QuantityMap7;
            case 8:
                return dataCatched.QuantityMap8;
            case 9:
                return dataCatched.QuantityMap9;
            case 10:
                return dataCatched.QuantityMap10;
            default:
                return 0;
        }
    }

    public static void SetNewCritter(bool value)
    {
        dataCritters.checkNew = value;
        SaveDataCritters();
    }
    public static bool GetNewCritter()
    {
        return dataCritters.checkNew;
    }

    public static void SetCurrentLanguage(string value)
    {
        dataLanguage.CurrentLanguage = value;
        SaveDataLanguage();
    }
    public static string GetCurrentLanguage()
    {
        return dataLanguage.CurrentLanguage;
    }
    public static bool GetIsDoneMoveDoor(int CurLevel)
    {
        return dataMoveDoorOfLevel.GetIsDoneMoveDoor(CurLevel);
    }
    public static void SetIsDoneMoveDoor(int CurLevel)
    {
        dataMoveDoorOfLevel.SetIsDoneMoveMap(CurLevel);
        SaveDataMoveDoor();
    }
}
public class DataUnlockFeture
{
    public bool isUnlockNoAds;
    public bool isUnlockLogin;
    public bool isUnlockZone;
    public bool isUnlockPickerWheel;
    public bool isUnlockUIMonster;
    public bool isOutPopUpRate;
    public bool isOffPopUpRate;

    public int CountEnemyCatched;
    public int CountBossCatched;
}
public class DataPickeWheel
{
    public DateTime timeAds;
    public DateTime timeFree;
    public DateTime timeCoolNextDay;
    public int CountFree;
}
public class DataRate
{
    public bool isRated;
}
public class DataAllidMerge
{
    public List<ElementData> keyValueAllid = new List<ElementData>();
    public DataAllidMerge(int count)
    {
        keyValueAllid = new List<ElementData>();
        for (int i = 0; i < count; i++)
        {
            ElementData elementData = new ElementData();
            elementData.Type = ECharacterType.NONE;
            keyValueAllid.Add(elementData);
        }
    }

    public void AddAllidItemMerge(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == ECharacterType.NONE)
            {
                keyValueAllid[i].Type = m_elementData.Type;
                keyValueAllid[i].ID = m_elementData.ID;
                keyValueAllid[i].HP = m_elementData.HP;
                break;
            }
        }
    }

    public void RemoveItemMerge(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                keyValueAllid[i].Type = ECharacterType.NONE;
                keyValueAllid[i].ID = 0;
                keyValueAllid[i].HP = 0;
                break;
            }
        }
    }

    public void ResetItemMerge()
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            keyValueAllid[i].Type = ECharacterType.NONE;
        }
    }
}
public class DataAllid
{
    public List<ElementData> keyValueAllid = new List<ElementData>();
    public bool isActiveSlot4;

    DataInfoPlayer m_dataInfo = new DataInfoPlayer();

    public DataAllid(int count, bool _isActiveSlot4)
    {
        keyValueAllid = new List<ElementData>();
        isActiveSlot4 = _isActiveSlot4;
        for (int i = 0; i < count; i++)
        {
            ElementData elementData = new ElementData();
            elementData.Type = ECharacterType.NONE;
            keyValueAllid.Add(elementData);
        }
    }
    public void SetSlot4(bool _value)
    {
        isActiveSlot4 = _value;
    }
    public bool GetSlot4()
    {
        return isActiveSlot4;
    }

    public void AddAllidItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == ECharacterType.NONE)
            {
                keyValueAllid[i].Type = m_elementData.Type;
                keyValueAllid[i].ID = m_elementData.ID;
                keyValueAllid[i].HP = m_elementData.HP;
                keyValueAllid[i].Rarity = m_elementData.Rarity;
                keyValueAllid[i].RangeAttack = m_elementData.RangeAttack;
                break;
            }
        }
    }

    public void RemoveItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                keyValueAllid[i].Type = ECharacterType.NONE;
                keyValueAllid[i].ID = 0;
                keyValueAllid[i].HP = 0;
                break;
            }
        }
    }
    public void Clear()
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {

            keyValueAllid[i].Type = ECharacterType.NONE;
            keyValueAllid[i].ID = 0;
            keyValueAllid[i].HP = 0;
        }
    }

    public void SetHP(int HP, ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                m_elementData.HP = HP;
                if (m_elementData.HP < 0)
                {
                    m_elementData.HP = 0;
                }
                keyValueAllid[i].HP = m_elementData.HP;
            }
        }
    }
}

public class UnLockNoAds
{
    public bool isUnlockDone;

}
public class DataCritters
{
    public List<ECharacterType> L_Type = new List<ECharacterType>();
    public bool checkNew;

    public void AddListType(ECharacterType type)
    {
        if (!L_Type.Contains(type))
        {
            L_Type.Add(type);
            checkNew = true;
        }
    }

    public List<ECharacterType> GetListCritters()
    {
        return L_Type;
    }
    public void ReloadDataCritter(DataCritters data)
    {
        data = new DataCritters();
    }
}
public class DataPackLogin
{
    public bool UnlockGemWithDolar;
    public int CountDay;
    public DateTime dateTime;

    public List<int> L_IDPurchasebuttonDoneGemFree;
    public List<int> L_IDPurchasebuttonDoneGemNoFree;
}
public class DataPackLevel
{
    public bool UnLockRewardsWithDolar;
    public int CurMap;
    public List<int> List_UnLock_Zone = new List<int>();

    public List<int> L_IDPurchasebuttonDoneGemFree;
    public List<int> L_IDPurchasebuttonDoneGemNoFree1;
    public List<int> L_IDPurchasebuttonDoneGemNoFree2;
}
public class DataPackShop
{
    public int quantityChestNormal;
    public int quantityChestEpic;
    public int quantityChestLegend;

    public List<int> L_ID = new List<int>();
}
public class DataTutorial
{
    public bool IsCheckTapGotIt;

    public bool IsCheckDoneTutorial;
    public bool IsCheckAddToSlotInUITeam;
    public bool IsCheckAddToSlotInUITeam2;
    public bool IsCheckTapAddSlot;
    public bool IsTapToBackUITeam;
    public bool IsTapToMove;
    public bool IsSlideToMove;
    public bool IsTriggerEnemy;


    public bool IsCheckTapToAllid;
    public bool IsCheckWin;
    public bool IsChecktapButtonCatchFree;
    public bool IsCheckTapButtonClaim;
    public bool isCheckTapButtonSkip;


    public bool isCheckTapBag;
    public bool isCheckTapMergeBtn;
    public bool isChecktapSlotMerge;
    public bool isCheckTapButtonOnMerge;
    public bool isCheckSkipPopUpMerge;
    public bool isCheckOutUIMerge;
    public bool isChecktapButtonTeam;


    public bool IsTapHealHp;
    public bool IsCheckTapCloseBag;
    public bool IsCheckActivetext;

    public bool IsCheckSelectMap1;

    public void ReLoad(DataTutorial data)
    {
        data = new DataTutorial();
    }
    public void SetIsCheckSelectMap1(bool value)
    {
        IsCheckSelectMap1 = value;
    }
    public bool GetIsCheckSelectMap1()
    {
        return IsCheckSelectMap1;
    }

    public void SetIsCheckTapCloseBag(bool Value)
    {
        IsCheckTapCloseBag = Value;
    }
    public bool GetIsCheckTapCloseBag()
    {
        return IsCheckTapCloseBag;
    }
    public bool GetIsCheckActivetext()
    {
        return IsCheckActivetext;
    }
    public void SetIsCheckActivetext(bool value)
    {
        IsCheckActivetext = value;
    }
    public bool GetIsCheckTapButtonteam()
    {
        return isChecktapButtonTeam;
    }
    public void SetIsCheckTapButtonTeam(bool value)
    {
        isChecktapButtonTeam = value;
    }
    public bool GetIsCheckTapBag()
    {
        return isCheckTapBag;
    }
    public bool GetisCheckTapMergeBtn()
    {
        return isCheckTapMergeBtn;
    }
    public bool getisCheckTapSLotMerge()
    {
        return isChecktapSlotMerge;
    }
    public bool GetIsCheckSkipPopUpMerge()
    {
        return isCheckSkipPopUpMerge;
    }
    public bool GetIsCheckOutUiMerge()
    {
        return isCheckOutUIMerge;
    }
    public bool GetIsCheckButtonOnMerge()
    {
        return isCheckTapButtonOnMerge;
    }
    public bool GetIsHealHp()
    {
        return IsTapHealHp;
    }
    public void SetIsTapToHealHp(bool value)
    {
        IsTapHealHp = value;
    }

    public void SetIsCheckButtonOnMerge(bool value)
    {
        isCheckTapButtonOnMerge = value;
    }
    public void SetIsCheckTapBag(bool Value)
    {
        isCheckTapBag = Value;
    }
    public void SetIsCheckTapMergeBtn(bool Value)
    {
        isCheckTapMergeBtn = Value;
    }
    public void SetIsCheckTapSlotMerge(bool value)
    {
        isChecktapSlotMerge = value;
    }
    public void SetIsCheckTapButtonMerge(bool value)
    {
        isCheckTapMergeBtn = value;
    }
    public void SetIsChecktapSKipPopUpMerge(bool value)
    {
        isCheckSkipPopUpMerge = value;
    }
    public void SetisCheckOutUIMerge(bool value)
    {
        isCheckOutUIMerge = value;
    }


    public bool GetIsCheckTapGotIt()
    {
        return IsCheckTapGotIt;
    }
    public bool GetIsCheckDoneTutorial()
    {
        return IsCheckDoneTutorial;
    }
    public bool GetIsCheckAddToSlot()
    {
        return IsCheckAddToSlotInUITeam;
    }
    public bool GetIsCheckTapAddSlot()
    {
        return IsCheckTapAddSlot;
    }
    public bool GetIsTapToBackUITeam()
    {
        return IsTapToBackUITeam;
    }
    public bool GetIsTapToMove()
    {
        return IsTapToMove;
    }
    public bool GetIsSlideToMove()
    {
        return IsSlideToMove;
    }
    public bool GetIsTriggerEnemy()
    {
        return IsTriggerEnemy;
    }
    public bool GetIsCheckTapButtonClaim()
    {
        return IsCheckTapButtonClaim;
    }

    public void SetIsCheckTapGotIt(bool value)
    {
        IsCheckTapGotIt = value;
    }
    public void SetIsCheckTapAddSlot(bool value)
    {
        IsCheckTapAddSlot = value;
    }
    public void SetIsCheckDoneTutorial(bool value)
    {
        IsCheckDoneTutorial = value;
    }

    public void SetIsCheckAddToSlot(bool value)
    {
        IsCheckAddToSlotInUITeam = value;
    }
    public void SetIsTapToBackUITeam(bool value)
    {
        IsTapToBackUITeam = value;
    }
    public void SetIsTapToMove(bool value)
    {
        IsTapToMove = value;
    }
    public void SetIsSlideToMove(bool value)
    {
        IsSlideToMove = value;
    }
    public void SetIsTriggerEnemy(bool value)
    {
        IsTriggerEnemy = value;
    }

    public void SetIsCheckTapToAllid(bool value)
    {
        IsCheckTapToAllid = value;
    }
    public void SetIsCheckWin(bool value)
    {
        IsCheckWin = value;
    }
    public void SetIsChecktapButtonCatchFree(bool value)
    {
        IsChecktapButtonCatchFree = value;
    }
    public void SetIsCheckTapButtonClaim(bool value)
    {
        IsCheckTapButtonClaim = value;
    }
    public void SetisCheckTapButtonSkip(bool value)
    {
        isCheckTapButtonSkip = value;
    }
    public bool getIsCheckTapToAllid()
    {
        return IsCheckTapToAllid;
    }
    public bool getIsCheckWin()
    {
        return IsCheckWin;
    }
    public bool GetIsChecktapButtonCatchFree()
    {
        return IsChecktapButtonCatchFree;
    }
    public bool GetIsCheckTapButtonSkip()
    {
        return isCheckTapButtonSkip;
    }

}
public class DataInfoPlayer
{
    public bool isCheckNewUser = false;
    public Dictionary<ECharacterType, List<ElementData>> keyValuePairs = new Dictionary<ECharacterType, List<ElementData>>();
    public bool CheckKey(ECharacterType Key)
    {
        return keyValuePairs.ContainsKey(Key);
    }
    public void ClearAllData()
    {
        keyValuePairs.Clear();
    }
    public void Add(ECharacterType Key)
    {
        if (keyValuePairs.ContainsKey(Key))
        {
            List<ElementData> L_elementData = keyValuePairs[Key];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = L_elementData[L_elementData.Count - 1].ID + 1;
            m_elemenData.HP = Controller.Instance.enemyData.GetHPEmemy(Key);
            m_elemenData.Rarity = Controller.Instance.enemyData.EnemyStatIndex(Key).Rarity;
            m_elemenData.RangeAttack = Controller.Instance.enemyData.EnemyStatIndex(Key).RangeAttack;

            L_elementData.Add(m_elemenData);

            keyValuePairs[Key] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = 0;
            m_elemenData.HP = Controller.Instance.enemyData.GetHPEmemy(Key);
            m_elemenData.Rarity = Controller.Instance.enemyData.EnemyStatIndex(Key).Rarity;
            m_elemenData.RangeAttack = Controller.Instance.enemyData.EnemyStatIndex(Key).RangeAttack;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(Key, L_elementData);
        }
    }
    public void Add(ElementData elementData)
    {
        if (keyValuePairs.ContainsKey(elementData.Type))
        {
            List<ElementData> L_elementData = keyValuePairs[elementData.Type];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            m_elemenData.Rarity = elementData.Rarity;
            m_elemenData.RangeAttack = elementData.RangeAttack;

            L_elementData.Add(m_elemenData);

            keyValuePairs[elementData.Type] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            m_elemenData.Rarity = elementData.Rarity;
            m_elemenData.RangeAttack = elementData.RangeAttack;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(m_elemenData.Type, L_elementData);
        }
    }
    public void Remove(ECharacterType Key, int ID)
    {

        List<ElementData> L_elementData = keyValuePairs[Key];
        if (L_elementData.Count == 1)
        {
            keyValuePairs.Remove(Key);
        }
        else
        {
            for (int i = 0; i < L_elementData.Count; i++)
            {
                if (L_elementData[i].ID == ID)
                {
                    L_elementData.RemoveAt(i);
                    break;
                }
            }
        }
    }
    public void SetHP(ECharacterType Key, int ID, int HP)
    {
        List<ElementData> L_elementData = keyValuePairs[Key];

        for (int i = 0; i < L_elementData.Count; i++)
        {
            if (L_elementData[i].ID == ID)
            {
                L_elementData[i].HP = HP;
                if (L_elementData[i].HP < 0)
                {
                    L_elementData[i].HP = 0;
                }
            }
        }
    }
}
public class DataSpinWheel
{
    public int CountSpin;
    public DateTime time;
}
public class DataCoin
{
    public int coin = 100;

    public DataCoin(int coin)
    {
        this.coin = coin;
    }

    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
}
public class DataGem
{
    public int Gem = 0;
}
public class DataPoint
{
    public Vector3 PlayerPosWhenExit;
    public List<Vector3> L_CheckPointPos = new List<Vector3>();
    public Vector3 StartPoint;

    public void AddCheckPointPosToList(Transform Pos)
    {
        L_CheckPointPos.Add(Pos.position);
    }
    public void SetPlayerPosWhenExit(Vector3 Pos)
    {
        PlayerPosWhenExit = Pos;
    }
    public Vector3 GetPlayerPosWhenExit()
    {
        return PlayerPosWhenExit;
    }
    public List<Vector3> GetListCheckPointPos()
    {
        return L_CheckPointPos;
    }
}
public class DataQuantitCritterCatched
{
    public int QuantityMap1;
    public int QuantityMap2;
    public int QuantityMap3;
    public int QuantityMap4;
    public int QuantityMap5;
    public int QuantityMap6;
    public int QuantityMap7;
    public int QuantityMap8;
    public int QuantityMap9;
    public int QuantityMap10;
}
public class DataBuyChest
{
    public DateTime timeOutAdsChestNormal;
    public DateTime timeOutAdsChestEpic;
    public DateTime timOutAdsChestLegend;
    public DateTime TimeNextDay;

    public int CountAdsNormal;
    public int CountAdsEpic;
    public int CountAdsLegend;

    public long NextTime;

    public bool isCheckNewUserWithChestEpic;
    public bool isCheckNewUserWithChestLegend;
}
public class DataLevel
{
    public List<int> List_Curent_Level = new List<int>();
    public int CurLevel;
    public bool isFirstLevelUp;

    public void AddListLevel(int value)
    {
        if (!List_Curent_Level.Contains(value))
        {
            List_Curent_Level.Add(value);
            isFirstLevelUp = true;
        }
    }
    public void SetisFirstLevelUp(bool value)
    {
        isFirstLevelUp = value;
    }

    public void SetCurLevel(int value)
    {
        CurLevel = value;
    }
    public int GetCurLevel()
    {
        return CurLevel;
    }
    public List<int> GetListCurrentLevel()
    {
        return List_Curent_Level;
    }
}

public class DataLanguage
{
    public string CurrentLanguage;
}

public class DataSound
{
    public int value;
    public void SetValueVolume(int value)
    {
        this.value = value;
    }
    public int GetValueVolume()
    {
        return value;
    }
}

public class DataMoveDoorOfLevel
{
    public bool IsDoneMoveMap1;
    public bool IsDoneMoveMap2;
    public bool IsDoneMoveMap3;
    public bool IsDoneMoveMap4;
    public bool IsDoneMoveMap5;
    public bool IsDoneMoveMap6;
    public bool IsDoneMoveMap7;


    public void SetIsDoneMoveMap(int curlevel)
    {
        switch (curlevel)
        {
            case 1:
                IsDoneMoveMap1 = true;
                break;
            case 2:
                IsDoneMoveMap2 = true;

                break;
            case 3:
                IsDoneMoveMap3 = true;
                break;
            case 4:
                IsDoneMoveMap4 = true;
                break;
            case 5:
                IsDoneMoveMap5 = true;
                break;
            case 6:
                IsDoneMoveMap6 = true;
                break;
            case 7:
                IsDoneMoveMap7 = true;
                break;
        }
    }
    public bool GetIsDoneMoveDoor(int Curlevel)
    {
        switch (Curlevel)
        {
            case 1:
                return IsDoneMoveMap1;
            case 2:
                return IsDoneMoveMap2;
            case 3:
                return IsDoneMoveMap3;
            case 4:
                return IsDoneMoveMap4;
            case 5:
                return IsDoneMoveMap5;
            case 6:
                return IsDoneMoveMap6;
            case 7:
                return IsDoneMoveMap7;
            default:
                return false;
        }
    }
}


public class ElementData
{
    public ECharacterType Type;
    public E_RangeAttack RangeAttack;
    public int ID;
    public int HP;
    public int Rarity;
}


