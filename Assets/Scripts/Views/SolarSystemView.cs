#pragma warning disable 0649

using UnityEngine;

public class SolarSystemView : Singleton<SolarSystemView>
{
    public Camera mainCamera;
    public Canvas uiCanvas;
    
    public GameObject planetPrefab;
    public GameObject rocketPrefab;
    public GameObject planetHudPrefab;

    public Material sun;
    public Material earth;
    public Material mars;
    public Material venus;
    
    public GameObject InstantiatePrefab(GameObject prefab)
    {
        return Instantiate(prefab, transform);
    }

    public GameObject InstantiateUiPrefab(GameObject prefab)
    {
        return Instantiate(prefab, uiCanvas.transform);
    }

    public Material GetPlanetMaterial(string planetName)
    {
        switch (planetName)
        {
            case "Sun":
                return sun;
            case "Earth":
                return earth;
            case "Mars":
                return mars;
            case "Venus":
            default:
                return venus;
        }
    }
}
