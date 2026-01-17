using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class PopUpRewardCritter : MonoBehaviour
{
    public SkeletonGraphic Skeleton1;
    public SkeletonGraphic Skeleton2;
    public Button GotIt;

    public CritterFollowController m_CritterFollowController;
    public UI_ShowAllid m_UiSHowAllid;

    ElementData elementData;
    ElementData elementData2;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        Skeleton1.AnimationState.SetAnimation(0, "Idle", true);
        Skeleton2.AnimationState.SetAnimation(0, "Idle", true);
    }
    private void Start()
    {
        GotIt.onClick.AddListener(OnclickButtonGotIt);
    }
    public void OnclickButtonGotIt()
    {
        if (!DataPlayer.GetIsNewUser())
        {
            DataPlayer.Add(ECharacterType.Mishmash);
            DataPlayer.Add(ECharacterType.Tuber);
            DataPlayer.AddCritter(ECharacterType.Mishmash);
            DataPlayer.AddCritter(ECharacterType.Tuber);
          

            DataPlayer.SetIsNewUser(true);

            for (int i = 0; i < DataPlayer.GetListCritters().Count; i++)
            {
                EnemyStat statEnemy = Controller.Instance.GetStatEnemy(DataPlayer.GetListCritters()[i]);
                elementData = new ElementData();
                elementData.Type = statEnemy.Type;
                elementData.Rarity = statEnemy.Rarity;
                elementData.HP = statEnemy.HP;
                elementData.ID = statEnemy.ID;
                DataPlayer.AddAlliedIteam(elementData);
            }

            m_UiSHowAllid.LoadAllidBaseShow();
            m_CritterFollowController.LoadCritterFollow();
            this.gameObject.SetActive(false);
        }
    }
}
