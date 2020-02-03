using System;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class BirdMovementComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private int _updatesCounter = 0;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
        }

        public override void OnFixedUpdate()
        {
            _updatesCounter++;
            _transformComponent.Translation = _transformComponent.Translation.WithY(Math.Sin(_updatesCounter * 0.1) * 10);
        }
    }
}