using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/New Weapon")]
public class Weapon : ScriptableObject
{
    public float Damage;
    public float Range;
    public float MaxDurability;

    public bool Attack()
    {
        return false;
    }
}