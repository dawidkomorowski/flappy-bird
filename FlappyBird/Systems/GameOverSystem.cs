using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird.Systems
{
    public sealed class GameOverSystem : ICustomSystem
    {
        private readonly ISceneManager _sceneManager;
        private readonly IEntityFactory _entityFactory;
        private int _updatesCounter;

        public GameOverSystem(ISceneManager sceneManager, IEntityFactory entityFactory)
        {
            _sceneManager = sceneManager;
            _entityFactory = entityFactory;
        }

        public void ProcessFixedUpdate(Scene scene)
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.GameOver)
            {
                if (_updatesCounter == 0)
                {
                    scene.AddEntity(_entityFactory.CreateGameOverVfx());
                }

                _updatesCounter++;

                if (_updatesCounter > 10)
                {
                    var gameOver = scene.RootEntities.Single(e => e.Name == "GameOver");
                    gameOver.GetComponent<SpriteRendererComponent>().Visible = true;
                }

                var bird = scene.RootEntities.Single(e => e.Name == "Bird");
                if (bird.GetComponent<InputComponent>().GetActionState("Flap") && _updatesCounter > 30)
                {
                    GlobalGameState.IsRetry = true;
                    _sceneManager.LoadScene(@"Assets\Level\Empty.scene");
                }
            }
            else
            {
                _updatesCounter = 0;
            }
        }

        public void ProcessUpdate(Scene scene, GameTime gameTime)
        {
        }

        public string Name { get; } = typeof(GameOverSystem).FullName;
    }
}