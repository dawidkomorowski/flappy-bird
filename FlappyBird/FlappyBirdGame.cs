using Autofac;
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

        public string WindowTitle => "Flappy Bird (Geisha Engine 0.5)";
    }
}