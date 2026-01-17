using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheatManager : MonoBehaviour
{
    public GameObject Cheat;

    public Button ActiveCheatBtn;
    public Button HiddenCheatBtn;

    public InputField inputNameCritter;
    public InputField inputCoin;

    public Button AddCritter;
    public Button AddCoin;
    public Button UnLockALlCritterBtn;
    public Button UnLockAllMaps;
    public Button CheatCurrentcyBtn;
    public Button DownTimeScalebtn;
    public Button UpTimeScalebtn;
    public Text timeScaleTxt;
    public UI_CoinManager m_Uicoin;

    private void Awake()
    {
        AddCritter.onClick.AddListener(OnclickbuttonAddCritter);
        AddCoin.onClick.AddListener(OnclickButtonAddCoin);
        ActiveCheatBtn.onClick.AddListener(OnClickButtonActiveCheat);
        HiddenCheatBtn.onClick.AddListener(OnClickButtonHiddenCheat);
        UnLockALlCritterBtn.onClick.AddListener(UnLockAllCritter);
        UnLockAllMaps.onClick.AddListener(UnLockAllMap);
        CheatCurrentcyBtn.onClick.AddListener(CheatCurrentCy);
        DownTimeScalebtn.onClick.AddListener(DownTimeScale);
        UpTimeScalebtn.onClick.AddListener(UptimeScale);
        timeScaleTxt.text = Time.timeScale.ToString();
    }
    private void OnClickButtonActiveCheat()
    {
        Cheat.SetActive(true);
        ActiveCheatBtn.gameObject.SetActive(false);
        HiddenCheatBtn.gameObject.SetActive(true);
    }
    private void OnClickButtonHiddenCheat()
    {
        Cheat.SetActive(false);
        HiddenCheatBtn.gameObject.SetActive(false);
        ActiveCheatBtn.gameObject.SetActive(true);
    }
    private void OnclickButtonAddCoin()
    {
        int Coin = DataPlayer.GetCoin();
        Coin += int.Parse(inputCoin.text);
        DataPlayer.SetCoin(Coin);
        m_Uicoin.SetTextCoin();
    }
    private void UnLockAllCritter()
    {
        for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            if (Controller.Instance.enemyData.enemies[i].Rarity <= 8)
            {
                DataPlayer.Add(Controller.Instance.enemyData.enemies[i].Type);
                DataPlayer.AddCritter(Controller.Instance.enemyData.enemies[i].Type);
            }
        }
    }
    private void UnLockAllMap()
    {
        for (int i = 1; i <= 8; i++)
        {
            DataPlayer.AddListLevel(i);
        }
    }
    private void OnclickbuttonAddCritter()
    {
        DataPlayer.Add(Utils.ToEnum<ECharacterType>(inputNameCritter.text));
        DataPlayer.AddCritter(Utils.ToEnum<ECharacterType>(inputNameCritter.text));
    }
    private void CheatCurrentCy()
    {
        DataPlayer.SetCoin(1000000);
        DataPlayer.SetGem(1000000);

        UI_Home.Instance.m_UICoinManager.SetTextCoin();
        UI_Home.Instance.m_UIGemManager.SetTextGem();
    }
    private void DownTimeScale()
    {
        if(Time.timeScale >= 2)
        {
            Time.timeScale--;
            timeScaleTxt.text = Time.timeScale.ToString();
        }
    }
    private void UptimeScale()
    {
        if (Time.timeScale <= 2)
        {
            Time.timeScale++;
            timeScaleTxt.text = Time.timeScale.ToString();
        }
    }
}
