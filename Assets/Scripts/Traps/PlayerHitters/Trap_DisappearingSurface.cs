using UnityEngine;

public class Trap_DisappearingSurface : MonoBehaviour, ITrap
{
    [SerializeField] private Sprite noWallSprite;
    private Sprite _wallSprite;
    private SpriteRenderer _rend;
    private Collider2D _col;

    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rend = GetComponent<SpriteRenderer>();
        _wallSprite = _rend.sprite;
    }

    public void ActivateTrap()
    {
        _rend.sprite = _wallSprite;
        
        _col.enabled = true;
    }

    public void DeactivateTrap()
    {
        _rend.sprite = noWallSprite;
        _col.enabled = false;
    }
}
