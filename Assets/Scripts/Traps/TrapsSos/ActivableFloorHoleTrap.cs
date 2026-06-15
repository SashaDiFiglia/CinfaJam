using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Hole", menuName = "Traps/Activable Floor - Hole")]
public class ActivableFloorHoleTrap: AActivableTrap
{
    [SerializeField] private Sprite holeSprite;
    [SerializeField] private Sprite floorSprite;

    public override void ActivateTrap()
    {
        trapController.GetComponent<Image>().sprite = holeSprite;
        trapController.GetComponent<Collider2D>().enabled = false;
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<Image>().sprite = floorSprite;
        trapController.GetComponent<Collider2D>().enabled = true;
    }
}
