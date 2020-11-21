using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class DebugBirdCollisionBoxComponent : BehaviorComponent
    {
        private Transform2DComponent _thisTransformComponent;
        private Transform2DComponent _birdTransformComponent;

        public override void OnStart()
        {
            _thisTransformComponent = Entity.GetComponent<Transform2DComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _thisTransformComponent.Translation = _birdTransformComponent.Translation;
            _thisTransformComponent.Rotation = _birdTransformComponent.Rotation;
            _thisTransformComponent.Scale = _birdTransformComponent.Scale;
        }
    }
}