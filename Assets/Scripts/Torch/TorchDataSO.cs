using UnityEngine;
using UnityEngine.Serialization;

namespace TorchDatas
{
    [CreateAssetMenu(menuName = "Torch/TorchDataSO")]
    public class TorchDataSO : ScriptableObject
    {
        [SerializeField] public TorchData dataToInject;
    }
}