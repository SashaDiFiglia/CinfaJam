using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Floor", menuName = "Traps/Activable Floor - Spikes")]
public class ActivableFloorSpikesTrap: AActivableTrap
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private GameObject collider;
    
    
    public override void ActivateTrap()
    {
        trapController.GetComponent<Image>().sprite = activeSprite;
        collider.SetActive(true);
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<Image>().sprite = inactiveSprite;
        collider.SetActive(false);
    }
}
