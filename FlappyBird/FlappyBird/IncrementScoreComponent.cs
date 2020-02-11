using System.Linq;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class IncrementScoreComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private TransformComponent _birdTransformComponent;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<TransformComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (_transformComponent.Translation.X < _birdTransformComponent.Translation.X)
            {
                GlobalGameState.Score++;
                Entity.RemoveComponent(this);
            }
        }
    }
}