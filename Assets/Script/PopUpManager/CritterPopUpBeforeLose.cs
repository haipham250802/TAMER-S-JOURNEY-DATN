using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class CritterPopUpBeforeLose : MonoBehaviour
{
    public Slider Hp_Bar;
    public Text TxtCoinHealing;

    public ECharacterType type;
    public SkeletonGraphic skeleton;
    public int HP;
    public int Rarity;
    public int ID;

    private void OnEnable()
    {

    }
    public void SetData(AllidELement allidElement)
    {
        if(allidElement != null)
        {
            type = allidElement.Type;
            HP = allidElement.HP;
            Rarity = allidElement.Rarity;
            ID = allidElement.ID;

            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(allidElement.Type);

            skeleton.skeletonDataAsset = null;
            skeleton.skeletonDataAsset = enemyStat.ICON;
            skeleton.Initialize(true);

            Hp_Bar.maxValue = Controller.Instance.enemyData.GetHPEmemy(type);
            Hp_Bar.value = HP;
        }
    }
}
