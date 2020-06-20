using System;
using Entitas;
using UnityEngine;

[Game]
public class CollisionComponent : IComponent
{
    public GameObject originalObj;
    public GameObject hitObj;
}

[Game]
public class ViewComponent : IComponent
{
    public GameObject Value;
}

[Game]
public class UiViewComponent : IComponent
{
    public GameObject Value;
}

[Game][Serializable]
public class PlanetComponent : IComponent
{
    public string Name;
    public float RotationRadius;
    public float RotationSpeed;
}

[Game][Serializable]
public class CannonComponent : IComponent
{
    public int FirePower;
    public float ThrowPower;
    public float CooldownTime;
}

[Game] [Serializable]
public class CooldownTimerComponent : IComponent
{
    public float Value;
}

[Game][Serializable]
public class MassComponent : IComponent
{
    public float Value;
}

[Game][Serializable]
public class PositionComponent : IComponent
{
    public Vector3 Value;
}

[Game][Serializable]
public class FirePowerComponent : IComponent
{
    public int Value;
}

[Game][Serializable]
public class HealthComponent : IComponent
{
    public float Value;
}

[Game][Serializable]
public class VelocityComponent : IComponent
{
    public Vector3 Value;
}

[Game]
public class RocketComponent : IComponent
{}

[Game]
public class ShootComponent : IComponent
{
    public string PlanetName;
    public Vector3 Target;
    public int FirePower;
    public float ThrowPower;
}

public enum PlayerType
{
    Player,
    Bot
}

[Game][Serializable]
public class PlayerComponent : IComponent
{
    public string Planet;
    public PlayerType Type;
    public float BotStrength;
    public bool IsAlive;
}