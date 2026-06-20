using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public CheckpointManager _checkPointManager;

    private CharacterHealth _character;

    private void Start()
    {
        _checkPointManager = FindFirstObjectByType<CheckpointManager>();

        _character = FindFirstObjectByType<CharacterHealth>();

        _character.OnDeath += RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        _character.transform.position = _checkPointManager.LastCheckPoint.transform.position;

        _character.Respawn();
    }
}