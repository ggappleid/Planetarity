using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.GameState
{
    public class CleanupGameDataOnGameEndSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly IGroup<GameEntity> _planets;
        private readonly IGroup<GameEntity> _rockets;
        private readonly IGroup<GameEntity> _players;

        public CleanupGameDataOnGameEndSystem(GameInfoContext gameInfo, GameContext game) : base(gameInfo)
        {
            _planets = game.GetGroup(GameMatcher.Planet);
            _rockets = game.GetGroup(GameMatcher.Rocket);
            _players = game.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.GameEnded);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            CleanOldData();
        }

        private void CleanOldData()
        {
            foreach (var planetE in _planets.GetEntities())
            {
                if (planetE.hasView)
                {
                    Object.Destroy(planetE.view.Value);
                }
                if (planetE.hasUiView)
                {
                    Object.Destroy(planetE.uiView.Value);
                }
                planetE.Destroy();
            }
            
            foreach (var rocketE in _rockets.GetEntities())
            {
                if (rocketE.hasView)
                {
                    Object.Destroy(rocketE.view.Value);
                }
                rocketE.Destroy();
            }
            
            foreach (var playerE in _players.GetEntities())
            {
                playerE.Destroy();
            }
        }
    }
}