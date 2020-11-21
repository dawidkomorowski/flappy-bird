using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public sealed class SetUpLevel : ISceneConstructionScript
    {
        private readonly IEntityFactory _entityFactory;

        public SetUpLevel(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public string Name { get; } = "SetUpLevel";

        public void Execute(Scene scene)
        {
            GlobalGameState.CurrentPhase = GlobalGameState.Phase.WaitingForPlayer;
            GlobalGameState.Score = 0;

            SetUpCamera(scene);
            SetUpBackground(scene);
            SetUpGround(scene);
            SetUpBird(scene);
            SetUpScore(scene);

            if (GlobalGameState.IsRetry == false)
            {
                SetUpIntro(scene);
            }

            SetUpGameOver(scene);
        }

        private void SetUpCamera(Scene scene)
        {
            var camera = _entityFactory.CreateCamera();
            scene.AddEntity(camera);
        }

        private void SetUpBackground(Scene scene)
        {
            var backgroundDayLeft = _entityFactory.CreateBackgroundDay();
            backgroundDayLeft.GetComponent<Transform2DComponent>().Translation = new Vector2(-512, 0);
            scene.AddEntity(backgroundDayLeft);

            var backgroundDayMiddle = _entityFactory.CreateBackgroundDay();
            backgroundDayMiddle.GetComponent<Transform2DComponent>().Translation = new Vector2(0, 0);
            scene.AddEntity(backgroundDayMiddle);

            var backgroundDayRight = _entityFactory.CreateBackgroundDay();
            backgroundDayRight.GetComponent<Transform2DComponent>().Translation = new Vector2(512, 0);
            scene.AddEntity(backgroundDayRight);
        }

        private void SetUpGround(Scene scene)
        {
            for (var i = 0; i < 10; i++)
            {
                const double groundWidth = 336;
                const double offsetToTheLeft = 2 * groundWidth;
                var initialX = i * groundWidth - offsetToTheLeft;

                var ground = _entityFactory.CreateGround();
                ground.GetComponent<Transform2DComponent>().Translation = new Vector2(initialX, -350);
                scene.AddEntity(ground);
            }
        }

        private void SetUpBird(Scene scene)
        {
            var bird = _entityFactory.CreateBird();
            bird.GetComponent<Transform2DComponent>().Translation = new Vector2(-100, 0);
            scene.AddEntity(bird);
        }

        private void SetUpScore(Scene scene)
        {
            var score = _entityFactory.CreateScore();
            score.GetComponent<Transform2DComponent>().Translation = new Vector2(0, 275);
            scene.AddEntity(score);
        }

        private void SetUpIntro(Scene scene)
        {
            var intro = _entityFactory.CreateIntro();
            scene.AddEntity(intro);
        }

        private void SetUpGameOver(Scene scene)
        {
            var gameOver = _entityFactory.CreateGameOver();
            gameOver.GetComponent<SpriteRendererComponent>().Visible = false;
            scene.AddEntity(gameOver);
        }
    }
}