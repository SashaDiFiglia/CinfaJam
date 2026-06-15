using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Activable Wall", menuName = "Traps/Activable Wall")]
public class DisappearingSurface: AActivableTrap
{
    [SerializeField] private Sprite noWallSprite;
    private Sprite _wallSprite;

    protected override void OnSetup()
    {
        Debug.Log("Setup");
        _wallSprite = trapController.GetComponent<SpriteRenderer>().sprite;
        
    }
    
    public override void ActivateTrap()
    {
        trapController.GetComponent<SpriteRenderer>().sprite = _wallSprite;
        trapController.GetComponent<Collider2D>().enabled = true;
    }

    public override void DeactivateTrap()
    {
        trapController.GetComponent<SpriteRenderer>().sprite = noWallSprite;
        trapController.GetComponent<Collider2D>().enabled = false;
    }
}
