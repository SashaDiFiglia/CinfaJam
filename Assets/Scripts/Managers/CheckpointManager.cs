using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<Checkpoint> _checkPoints = new List<Checkpoint>();

    public Checkpoint LastCheckPoint;

    private void Awake()
    {
        var points = FindObjectsByType<Checkpoint>(default);

        _checkPoints.AddRange(points);
    }

    private void Start()
    {
        foreach (var checkPoint in _checkPoints)
        {
            checkPoint.OnPlayerEntered += SetLastCheckPoint;

            if (checkPoint.IsFirst)
            {
                LastCheckPoint = checkPoint;
            }
        }
    }

    private void SetLastCheckPoint(Checkpoint checkPoint)
    {
        LastCheckPoint = checkPoint;

        foreach (var point in _checkPoints)
        {
            if (point == LastCheckPoint)
            {
                point.SetActive(true);

                continue;
            }

            point.SetActive(false);
        }
    }
}