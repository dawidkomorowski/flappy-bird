using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;

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
            scene.AddEntity(_entityFactory.CreateCamera());

            SetUpBackground(scene);
            SetUpGround(scene);
            SetUpBird(scene);
        }

        private void SetUpBackground(Scene scene)
        {
            var backgroundDayLeft = _entityFactory.CreateBackgroundDay();
            backgroundDayLeft.GetComponent<TransformComponent>().Translation = new Vector3(-512, 0, 0);
            scene.AddEntity(backgroundDayLeft);

            var backgroundDayMiddle = _entityFactory.CreateBackgroundDay();
            backgroundDayMiddle.GetComponent<TransformComponent>().Translation = new Vector3(0, 0, 0);
            scene.AddEntity(backgroundDayMiddle);

            var backgroundDayRight = _entityFactory.CreateBackgroundDay();
            backgroundDayRight.GetComponent<TransformComponent>().Translation = new Vector3(512, 0, 0);
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
                ground.GetComponent<TransformComponent>().Translation = new Vector3(initialX, -350, 0);
                scene.AddEntity(ground);
            }
        }

        private void SetUpBird(Scene scene)
        {
            var bird = _entityFactory.CreateBird();
            bird.GetComponent<TransformComponent>().Translation = new Vector3(-100, 0, 0);
            scene.AddEntity(bird);
        }
    }
}