using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Spikes", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorHazard: AActivableTrap
{
    [Header("Hazard Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenHits;
    [SerializeField] private bool dealsDamageOverTime;
    private HazardController _hazardController;

    protected override void OnSetup()
    {
        _hazardController = trapController.GetComponent<HazardController>();
        _hazardController.Setup(damage, delayBetweenHits, dealsDamageOverTime);
    }
    protected override void OnClear()
    { if (_hazardController != null) { Destroy(_hazardController);} }

    public override void ActivateTrap() { _hazardController.ToggleHazard(true); }
    public override void DeactivateTrap() {  _hazardController.ToggleHazard(false); }
}
