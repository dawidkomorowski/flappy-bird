﻿using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Physics.Components;

namespace FlappyBird
{
    public sealed class BirdPhysicsComponent : BehaviorComponent
    {
        private TransformComponent _transformComponent;
        private RectangleColliderComponent _rectangleColliderComponent;
        private const double FlapVelocity = 13;
        private double _gravity = 0.8;
        private double _verticalVelocity;

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _rectangleColliderComponent = Entity.GetComponent<RectangleColliderComponent>();

            GlobalGameState.CurrentPhase = GlobalGameState.Phase.Playing;
        }

        public override void OnFixedUpdate()
        {
            ApplyGravity();
            ApplyMovement();
            ApplyRotationBasedOnVelocity();
            ApplyHeightLimit();

            CheckIfStillAlive();
        }

        public void Flap()
        {
            _verticalVelocity = FlapVelocity;
        }

        private void ApplyGravity()
        {
            _verticalVelocity -= _gravity;
        }

        private void ApplyMovement()
        {
            _transformComponent.Translation += new Vector3(0, _verticalVelocity, 0);
        }

        private void ApplyRotationBasedOnVelocity()
        {
            double rotation;
            const double upperRangeLimit = -15;
            const double lowerRangeLimit = -25;
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
            var hitTheGround = _transformComponent.Translation.Y < groundLevel;

            if (hitTheGround)
            {
                _transformComponent.Translation = _transformComponent.Translation.WithY(groundLevel);
                _gravity = 0;
            }

            var hitThePipe = _rectangleColliderComponent.IsColliding;

            if (hitThePipe)
            {
                if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing)
                {
                    _verticalVelocity = 0;
                    _gravity *= 1.5;
                }
            }

            if (hitTheGround || hitThePipe)
            {
                GlobalGameState.CurrentPhase = GlobalGameState.Phase.GameOver;
            }
        }
    }
}