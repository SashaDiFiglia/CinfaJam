using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Bracier/BracierSO", fileName = "BracierDataSO")]
public class BracierDataSO : ScriptableObject
{
     [SerializeField] public BracierData dataToInject;
}