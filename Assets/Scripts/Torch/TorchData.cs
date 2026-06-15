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

        [Header("TreshHold Variables")] public float maxThreshold;
        public float halfThreshold;
        public float minThreshold;
        
        public enum TorchState //Settare a Runtime
        {
            Lit,
            HalfLit,
            QuarterLit,
            TurnedOff
        };
        public TorchState torchState;
        

        public void CopyFrom(TorchData other)
        {
            duration = other.duration;
            maxDuration = other.maxDuration;
            costForUse = other.costForUse;
            maxThreshold = other.maxThreshold;
            halfThreshold = other.halfThreshold;
            minThreshold = other.minThreshold;
            torchState = other.torchState;
        }

    }


}