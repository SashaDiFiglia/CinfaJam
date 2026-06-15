using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public EventReference _attackSound;
    public EventInstance _attack;

    private void Awake()
    {
        _attack = RuntimeManager.CreateInstance(_attackSound);
    }

    public void Attack()
    {
        _attack.start();
    }
}