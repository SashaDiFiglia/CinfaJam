using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int amount = 1;

    [SerializeField] private EventReference _pickUpSound;

    private EventInstance _pickUpSoundInstance;

    private void Awake()
    {
        _pickUpSoundInstance = RuntimeManager.CreateInstance(_pickUpSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterHealth>(out var health))
        {
            _pickUpSoundInstance.start();

            health.Heal(amount);

            gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}