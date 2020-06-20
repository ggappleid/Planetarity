using System;
using UnityEngine;
using Random = System.Random;

namespace Entitas.Features.Game.Gameplay
{
    public class CalculatePlanetPositionExecuteSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _planets;
        private readonly Random _rand;
        
        public CalculatePlanetPositionExecuteSystem(GameContext game)
        {
            _planets = game.GetGroup(GameMatcher.Planet);
            _rand = new Random();
        }
    
        public void Execute()
        {
            foreach (var planetE in _planets.GetEntities())
            {
                if (planetE.hasPosition)
                {
                    var rotateBy = planetE.planet.RotationSpeed * Time.deltaTime;
                    var planetPosition = planetE.position.Value;
                    var planet2DPosition = new Vector2(planetPosition.x, planetPosition.z);
                    var newPlanet2DPosition = Rotate2DPoint(planet2DPosition, Vector2.zero, rotateBy);
                    planetE.ReplacePosition(new Vector3(newPlanet2DPosition.x, 0f, newPlanet2DPosition.y));
                }
                else
                {
                    var angle = _rand.NextDouble() * Math.PI * 2f;
                    var x = Math.Cos(angle) * planetE.planet.RotationRadius;
                    var y = Math.Sin(angle) * planetE.planet.RotationRadius;
                    planetE.ReplacePosition(new Vector3((float)x, 0f, (float)y));
                }
            }
        }
        
        private Vector2 Rotate2DPoint (Vector2 point, Vector2 center, float angle)
        {
            var rad = angle * (Math.PI/180f); // Convert to radians
            var rotatedX = Math.Cos(rad) * (point.x - center.x) - Math.Sin(rad) * (point.y - center.y) + center.x;
            var rotatedY = Math.Sin(rad) * (point.x - center.x) + Math.Cos(rad) * (point.y - center.y) + center.y;
            return new Vector2((float)rotatedX, (float)rotatedY);
        }
    }
}
