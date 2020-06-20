using System.Collections.Generic;
using System.Linq;

namespace Entitas.Features.GameState
{
    public class EndGameSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly GameInfoContext _gameInfo;

        private readonly IGroup<GameEntity> _players;
     
        public EndGameSystem(GameContext game, GameInfoContext gameInfo) : base(game)
        {
            _game = game;
            _gameInfo = gameInfo;
            _players = game.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Player);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var playerE = _players
                .GetEntities()
                .First(i => i.player.Type == PlayerType.Player);

            var botEs = _players
                .GetEntities()
                .Where(i => i.player.Type == PlayerType.Bot);

            var isPlayerDead = !playerE.player.IsAlive;
            var areBotsDead = botEs.All(i => !i.player.IsAlive);
            
            if (isPlayerDead)
            {
                _gameInfo.ReplaceGameEnded(false);
            } 
            else if (areBotsDead)
            {
                _gameInfo.ReplaceGameEnded(true);
            }
        }
    }
}
