using System;
using Autofac;
using Geisha.Common.Extensibility;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace FlappyBird
{
    public sealed class Extension : IExtension
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SetUpLevel>().As<ISceneConstructionScript>().SingleInstance();
            containerBuilder.RegisterType<EntityFactory>().As<IEntityFactory>().SingleInstance();
            containerBuilder.RegisterType<GameOverSystem>().As<IFixedTimeStepSystem>().SingleInstance();
        }

        public string Name { get; } = "Flappy Bird";
        public string Description { get; } = "";
        public string Category { get; } = "Game";
        public string Author { get; } = "Dawid Komorowski";
        public Version Version { get; } = new Version(0, 1);
    }
}