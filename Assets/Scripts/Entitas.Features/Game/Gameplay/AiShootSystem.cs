using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class AiShootSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private float _aiTimer = 0.01f;
        private float _aiCooldown = 0.3f;

        private readonly IGroup<GameEntity> _players;
        
        public AiShootSystem(GameContext game)
        {
            _game = game;
            _players = game.GetGroup(GameMatcher.Player);
        }
        
        public void Execute()
        {
            _aiTimer -= Time.deltaTime;
            
            if (_aiTimer >= 0)
            {
                return;
            }
            
            _aiTimer = _aiCooldown;
            
            var playerE = _game.GetPlayerEntity();

            if (playerE == null ||
                !playerE.player.IsAlive)
            {
                return;
            }
            
            var botEntities = _players
                .GetEntities()
                .Where(i => i.player.IsAlive)
                .Where(i => i != playerE);

            var targets = _players
                .GetEntities()
                .Where(i => i.player.IsAlive);
            
            foreach (var botE in botEntities)
            {
                var botPlanetE = _game.GetPlanetEntity(botE);
                var desiredTarget = GetTarget(botE, targets);

                _game
                    .CreateEntity()
                    .ReplaceShoot(botE.player.Planet, desiredTarget, botPlanetE.cannon.FirePower, botPlanetE.cannon.ThrowPower);
            }
        }

        private Vector3 GetTarget(GameEntity botE, IEnumerable<GameEntity> targets)
        {
            var actualTargets = targets.Where(i => i != botE).ToList();
            actualTargets.Shuffle();
            var targetPlanet = _game.GetPlanetEntity(actualTargets.First());
            var aimBias = Random.insideUnitCircle * (3f * (1f - botE.player.BotStrength));
            var aimBiasV3 = new Vector3(aimBias.x, 0f, aimBias.y);
            return targetPlanet.position.Value + aimBiasV3;
        }
    }
}
