using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird
{
    public sealed class BirdPlayerControlsComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private InputComponent _inputComponent;
        private const double MaxJumpVelocity = 25;
        private const double JumpVelocityDamping = 2;
        private double _jumpVelocity = 0;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _inputComponent = Entity.GetComponent<InputComponent>();
        }

        public override void OnFixedUpdate()
        {
            HandleSpaceKey();

            ApplyGravity();
            ApplyJump();
            ApplyHeightLimit();

            CheckIfStillAlive();
        }

        private void HandleSpaceKey()
        {
            if (_inputComponent.HardwareInput.KeyboardInput.Space)
            {
                _jumpVelocity = MaxJumpVelocity;
            }
        }

        private void ApplyGravity()
        {
            const int gravity = 10;
            _transformComponent.Translation += new Vector3(0, -gravity, 0);
        }

        private void ApplyJump()
        {
            if (_jumpVelocity > 0) _jumpVelocity -= JumpVelocityDamping;
            _transformComponent.Translation += new Vector3(0, _jumpVelocity, 0);
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