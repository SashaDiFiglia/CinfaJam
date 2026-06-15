using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Spikes", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorSpikesTrap: AActivableTrap
{
    [Header("Spikes Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenHits;

    private Spikes _spikesController;

    protected override void OnSetup()
    {
        _spikesController = trapController.GetComponent<Spikes>();
        _spikesController.Setup(damage, delayBetweenHits);
    }
    protected override void OnClear()
    { Destroy(_spikesController.gameObject); _spikesController = null; }

    public override void ActivateTrap() { _spikesController.ToggleSpikes(true); }
    public override void DeactivateTrap() { _spikesController.ToggleSpikes(false); }
}
