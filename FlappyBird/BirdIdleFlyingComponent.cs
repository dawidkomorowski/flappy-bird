using System;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird
{
    public sealed class BirdIdleFlyingComponent : BehaviorComponent
    {
        private Transform2DComponent _transformComponent;
        private InputComponent _inputComponent;
        private int _updatesCounter = 0;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<Transform2DComponent>();
            _inputComponent = Entity.GetComponent<InputComponent>();
        }

        public override void OnFixedUpdate()
        {
            _updatesCounter++;
            _transformComponent.Translation = _transformComponent.Translation.WithY(Math.Sin(_updatesCounter * 0.1) * 10);

            if (_inputComponent.GetActionState("Flap") && _updatesCounter > 30)
            {
                Entity.RemoveComponent(this);
                Entity.AddComponent(new BirdPhysicsComponent());
                Entity.AddComponent(new BirdPlayerControlsComponent());
            }
        }
    }
}