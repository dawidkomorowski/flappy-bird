using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class DebugBirdCollisionBoxComponent : BehaviorComponent
    {
        private TransformComponent _thisTransformComponent;
        private TransformComponent _birdTransformComponent;

        public override void OnStart()
        {
            _thisTransformComponent = Entity.GetComponent<TransformComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<TransformComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _thisTransformComponent.Translation = _birdTransformComponent.Translation;
            _thisTransformComponent.Rotation = _birdTransformComponent.Rotation;
            _thisTransformComponent.Scale = _birdTransformComponent.Scale;
        }
    }
}