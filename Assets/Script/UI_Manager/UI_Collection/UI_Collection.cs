using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Collection : MonoBehaviour
{
    private bool isUITeam;
    private bool isUIMerge;

    public BagManager m_Bag;

    public bool IsUIteam { get => isUITeam; set => isUITeam = value; }
    public bool IsUIMerge { get => isUIMerge; set => isUIMerge = value; }

    private void Start()
    {

    }
    public void ActiveUIMerge()
    {
        isUIMerge = true;
        isUITeam = false;
        PauseGame();
    }
    public void ActiveUIteam()
    {
        isUITeam = true;
        isUIMerge = false;
        PauseGame();
    }
    public void PauseGame()
    {
        for (int i = 0; i < m_Bag.m_RuleController.L_enemy.Count; i++)
        {
            m_Bag.m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 0;
        }
    }
    public void UnPauseGame()
    {
       // Time.timeScale = 1;
        for (int i = 0; i < m_Bag.m_RuleController.L_enemy.Count; i++)
        {
            m_Bag.m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
        }
    }
}
