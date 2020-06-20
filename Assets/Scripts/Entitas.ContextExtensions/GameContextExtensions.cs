using System.Linq;

public partial class GameContext
{
    public GameEntity GetPlanetEntity(GameEntity playerEntity)
    {
        return this
            .GetGroup(GameMatcher.Planet)
            .GetEntities()
            .FirstOrDefault(i => string.Equals(i.planet.Name, playerEntity.player.Planet));
    }

    public GameEntity GetPlayerEntityByPlanet(GameEntity planetEntity)
    {
        return this
            .GetGroup(GameMatcher.Player)
            .GetEntities()
            .FirstOrDefault(i => i.player.Planet == planetEntity.planet.Name);
    } 
    
    public GameEntity GetPlayerEntity()
    {
        return this
            .GetGroup(GameMatcher.Player)
            .GetEntities()
            .FirstOrDefault(i => i.player.Type == PlayerType.Player);
    }
}
