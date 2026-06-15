using System;
using System.Numerics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using TorchDatas;
using Vector3 = UnityEngine.Vector3;

public class Torch : MonoBehaviour
{
    [Header("Core Settings")] [SerializeField]
    private TorchDataSO _dataSO;

    [SerializeField] private TorchData _torchData;
    [SerializeField] private ParticleSystem _torchFireParticles;
    [SerializeField] private Transform _lightObj;

    [Header("Timer Settings")] [SerializeField]
    private float _tick = 3;

    [SerializeField] private float _reductionForTick;
    [SerializeField] private float _reductionMultiplier;
    [SerializeField, Unity.Collections.ReadOnly] private float _time;

    public TorchData TorchData
    {
        get { return _torchData; }
    }

    [Header("GameplayTestingSettings")] [SerializeField]
    private bool _isStartingAtMaxDuration;


    private void Start()
    {
        _torchData.CopyFrom(_dataSO.dataToInject);

        if (_isStartingAtMaxDuration)
        {
            _torchData.duration = _torchData.maxDuration;
        }

        if (_torchFireParticles == null)
        {
            _torchFireParticles = FindAnyObjectByType<TorchParticleIdentifier>().GetComponent<ParticleSystem>();
        }

        if (_lightObj == null)
        {
            _lightObj = FindAnyObjectByType<LightIdentifier>().GetComponent<Transform>();
        }
    }


    private void Update()
    {
        _torchData.duration = Mathf.Clamp(_torchData.duration, 0, _torchData.maxDuration);
        // CheckTorchTreshHold();
        Timer();
    }

    private void Timer()
    {
        _time += Time.deltaTime;

        if (_time >= _tick)
        {
            _torchData.duration -= _reductionForTick;
            //reduce sound
            
            SetLightRadius(_torchData.duration);
            SetTorchParticleEmission(_torchData.duration);
            
            _time = 0;
        }
    }

    private void SetLightRadius(float duration)
    {
        
        if (duration <= 0)
        {
            _lightObj.localScale = Vector3.zero;
            return;
        }
        float _reductionDimension = duration * _reductionMultiplier;
            
        _lightObj.localScale = new Vector3(_reductionDimension,
            _reductionDimension, _reductionDimension);
            
        
        //Bounce o lerp da attuale posizione alla scale a cui deve arrivare

    }

    private void SetTorchParticleEmission(float duration)
    {
         _torchFireParticles.emissionRate = duration;

    }
    
    // private void CheckTorchTreshHold()
    // {
    //     switch (_torchData.duration)
    //     {
    //         case 
    //     }
    // }
    
    /////////////////////////////////// EVENTS
    //Collegare Evento Jasbon
    [Button]
    public void ReduceTorchDurationOnUse()
    {
        _torchData.duration -= _torchData.costForUse;
    }

    [Button]
    public void RegainTorchDuration(float value)
    {
        _torchData.duration += value;
        SetLightRadius(_torchData.duration);
        SetTorchParticleEmission(_torchData.duration);


    }

}