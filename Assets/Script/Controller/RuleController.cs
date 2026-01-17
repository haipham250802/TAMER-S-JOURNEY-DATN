using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class RuleController : MonoBehaviour
{
    public int CurLevel;
    public List<Enemy> L_enemy = new List<Enemy>();
    public List<Enemy> L_enemy2 = new List<Enemy>();
    public List<BossPatrol> L_boss = new List<BossPatrol>();
    public StoryTutorial storyTutorial;
    public GameObject TeleMap;
    public GameObject EnemyPrefabs;
    public GameObject StoryBoss;
    int SumSoldier = 0;
    int CurKillSoldier = 0;
    public int Count = 0;
    public int QuantityEnemy;
    public GameObject objEnemy;

    public Transform PosDoor;
    public SkeletonAnimation SkeletonAnimation;
    Spine.EventData eventData;

    private void Start()
    {
        ArrowDirection.Instance.gameObject.SetActive(false);
    }
    IEnumerator IE_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        if (DataPlayer.GetIsDoneMoveDoor(CurLevel))
        {
            TeleMap.SetActive(true);    
        }
        if (StoryBoss != null)
            StoryBoss.SetActive(false);
        if (!DataPlayer.GetIsCheckDoneTutorial())
            ArrowDirection.Instance.Target = L_enemy[0].transform;
        else
        {

        }
        if (CurLevel > 0)
        {
            QuantityEnemy = Controller.Instance.dataquantityOfZone.dataQuantityOfZone[CurLevel - 1].Zone;
            TaskPartZone.Instance.Quantity = this.QuantityEnemy;
            TaskPartZone.Instance.RenderNotiHole();
        }
        else
        {
            QuantityEnemy = 5;
            TaskPartZone.Instance.Quantity = this.QuantityEnemy;
            TaskPartZone.Instance.RenderNotiHole();
        }

        if (DataPlayer.GetQuantityCatchedMap(CurLevel) > QuantityEnemy)
        {
            DataPlayer.SetQuantiyCatched(QuantityEnemy, CurLevel);
        }


        Count = DataPlayer.GetQuantityCatchedMap(CurLevel);
        //  QuestArrow.Instance.Target = L_enemy2[0];
        //  SpawnEnemy();
       // TaskPartZone.Instance.ActiveIconNotiBoss(QuantityEnemy + 1);
        TaskPartZone.Instance.LoadTasking(DataPlayer.GetQuantityCatchedMap(CurLevel));
        if (DataPlayer.GetQuantityCatchedMap(CurLevel) == QuantityEnemy)
        {
            L_boss[0].gameObject.SetActive(true);
        }
        StartCoroutine(IE_DelayBoss());
    }
    public void MoveDoorAtStartGame()
    {
        StartCoroutine(IE_delayMoveDoor());
    }
    IEnumerator IE_delayMoveDoor()
    {
        yield return new WaitForSeconds(1f);
        if (TeleMap)
        {
            if (DataPlayer.GetListLevel().Contains(CurLevel + 1))
            {
                DoSomethingKillDoneBoss();
            }
        }
    }

    private void Update()
    {
        if (DataPlayer.GetEnemyCatched() == 4 && !DataPlayer.GetUnLockNoAds())
        {
            popUpManager.Instance.m_PopUpNoAds.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.NoAdsObj.SetActive(true);
            DataPlayer.SetUnLockNoAds(true);
        }
        if (DataPlayer.GetEnemyCatched() == 2 && !DataPlayer.GetUnLockLogin())
        {
            popUpManager.Instance.m_PopUPpackOnline.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.OnlineObj.SetActive(true);
            DataPlayer.SetUnLockLogin(true);
        }
        if (DataPlayer.GetBossCatched() == 1 && !DataPlayer.GetUnLockZone() && UI_Home.Instance.CanShowUIBattle() &&
            DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.getCurLevel() == 2)
        {
            popUpManager.Instance.m_PopUpPackZone.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.ZoneObj.SetActive(true);
            DataPlayer.SetUnLockZone(true);
        }
        if (DataPlayer.GetEnemyCatched() == 6 && !DataPlayer.GetUnLockPikcerWheel())
        {
            popUpManager.Instance.m_PickerWheel.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.LuckyWheel.SetActive(true);

            DataPlayer.SetUnLockPickerWheel(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 1 && !DataPlayer.GetUnlockNonster())
        {
            BagManager.Instance.OnClickMonsterButton();
            UI_Home.Instance.m_UiCritter.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.InfoObj.SetActive(true);
            DataPlayer.SetUnLockMonster(true);
        }

        if (L_enemy2.Count > 0)
        {
            if (L_boss.Count > 0)
            {
                if (!L_boss[0].gameObject.activeInHierarchy)
                {
                    QuestArrow.Instance.Target = GetNearestEnemy(L_enemy2).transform;
                    QuestArrow.Instance.GetComponent<SpriteRenderer>().sprite = QuestArrow.Instance.IconEnemy;
                    Vector3 POs = GetNearestEnemy(L_enemy2).transform.position;
                    Vector3 POs2 = player.Instance.transform.position;
                    QuestArrow.Instance.Arrow.GetComponent<ArrowDirectionFollowEnemy>().LimitDistance = 0.4f;
                    float Dis = Vector3.Distance(POs, POs2);
                    if (Dis <= 15)
                    {
                        QuestArrow.Instance.gameObject.SetActive(false);
                    }
                    else
                    {
                        QuestArrow.Instance.gameObject.SetActive(true);
                    }
                }
            }
        }
        if (L_boss.Count > 0)
        {
            if (L_boss[0].gameObject.activeInHierarchy)
            {
                QuestArrow.Instance.GetComponent<SpriteRenderer>().sprite = QuestArrow.Instance.IconBoss;
                QuestArrow.Instance.Target = L_boss[0].transform;
                Vector3 POs = L_boss[0].transform.position;
                Vector3 POs2 = player.Instance.transform.position;
                float Dis = Vector3.Distance(POs, POs2);
                QuestArrow.Instance.Arrow.GetComponent<ArrowDirectionFollowEnemy>().LimitDistance = 0.7f;

                if (Dis <= 5)
                {
                    QuestArrow.Instance.gameObject.SetActive(false);
                }
                else
                {
                    QuestArrow.Instance.gameObject.SetActive(true);
                }
            }
        }
    }
    public Enemy GetNearestEnemy(List<Enemy> L_enemies)
    {

        if (L_enemies.Count == 1)
        {
            return L_enemies[0];
        }
        else if (L_enemies.Count > 1)
        {
            int K = 0;
            if (L_enemies[0])
            {
                float Dis = Vector2.Distance(player.Instance.transform.position, L_enemies[0].gameObject.transform.position);
                for (int i = 0; i < L_enemies.Count; i++)
                {
                    if (!L_enemies[i].gameObject.activeSelf)
                    {
                        continue;
                    }
                    if (L_enemies[i].gameObject.activeSelf)
                    {
                        if (Vector2.Distance(player.Instance.transform.position, L_enemies[i].gameObject.transform.position) < Dis)
                        {
                            Dis = Vector2.Distance(player.Instance.transform.position, L_enemies[i].gameObject.transform.position);
                            K = i;
                        }
                    }
                }
                return L_enemies[K];
            }
        }
        return null;
    }
    IEnumerator IE_DelayBoss()
    {
        yield return new WaitForSeconds(2f);
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Count == QuantityEnemy)
            {
                L_boss[0].gameObject.SetActive(true);
                if (DataPlayer.GetIsCheckDoneTutorial())
                {
                    MoveToBoss();
                    StoryBoss.SetActive(true);
                }
            }
        }
    }
    public void SpawnEnemy()
    {
        for (int i = 0; i < QuantityEnemy; i++)
        {
            var obj = SimplePool.Spawn(EnemyPrefabs, Vector3.zero, Quaternion.identity);
            L_enemy.Add(obj.GetComponent<Enemy>());
        }
    }
    public void AddListEnemy(Enemy enemy)
    {
        L_enemy.Add(enemy);
        SumSoldier += 1;

        UI_Home.Instance.m_TaskManager.SetTextTask(CurKillSoldier.ToString(), SumSoldier.ToString());
    }
    public void AddListEnemy2(Enemy enemy)
    {
        L_enemy.Add(enemy);
        //  SumSoldier += 1;

        // UI_Home.Instance.m_TaskManager.SetTextTask(CurKillSoldier.ToString(), SumSoldier.ToString());
    }
    public void AddListBoss(BossPatrol boss)
    {
        L_boss.Add(boss);
    }
    public void HiddenBoss()
    {
        for (int i = 0; i < L_boss.Count; i++)
        {
            L_boss[i].gameObject.SetActive(false);
        }
    }
    public void Remove(Character m_character)
    {
        /*if (m_character is Enemy)
        {
            L_enemy.Remove((Enemy)m_character);
            ((Enemy)m_character).gameObject.SetActive(false);

            CurKillSoldier++;
            Debug.Log(CurKillSoldier);
            UI_Home.Instance.m_TaskManager.SetTextTask(CurKillSoldier.ToString(), SumSoldier.ToString());
        }*/
    }

    public void HiddenEnemy(Character character)
    {
        if (character is Enemy)
        {
            character.gameObject.SetActive(false);
            Count = DataPlayer.GetQuantityCatchedMap(CurLevel);
            Count++;
            DataPlayer.SetEnemyCatched();
            DataPlayer.SetQuantiyCatched(Count, CurLevel);

            TaskPartZone.Instance.LoadTasking(DataPlayer.GetQuantityCatchedMap(CurLevel));
            Debug.Log("da giet");
            if (!L_boss[0].gameObject.activeInHierarchy)
            {
                TaskPartZone.Instance.LoadTasking(Count);
                Debug.Log("da den giet");
            }
            if (Count == QuantityEnemy)
            {
                L_boss[0].gameObject.SetActive(true);
                if (DataPlayer.GetIsCheckDoneTutorial() && Controller.Instance.isMoveBoss)
                {
                    MoveToBoss();
                    StoryBoss.SetActive(true);
                    Controller.Instance.isMoveBoss = false;
                }
            }
        }
        if (DataPlayer.GetIsCheckDoneTutorial())
            StartCoroutine(IE_Spawn(character));
    }
    public void MoveToBoss()
    {
        int count = 0;
        /* for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
         {
             if (DataPlayer.GetListLevel()[i] > count)
             {
                 count = DataPlayer.GetListLevel()[i];
             }
         }*/
        if (L_boss.Count > 0)
            VirtualCamera.Instance.MoveToObject(L_boss[0].transform);

        ArrowDirection.Instance.gameObject.SetActive(true);
        if (L_boss.Count > 1)
            ArrowDirection.Instance.Target = L_boss[0].transform;
        //    ArrowDirection.Instance.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        /* }*/
     //   TaskPartZone.Instance.ActiveIconNotiBoss(QuantityEnemy + 1);
        Controller.Instance.isMoveBoss = false;
    }
    public void MoveToDoor()
    {
        if (!DataPlayer.GetIsDoneMoveDoor(CurLevel))
        {
            VirtualCamera.Instance.MoveToObject(PosDoor, CallbackMoveDoor);
            DataPlayer.SetIsDoneMoveDoor(CurLevel);
        }
    }
    void CallbackMoveDoor()
    {
        if (SkeletonAnimation.skeletonDataAsset)
        {
            SkeletonAnimation.AnimationState.SetAnimation(0, "animation", false);
            SkeletonAnimation.AnimationState.Event += AnimationState_Event;
        }
           
    }

    private void AnimationState_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        SkeletonAnimation.AnimationState.Event -= AnimationState_Event;
        if (e.Data.Name == "Attack")
        {
            TeleMap.SetActive(true);
        }
    }

    IEnumerator IE_Spawn(Character enemy)
    {
        yield return new WaitForSeconds(30);
        if (!enemy.gameObject.activeInHierarchy)
        {
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<Collider2D>().enabled = true;
        }
    }
    public void RemoveBoss(Character m_character)
    {
        if (m_character is BossPatrol)
        {
            L_boss.Remove((BossPatrol)m_character);
            ((BossPatrol)m_character).gameObject.SetActive(false);
            Debug.Log("Chuyen man");
            ArrowDirection.Instance.gameObject.SetActive(false);
            DataPlayer.SetQuantiyCatched(0, CurLevel);
            /*            Count = DataPlayer.GetQuantityCatchedMap(CurLevel);
            *//*            TaskPartZone.Instance.RenderNotiHole();
            */
            TaskPartZone.Instance.RenderNotiHole();
          //  TaskPartZone.Instance.ActiveIconNotiBoss(QuantityEnemy + 1);
            TaskPartZone.Instance.LoadTasking(DataPlayer.GetQuantityCatchedMap(CurLevel));

            StoryBoss.SetActive(false);
            /*   if (!TeleMap.activeInHierarchy)
                   TeleMap.SetActive(true);*/
            //  BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();

            DataPlayer.SetBossCatched();
        }
    }
    public void ActiveBoss()
    {
        if (L_enemy.Count == 0)
        {
            for (int i = 0; i < L_boss.Count; i++)
            {
                L_boss[i].gameObject.SetActive(true);
            }
        }
    }
    public void StopEnemy()
    {
        for (int i = 0; i < L_enemy.Count; i++)
        {
            L_enemy[i].m_MoveEnemy.agent.maxSpeed = 0;
            L_enemy[i].isCanAI = true;
        }
        for (int i = 0; i < L_boss.Count; i++)
        {
            L_boss[i].m_MoveEnemy.agent.maxSpeed = 0;
            L_boss[i].isCanAI = true;
        }
    }
    public void UnStopEnemy()
    {
        for (int i = 0; i < L_enemy.Count; i++)
        {
            L_enemy[i].m_MoveEnemy.agent.maxSpeed = 3.5f;
            if (UI_Home.Instance.CanShowUIBattle())
                L_enemy[i].isCanAI = false;
        }
        for (int i = 0; i < L_boss.Count; i++)
        {
            L_boss[i].m_MoveEnemy.agent.maxSpeed = 3.5f;
            L_boss[i].isCanAI = false;
        }
    }
    private void OnApplicationPause(bool pause)
    {
        DataPlayer.SetCurLevel(CurLevel);
    }
    private void OnApplicationQuit()
    {
        DataPlayer.SetCurLevel(CurLevel);
    }
    private void OnDisable()
    {
        if (UI_Home.Instance.m_TaskManager)
            UI_Home.Instance.m_TaskManager.ResetTaskManager();
    }
    private void OnEnable()
    {
        if (TeleMap)
            TeleMap.SetActive(false);
        StartCoroutine(IE_Delay());
        StartCoroutine(IE_DelayAddList());
    }
    IEnumerator IE_DelayAddList()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < L_enemy.Count; i++)
        {
            L_enemy2.Add(L_enemy[i]);
        }
    }
    public void DoSomethingKillDoneBoss()
    {
        MoveToDoor();
    }
}
