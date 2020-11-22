using System.Reflection;
using Autofac;
using FlappyBird.Systems;
using Geisha.Engine;

namespace FlappyBird
{
    public sealed class FlappyBirdGame : IGame
    {
        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            componentsRegistry.RegisterSceneConstructionScript<SetUpLevel>();
            componentsRegistry.RegisterSystem<PipeSystem>();
            componentsRegistry.RegisterSystem<GameOverSystem>();

            componentsRegistry.AutofacContainerBuilder.RegisterType<EntityFactory>().As<IEntityFactory>();
        }

        public string WindowTitle => $"FlappyBird (Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version})";
    }
}