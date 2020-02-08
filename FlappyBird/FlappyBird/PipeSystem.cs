using System;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public sealed class PipeSystem : IFixedTimeStepSystem
    {
        private const int PipeTimeInterval = 90;
        private const double PipeInitialXPos = 1000;
        private const double MaximumPipeYPos = -250;
        private const double MinimumPipeYPos = -500;
        private const double GapBetweenPipes = 200;
        private readonly IEntityFactory _entityFactory;
        private readonly Random _random = new Random();
        private int _updatesCounter;

        public PipeSystem(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void FixedUpdate(Scene scene)
        {
            switch (GlobalGameState.CurrentPhase)
            {
                case GlobalGameState.Phase.WaitingForPlayer:
                    WaitForPlayer();
                    break;
                case GlobalGameState.Phase.Playing:
                    Play(scene);
                    break;
                case GlobalGameState.Phase.GameOver:
                    GameOver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string Name { get; } = typeof(PipeSystem).FullName;

        private void WaitForPlayer()
        {
        }

        private void Play(Scene scene)
        {
            _updatesCounter++;

            if (_updatesCounter > PipeTimeInterval)
            {
                _updatesCounter = 0;

                var pipeYPos = MinimumPipeYPos + _random.NextDouble() * (MaximumPipeYPos - MinimumPipeYPos);

                var pipeUp = _entityFactory.CreatePipe();
                pipeUp.GetComponent<TransformComponent>().Translation = new Vector3(PipeInitialXPos, pipeYPos, 0);
                scene.AddEntity(pipeUp);

                var pipeDown = _entityFactory.CreatePipe();
                var pipeDownTransform = pipeDown.GetComponent<TransformComponent>();
                var pipeHeight = pipeDown.GetComponent<SpriteRendererComponent>().Sprite.Rectangle.Height * pipeDownTransform.Scale.Y;
                pipeDownTransform.Translation = new Vector3(PipeInitialXPos, pipeYPos + pipeHeight + GapBetweenPipes, 0);
                pipeDownTransform.Scale = pipeDownTransform.Scale.WithY(-pipeDownTransform.Scale.Y);
                scene.AddEntity(pipeDown);
            }
        }

        private void GameOver()
        {
        }
    }
}