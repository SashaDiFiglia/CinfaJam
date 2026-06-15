using System;
using UnityEngine;
using UnityEngine.Serialization;
using TorchDatas;
using Unity.Collections;

public class Torch : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private TorchDataSO _dataSO; 
    [SerializeField] private TorchData _torchData;
    [SerializeField] private ParticleSystem _torchFireParticles;
    [SerializeField] private Transform _lightObj;
    
    [Header("Timer Settings")]
    [SerializeField] private float _tick = 1;
    [SerializeField] private float _reductionForTick;
    [SerializeField, ReadOnly] private float _time;

    public TorchData TorchData
    {
        get { return _torchData; }
    }

    [Header("GameplayTestingSettings")]
    [SerializeField] private bool _isStartingAtMaxDuration;


    private void Start()
    {
        _torchData.CopyFrom(_dataSO.dataToInject);
        
        if(_isStartingAtMaxDuration)
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
        CheckTorchTreshHold();
        Timer();
        //change aumetare e ridurre dimensione
    }


    //Collegare Evento Jasbon
    public void ReduceTorchDurationOnUse()
    {
        _torchData.duration -= _torchData.costForUse;
    }

    private void Timer()
    {
        _time += Time.deltaTime;

        if (_time >= _tick)
        {
            _torchData.duration -= _reductionForTick;
            //reduce sound
            _lightObj.localScale = new Vector3(_lightObj.localScale.x - _reductionForTick, _lightObj.localScale.y- _reductionForTick, _lightObj.localScale.z- _reductionForTick);
            _time = 0;
        }
    }


    private void CheckTorchTreshHold()
    {
        switch (_torchData.duration)
        {
            case 
        }
    }
    
    
}
