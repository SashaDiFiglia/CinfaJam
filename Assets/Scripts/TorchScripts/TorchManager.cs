using System;
using UnityEngine;
using TorchDatas;
using UnityEngine.Serialization;

public class TorchManager : MonoBehaviour
{
    [FormerlySerializedAs("_torch")] [SerializeField] private TorchInstance _torchInstance;
    [SerializeField] private TorchData _torchData;


    private void Awake()
    {
        _torchInstance = _torchInstance ?? gameObject.GetComponent<TorchInstance>();
        _torchData = _torchInstance.TorchData;
    }
}
