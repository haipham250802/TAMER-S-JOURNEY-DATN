using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class PopUpNewUser : MonoBehaviour
{
    public SkeletonGraphic Critter_01;
    public SkeletonGraphic Critter_02;

    public CritterFollowController m_CritterFollowController;
    public UI_ShowAllid m_UiSHowAllid;
    public Button GotItbtn;

    ElementData elementData01;
    ElementData elementData02;
    
   

    public void OnCLickButtonGotTt()
    {
        if(!DataPlayer.GetIsTapToMove() && player.Instance.HandTut)
        {
          //  player.Instance.HandTut.SetActive(true);
            this.gameObject.SetActive(false);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_MOVE");
            ExampleStoryTut.Instance.SetText(str);
            Controller.Instance.exampObj.gameObject.SetActive(true);

        }
        if (gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        TutorialManager.Instance.DeSpawn();
        if (!DataPlayer.GetIsTapGotIt())
        {
            DataPlayer.AddAlliedIteam(elementData01);
            DataPlayer.AddAlliedIteam(elementData02);

            m_UiSHowAllid.LoadAllidBaseShow();
            m_CritterFollowController.LoadCritterFollow();

            // DataPlayer.SetIsNewUser(true);
        }
        DataPlayer.SetIsTapGotIt(true);

        if (DataPlayer.GetIsCheckDoneTutorial())
            return;
      /*  if (!DataPlayer.GetIsTapToMove())
        {
            TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_Player.TabToMoveBtn.transform, new Vector3(30, -70));
        }*/
    }

    private void Awake()
    {/*
        if(!DataPlayer.GetIsCheckDoneTutorial())
        {
            this.gameObject.SetActive(false);
        }
        if(!DataPlayer.GetIsNewUser() && DataPlayer.GetIsCheckDoneTutorial())
        {
           
        }
*/
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

    }
    private void Start()
    {
        EnemyStat statEnemy = Controller.Instance.GetStatEnemy(DataPlayer.GetListCritters()[0]);
        Critter_01.skeletonDataAsset = null;
        Critter_01.skeletonDataAsset = statEnemy.ICON;
        Critter_01.Initialize(true);
        Critter_01.AnimationState.SetAnimation(0, "Idle", true);
        elementData01 = new ElementData();
        elementData01.Type = statEnemy.Type;
        elementData01.Rarity = statEnemy.Rarity;
        elementData01.HP = statEnemy.HP;
        elementData01.ID = statEnemy.ID;

        EnemyStat statEnemy1 = Controller.Instance.GetStatEnemy(DataPlayer.GetListCritters()[1]);
        Critter_02.skeletonDataAsset = null;
        Critter_02.skeletonDataAsset = statEnemy1.ICON;
        Critter_02.Initialize(true);
        Critter_02.AnimationState.SetAnimation(0, "Idle", true);
        elementData02 = new ElementData();
        elementData02.Type = statEnemy1.Type;
        elementData02.Rarity = statEnemy1.Rarity;
        elementData02.HP = statEnemy1.HP;
        elementData02.ID = statEnemy1.ID;

        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.1f);
        if (DataPlayer.GetIsTapGotIt())
        {
            this.gameObject.SetActive(false);
        }
        if (!DataPlayer.GetIsTapGotIt())
        {
            GotItbtn.onClick.AddListener(OnCLickButtonGotTt);
        }
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            TutorialManager.Instance.SpawnHandUIHome(transform, new Vector3(30, -130));
          
        }
       
    }
}
