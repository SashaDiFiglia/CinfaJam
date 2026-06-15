using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Spikes", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorSpikesTrap: AActivableTrap
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private GameObject colliderObj;
    private GameObject _currentCollider;

    protected override void OnSetup()
    { _currentCollider = Instantiate(colliderObj, trapController.transform.position, Quaternion.identity); }
    protected override void OnClear()
    { Destroy(_currentCollider); _currentCollider = null; }

    public override void ActivateTrap()
    {
        trapController.GetComponent<Image>().sprite = activeSprite;
        _currentCollider.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<Image>().sprite = inactiveSprite;
        _currentCollider.SetActive(false);
    }
}
