using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private static BattleController instance;
    public static BattleController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleController();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public List<AllidELement> L_AllidELement;
    public List<EnemyBased> L_EnemyBase;

    public List<EnemyBaseElement> L_EnemyBaseDie;

    public void TakeDamageAllid(int damage, AllidBase allidBase)
    {
        for (int i = 0; i < L_AllidELement.Count; i++)
        {
            if (allidBase.GetComponent<AllidBase>().ID == L_AllidELement[i].ID
                && allidBase.GetComponent<AllidBase>().Type == L_AllidELement[i].Type)
            {
                allidBase.HP -= damage;
                if (allidBase.HP <= 0)
                {
                    allidBase.HP = 0;
                }
                DataPlayer.SetHP(allidBase.HP, L_AllidELement[i].ThisElementData);
                L_AllidELement[i].Init(L_AllidELement[i].ThisElementData);

            }
        }
    }
    public void TakeDamageEnemy(int damage, EnemyBased enemyBase)
    {
        enemyBase.HP -= damage;
    }
}
