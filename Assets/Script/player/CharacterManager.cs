using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CharacterManager();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    [FoldoutGroup("List Character")]
    public List<BossPatrol> List_Boss = new List<BossPatrol>();
    [FoldoutGroup("List Character")]
    public List<Enemy> List_Enemy = new List<Enemy>();

    public Action A_GameWin;

    private void Start()
    {
        A_GameWin += GameWin;
    }
    public void GameWin()
    {
        Debug.Log("Thang Roi !!!");
    }
    public void AddEnemyToListEnemy(Enemy enemy)
    {
        List_Enemy.Add(enemy);
    }
    public void AddBossToListBoss(BossPatrol boss)
    {
        List_Boss.Add(boss);
    }
    private void OnDisable()
    {
        Destroy(gameObject);
        A_GameWin -= GameWin;
    }
}
