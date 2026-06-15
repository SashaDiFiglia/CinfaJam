using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Wall", menuName = "Traps/Activable Wall")]
public class ActivableWall: AActivableTrap
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    
    public override void ActivateTrap()
    {
        trapController.GetComponent<Image>().sprite = activeSprite;
        trapController.GetComponent<Collider2D>().enabled = true;
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<Image>().sprite = inactiveSprite;
        trapController.GetComponent<Collider2D>().enabled = false;

    }
}
