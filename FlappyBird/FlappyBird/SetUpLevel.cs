using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public sealed class SetUpLevel : ISceneConstructionScript
    {
        public string Name { get; } = "SetUpLevel";

        public void Execute(Scene scene)
        {
            var camera = new Entity();
            camera.AddComponent(TransformComponent.Default);
            camera.AddComponent(new CameraComponent());
            scene.AddEntity(camera);

            var entity = new Entity();
            entity.AddComponent(TransformComponent.Default);
            entity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 255, 0, 0),
                Dimension = new Vector2(400, 300),
                SortingLayerName = "Default",
                Visible = true,
                FillInterior = true
            });
            scene.AddEntity(entity);
        }
    }
}