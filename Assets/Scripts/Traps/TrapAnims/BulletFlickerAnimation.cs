using UnityEngine;

public class BulletFlickerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] _animSprites;
    [SerializeField] private float _timeBetweenSprites = 0.1f;
    [SerializeField] private float _spriteScale;
    
    private int _currentSprite;
    private SpriteRenderer _spriteRenderer;
    
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentSprite = 0;
        InvokeRepeating(nameof(ChangeSprite), 0, _timeBetweenSprites);
    }
    private void ChangeSprite()
    {
        _spriteRenderer.sprite = _animSprites[_currentSprite];
        _currentSprite = _currentSprite ++;
    }
    
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
