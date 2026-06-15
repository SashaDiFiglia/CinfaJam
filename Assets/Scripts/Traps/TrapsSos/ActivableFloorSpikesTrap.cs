using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Spikes", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorSpikesTrap: AActivableTrap
{
    [Header("Trap Properties")]
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private GameObject colliderPrefab;
    
    [Header("Spikes Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenHits;

    private GameObject _currentCol;

    protected override void OnSetup()
    {
        _currentCol = Instantiate(colliderPrefab, trapController.transform.position, Quaternion.identity);
        _currentCol.GetComponent<Spikes>().Setup(damage, delayBetweenHits);
    }
    protected override void OnClear()
    { Destroy(_currentCol); _currentCol = null; }

    public override void ActivateTrap()
    {
        trapController.GetComponent<Image>().sprite = activeSprite;
        _currentCol.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<Image>().sprite = inactiveSprite;
        _currentCol.SetActive(false);
    }
}
