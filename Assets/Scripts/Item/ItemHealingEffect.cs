using UnityEngine;
[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEffect : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp Add" + healingPoint);
        return true;
    }
    
}
