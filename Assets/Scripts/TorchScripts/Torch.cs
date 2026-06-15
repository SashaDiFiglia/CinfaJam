using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using TorchDatas;
using Vector3 = UnityEngine.Vector3;

public class TorchInstance : MonoBehaviour
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
    [SerializeField] private float _time;

    // [Header("Events,Action ecc")] private Action _onTorchCheck;//nome temporaneo
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

        // _onTorchCheck += SetTorchState;
    }


    private void Update()
    {
        _torchData.duration = Mathf.Clamp(_torchData.duration, 0, _torchData.maxDuration);
        // CheckTorchTreshHold();
        Timer();
        SetTorchState();
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
        float reductionDimension = duration <= 0
            ? 0f
            : duration * _reductionMultiplier;

        Vector3 targetScale = Vector3.one * reductionDimension;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(LerpScale(targetScale, 0.2f));
    }

    private void SetTorchParticleEmission(float duration)
    {
         _torchFireParticles.emissionRate = duration;

    }

    private void SetTorchState()
    {
        _torchData.torchState = _torchData.duration > 0 ? TorchData.TorchState.Lit : TorchData.TorchState.TurnedOff;
    }
    
    //////////////////////////////// COROUTINES
    
    
    private Coroutine scaleCoroutine;


    private IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        Vector3 startScale = _lightObj.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            _lightObj.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        _lightObj.localScale = targetScale;
    }
    
    /////////////////////////////////// EVENTS
    //
    //Collegare Evento Jasbon dai collega dai collega dai collega dai collega dai collega
    [Button]
    public void ReduceTorchDurationOnUse()
    {
        _torchData.duration -= _torchData.costForUse;
        SetLightRadius(_torchData.duration);
        SetTorchParticleEmission(_torchData.duration);
        _time = 0;
    }

    [Button]
    public void RegainTorchDuration(float value)
    {
        _torchData.duration += value;
        SetLightRadius(_torchData.duration);
        SetTorchParticleEmission(_torchData.duration);

        _time = 0;

    }
    
    //Getters
    public TorchData.TorchState GetTorchState()
    {
        return _torchData.torchState;
    }

}