using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entitas.Features.GameState.SaveLoadGameFeature
{
    public class SaveGameSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly IDataSaver _dataSaver;
        
        private readonly IGroup<GameEntity> _planets;
        private readonly IGroup<GameEntity> _rockets;
        private readonly IGroup<GameEntity> _players;
        
        public SaveGameSystem(GameInfoContext gameInfo, GameContext game, IDataSaver dataSaver) : base(gameInfo)
        {
            _dataSaver = dataSaver;

            _planets = game.GetGroup(GameMatcher.Planet);
            _rockets = game.GetGroup(GameMatcher.Rocket);
            _players = game.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.SaveGame);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            var gameBoard = new GameBoardState
            {
                Planets = _planets
                    .GetEntities()
                    .Select(e => new PlanetEntity(e))
                    .ToArray(),
                Rockets = _rockets
                    .GetEntities()
                    .Select(e => new RocketEntity(e))
                    .ToArray(),
                Players = _players
                    .GetEntities()
                    .Select(e => new PlayerEntity(e))
                    .ToArray()
            };
            
            _dataSaver.SaveData(gameBoard);
        }
    }
}