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
    }
}