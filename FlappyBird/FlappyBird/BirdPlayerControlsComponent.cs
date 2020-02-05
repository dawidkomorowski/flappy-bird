using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace FlappyBird
{
    public sealed class BirdPlayerControlsComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private InputComponent _inputComponent;
        private const double FlapVelocity = 20;
        private const double Gravity = 1.2;
        private double _verticalVelocity;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _inputComponent = Entity.GetComponent<InputComponent>();

            _inputComponent.BindAction("Flap", Flap);

            Flap();
        }

        public override void OnFixedUpdate()
        {
            ApplyGravity();
            ApplyMovement();
            ApplyRotationBasedOnVelocity();
            ApplyHeightLimit();

            CheckIfStillAlive();
        }

        private void Flap()
        {
            _verticalVelocity = FlapVelocity;
        }

        private void ApplyGravity()
        {
            _verticalVelocity -= Gravity;
        }

        private void ApplyMovement()
        {
            _transformComponent.Translation += new Vector3(0, _verticalVelocity, 0);
        }

        private void ApplyRotationBasedOnVelocity()
        {
            double rotation;
            const double upperRangeLimit = -20;
            const double lowerRangeLimit = -35;
            const double maximumAngle = 15;
            const double minimumAngle = -90;

            if (_verticalVelocity > upperRangeLimit)
            {
                rotation = Angle.Deg2Rad(maximumAngle);
            }
            else
            {
                if (_verticalVelocity <= upperRangeLimit && _verticalVelocity > lowerRangeLimit)
                {
                    var normalizedVelocityInRange = -(_verticalVelocity - upperRangeLimit) / (upperRangeLimit - lowerRangeLimit);
                    const double angleDifference = maximumAngle - minimumAngle;
                    var degrees = maximumAngle - angleDifference * normalizedVelocityInRange;
                    rotation = Angle.Deg2Rad(degrees);
                }
                else
                {
                    rotation = Angle.Deg2Rad(minimumAngle);
                }
            }

            _transformComponent.Rotation = new Vector3(0, 0, rotation);
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