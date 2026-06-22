using UnityEngine;

[CreateAssetMenu(fileName = "FloorHazardData", menuName = "Traps/Data/FloorHazardData")]
public class FloorHazardSO : ScriptableObject
{
    [Header("Hazard Properties")]
    public float damage = 1;
    public float delayBetweenHits = 2;
    public bool dealsDamageOverTime = true;
}
