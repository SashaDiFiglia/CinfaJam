using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Spikes", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorSpikesTrap: AActivableTrap
{
    [Header("Spikes Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenHits;

    private SpikeController _spikesController;

    protected override void OnSetup()
    {
        _spikesController = trapController.GetComponent<SpikeController>();
        _spikesController.Setup(damage, delayBetweenHits);
    }
    protected override void OnClear()
    { if (_spikesController != null) { Destroy(_spikesController);} }

    public override void ActivateTrap() { _spikesController.ToggleSpikes(true); }
    public override void DeactivateTrap() {  _spikesController.ToggleSpikes(false); }
}
