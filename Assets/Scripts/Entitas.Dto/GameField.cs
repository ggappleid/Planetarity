using System;
using UnityEngine;

[Serializable]
public class PlanetEntity
{
    [SerializeField] public PlanetComponent Planet;
    [SerializeField] public PositionComponent Position;
    [SerializeField] public MassComponent Mass;
    [SerializeField] public HealthComponent Health;
    [SerializeField] public CannonComponent Cannon;
    [SerializeField] public CooldownTimerComponent CooldownTimer;
    
    public PlanetEntity(GameEntity planetE)
    {
        Planet = planetE.planet;
        Position = planetE.position;
        Mass = planetE.mass;
        Health = planetE.health;

        if (planetE.hasCannon)
        {
            Cannon = planetE.cannon;
        }
        if (planetE.hasCooldownTimer)
        {
            CooldownTimer = planetE.cooldownTimer;
        }
    }
}

[Serializable]
public class RocketEntity
{
    [SerializeField] public FirePowerComponent FirePower;
    [SerializeField] public HealthComponent Health;
    [SerializeField] public PositionComponent Position;
    [SerializeField] public VelocityComponent Velocity;

    public RocketEntity(GameEntity rocketE)
    {
        FirePower = rocketE.firePower;
        Health = rocketE.health;
        Position = rocketE.position;
        Velocity = rocketE.velocity;
    }
}

[Serializable]
public class PlayerEntity
{
    [SerializeField] public PlayerComponent Player;

    public PlayerEntity(GameEntity playerE)
    {
        Player = playerE.player;
    }
}

[Serializable]
public class GameBoardState
{
    [SerializeField] public PlanetEntity[] Planets;
    [SerializeField] public RocketEntity[] Rockets;
    [SerializeField] public PlayerEntity[] Players;
}
