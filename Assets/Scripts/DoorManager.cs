using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<BracierInstance> braciers;
    [SerializeField] private List<Enemy> enemies; 
    [SerializeField] private List<Door> doors;

    private bool completed;



    private void Awake()
    {
        
    }


    private void Update()
    {
        if (completed)
            return;

        if (braciers.All(b => b.GetInstanceData().hasBeenLit))
        {
            completed = true;
            
            foreach (var d in doors)
            {
                d.Open();
            }
        }
    }
    
}