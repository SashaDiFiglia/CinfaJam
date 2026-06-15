using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TorchDatas
{
    [Serializable]
    public struct TorchData
    {
        [Header("Torch Timer Variables")] public float duration;
        public float maxDuration;
        public float costForUse;
        
        
        public enum TorchState
        {
            Lit,
            TurnedOff
        };
        public TorchState torchState;
        

        public void CopyFrom(TorchData other)
        {
            duration = other.duration;
            maxDuration = other.maxDuration;
            costForUse = other.costForUse;
            torchState = other.torchState;
        }

    }


}