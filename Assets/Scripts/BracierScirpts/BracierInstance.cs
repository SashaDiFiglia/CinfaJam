using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BracierInstance : MonoBehaviour
{
    [SerializeField] private BracierDataSO _dataSO;
    [SerializeField] private BracierData _bracierData;
    [SerializeField] private ParticleSystem _bracierFireParticle;
    [SerializeField] private Transform _lightObj;


    private void Awake()
    {
        _bracierData.CopyFrom(_dataSO.dataToInject);
        
        
        
        
        
        
        if(!_bracierData.hasBeenLit){return;}
        LightUp();

    }

    private void SetLightRadius()
    {

        Vector3 targetScale = new Vector3(_bracierData.lightRadius, _bracierData.lightRadius, _bracierData.lightRadius);
        
        _lightObj.localScale = Vector3.zero;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(LerpScale(targetScale, _bracierData.timeToLightUp));
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
    
    
    
    /////////////////////// EVENTS

    
    //Evento da Chiamare per accendere
    [Button]
    public void LightUp()
    {
        _lightObj.gameObject.SetActive(true);
        SetLightRadius();
        _bracierFireParticle.Play();
        _bracierData.hasBeenLit = true;
        
    }

    public BracierData GetInstanceData()
    {
        return _bracierData;
    }
}