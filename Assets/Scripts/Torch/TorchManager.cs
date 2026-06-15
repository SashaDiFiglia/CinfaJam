using System;
using UnityEngine;
using TorchDatas;
public class TorchManager : MonoBehaviour
{
    [SerializeField] private Torch _torch;
    [SerializeField] private TorchData _torchData;


    private void Awake()
    {
        _torch = _torch ?? gameObject.GetComponent<Torch>();
        _torchData = _torch.TorchData;
    }
}
