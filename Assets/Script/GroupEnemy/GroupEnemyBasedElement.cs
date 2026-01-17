using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GroupEnemyBasedElement : MonoBehaviour
{
    public int Level;
    //  public List<Items> List_Items = new List<Items>();
    public List<Items> L_Items = new List<Items>();
    public Transform ParentItems;
    public EnemyBaseElement m_enemyBaseElement;
    public List<EnemyBaseElement> L_enemyBaseElement = new List<EnemyBaseElement>();

    int randQuantityEnemy;
    int minQuantity;
    int maxQuantity;

    public int IndexGive;
    private void OnEnable()
    {
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return null;
      //  
        SetQuantity();
        RenderItems();
    }
    public void SetQuantity()
    {
        switch (BagManager.Instance.m_RuleController.CurLevel)
        {
            case 0:
                randQuantityEnemy = 1;
                break;
            case 1:
                minQuantity = 1;
                maxQuantity = 2;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 2:
                minQuantity = 1;
                maxQuantity = 2;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 3:
                minQuantity = 2;
                maxQuantity = 3;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 4:
                minQuantity = 2;
                maxQuantity = 3;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 5:
                minQuantity = 3;
                maxQuantity = 4;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 6:
                minQuantity = 3;
                maxQuantity = 4;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 7:
                minQuantity = 3;
                maxQuantity = 4;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
            case 8:
                minQuantity = 3;
                maxQuantity = 4;
                randQuantityEnemy = Random.Range(minQuantity, maxQuantity);
                break;
        }
    }
    private void Start()
    {

    }

    [Button("Render Items")]
    private void RenderItems()
    {
       // UI_Home.Instance.uI_Battle.resetTeamenemy();

        if (L_Items.Count > 0)
        {
            L_enemyBaseElement = new List<EnemyBaseElement>();
           
            if (L_Items[0].TypeEnemy == TypeEnemy.Boss)
            {
                if (L_Items[0].enemyBaseElement != null)
                {
                    var Obj = /*Instantiate(L_Items[randElement].enemyBaseElement.gameObject);*/SimplePool.Spawn(L_Items[0].enemyBaseElement.gameObject, Vector3.zero, Quaternion.identity);
                    Obj.transform.SetParent(ParentItems.transform);
                    EnemyBaseElement baseE = Obj.gameObject.GetComponent<EnemyBaseElement>();
                    baseE.Type = L_Items[0].TypeName;
                    baseE.ID = L_Items[0].ID;
                    baseE.TypeEnemy = L_Items[0].TypeEnemy;
                    baseE.transform.localPosition = Vector3.zero;
                    baseE.transform.localScale = Vector3.one;
                    L_enemyBaseElement.Add(baseE);
                }
                return;
            }
            if(DataPlayer.GetIsCheckDoneTutorial())
            {
                L_Items[IndexGive].enemyBaseElement = m_enemyBaseElement;
                if (L_Items[IndexGive].enemyBaseElement != null)
                {
                    var Obj = /*Instantiate(L_Items[randElement].enemyBaseElement.gameObject);*/SimplePool.Spawn(L_Items[IndexGive].enemyBaseElement.gameObject, Vector3.zero, Quaternion.identity);
                    Obj.transform.SetParent(ParentItems.transform);
                    EnemyBaseElement baseE = Obj.gameObject.GetComponent<EnemyBaseElement>();
                    baseE.Type = L_Items[IndexGive].TypeName;
                    baseE.ID = L_Items[IndexGive].ID;
                    for (int j = 0; j < L_enemyBaseElement.Count; j++)
                    {
                        if (baseE.ID == L_enemyBaseElement[j].ID)
                        {
                            baseE.ID = L_enemyBaseElement[j].ID + 1;
                        }
                    }
                    baseE.TypeEnemy = L_Items[IndexGive].TypeEnemy;
                    baseE.transform.localPosition = Vector3.zero;
                    baseE.transform.localScale = Vector3.one;
                    L_enemyBaseElement.Add(baseE);
                }
            }
           

            for (int i = 0; i < randQuantityEnemy; i++)
            {
                int randElement = Random.Range(0, L_Items.Count);
                L_Items[randElement].enemyBaseElement = m_enemyBaseElement;
                if (L_Items[randElement].enemyBaseElement != null)
                {
                    var Obj = /*Instantiate(L_Items[randElement].enemyBaseElement.gameObject);*/SimplePool.Spawn(L_Items[randElement].enemyBaseElement.gameObject, Vector3.zero, Quaternion.identity);
                    Obj.transform.SetParent(ParentItems.transform);
                    EnemyBaseElement baseE = Obj.gameObject.GetComponent<EnemyBaseElement>();
                    baseE.Type = L_Items[randElement].TypeName;
                    baseE.ID = L_Items[randElement].ID;
                    for (int j = 0; j < L_enemyBaseElement.Count; j++)
                    {
                        if (baseE.ID == L_enemyBaseElement[j].ID)
                        {
                            baseE.ID = L_enemyBaseElement[j].ID + 1;
                        }
                    }
                    baseE.TypeEnemy = L_Items[randElement].TypeEnemy;
                    baseE.transform.localPosition = Vector3.zero;
                    baseE.transform.localScale = Vector3.one;
                    L_enemyBaseElement.Add(baseE);
                }
            }
        }
    }

    private void OnDisable()
    {
     //   UI_Home.Instance.uI_Battle.ResetEnemyBasedELement();
        for (int i = 0; i < L_enemyBaseElement.Count; i++)
        {
            if (L_enemyBaseElement[i])
            SimplePool.Despawn(L_enemyBaseElement[i].gameObject);
        }
        L_enemyBaseElement.Clear();
    }
}
[System.Serializable]
public class Items
{
    [FoldoutGroup("$TypeName")]
    public TypeEnemy TypeEnemy;

    [FoldoutGroup("$TypeName")]
    public ECharacterType TypeName;

    [FoldoutGroup("$TypeName")]
    public EnemyBaseElement enemyBaseElement;

    public int ID;
}
public enum TypeEnemy
{
    NONE = -1,
    Soldier,
    Boss,
}