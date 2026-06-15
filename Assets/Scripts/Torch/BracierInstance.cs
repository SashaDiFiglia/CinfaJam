using Sirenix.OdinInspector;
using UnityEngine;

public class BracierInstance : MonoBehaviour
{
    [SerializeField] private BracierDataSO _dataSO;
    [SerializeField] private BracierData _bracierData;
    [SerializeField] private ParticleSystem _fireParticle;
    [SerializeField] private Transform _lightObj;
    
    
    
    
    
    
    
    
    
    
    
    
    
    /////////////////////// EVENTS

    [Button]
    public void LightUp()
    {
        _lightObj.gameObject.SetActive(true);
        _fireParticle.Play();
        
    }
}