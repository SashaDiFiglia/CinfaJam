using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TestFMOD : MonoBehaviour
{
    public EventReference gnorf;
    public EventInstance gnirt;

    private void Awake()
    {
        gnirt = RuntimeManager.CreateInstance(gnorf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gnirt.start();
        }
    }
}