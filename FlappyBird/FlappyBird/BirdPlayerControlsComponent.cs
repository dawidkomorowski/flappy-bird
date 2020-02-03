using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird
{
    public sealed class BirdPlayerControlsComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private InputComponent _inputComponent;
        private const double FlapVelocity = 15;
        private const double Gravity = 1.3;
        private double _verticalVelocity;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _inputComponent = Entity.GetComponent<InputComponent>();
        }

        public override void OnFixedUpdate()
        {
            HandleSpaceKey();

            ApplyGravity();
            ApplyHeightLimit();

            CheckIfStillAlive();
        }

        private void HandleSpaceKey()
        {
            if (_inputComponent.HardwareInput.KeyboardInput.Space)
            {
                _verticalVelocity = FlapVelocity;
            }
        }

        private void ApplyGravity()
        {
            _verticalVelocity -= Gravity;
            _transformComponent.Translation += new Vector3(0, _verticalVelocity, 0);
        }

        private void ApplyHeightLimit()
        {
            const int heightLimit = 500;
            if (_transformComponent.Translation.Y > heightLimit)
            {
                _transformComponent.Translation = _transformComponent.Translation.WithY(heightLimit);
            }
        }

        private void CheckIfStillAlive()
        {
            const int groundLevel = -280;
            if (_transformComponent.Translation.Y < groundLevel)
            {
                GlobalGameState.PlayerIsAlive = false;
                Entity.RemoveComponent(this);
            }
        }
    }
}