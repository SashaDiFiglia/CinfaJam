using System;

[Serializable]
public struct BracierData
{
    public bool hasBeenLit;
    public float lightRadius;
    public float lightIntensity;
    public float timeToLightUp;
    
    
    
    public void CopyFrom(BracierData other)
    {
        hasBeenLit = other.hasBeenLit;
        lightRadius = other.lightRadius;
        lightIntensity = other.lightIntensity;
        timeToLightUp = other.timeToLightUp;
    }
    
}