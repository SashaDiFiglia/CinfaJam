using System;

[Serializable]
public struct BracierData
{
    public bool hasBeenLit;
    public float lightRadius;
    public float lightIntensity;
    
    
    
    public void CopyFrom(BracierData other)
    {
        hasBeenLit = other.hasBeenLit;
        lightRadius = other.lightRadius;
        lightIntensity = other.lightIntensity;
    }
    
}