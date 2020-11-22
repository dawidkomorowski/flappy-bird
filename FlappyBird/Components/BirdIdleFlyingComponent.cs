using System;
using System.Diagnostics;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird.Components
{
    public sealed class BirdIdleFlyingComponent : BehaviorComponent
    {
        private Transform2DComponent _transformComponent = null!;
        private InputComponent _inputComponent = null!;
        private int _updatesCounter = 0;

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _transformComponent = Entity.GetComponent<Transform2DComponent>();
            _inputComponent = Entity.GetComponent<InputComponent>();
        }

        public override void OnFixedUpdate()
        {
            _updatesCounter++;
            _transformComponent.Translation = _transformComponent.Translation.WithY(Math.Sin(_updatesCounter * 0.1) * 10);

            if (_inputComponent.GetActionState("Flap") && _updatesCounter > 30)
            {
                Debug.Assert(Entity != null, nameof(Entity) + " != null");

                Entity.RemoveComponent(this);
                Entity.AddComponent(new BirdPhysicsComponent());
                Entity.AddComponent(new BirdPlayerControlsComponent());
            }
        }
    }
}