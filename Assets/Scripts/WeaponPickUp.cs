using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    [SerializeField] private EventReference _pickUpSound;

    private EventInstance _pickUpSoundInstance;

    private void Awake()
    {
        _pickUpSoundInstance = RuntimeManager.CreateInstance(_pickUpSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterCombat>(out var combat))
        {
            _pickUpSoundInstance.start();

            combat.Weapon = _weapon;

            gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}