using UnityEngine;

[CreateAssetMenu(fileName = "FloorHazardData", menuName = "Traps/Data/FloorHazardData")]
public class FloorHazardDataSO : ScriptableObject
{
    [Header("Hazard Properties")]
    public float damage;
    public float delayBetweenHits;
    public bool dealsDamageOverTime;

}
