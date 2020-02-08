using System.Linq;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public sealed class GameOverSystem : IFixedTimeStepSystem
    {
        private readonly ISceneManager _sceneManager;
        private int _updatesCounter;

        public GameOverSystem(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void FixedUpdate(Scene scene)
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.GameOver)
            {
                _updatesCounter++;

                var gameOver = scene.RootEntities.Single(e => e.Name == "GameOver");
                gameOver.GetComponent<SpriteRendererComponent>().Visible = true;

                var bird = scene.RootEntities.Single(e => e.Name == "Bird");
                if (bird.GetComponent<InputComponent>().GetActionState("Flap") && _updatesCounter > 30)
                {
                    _sceneManager.LoadScene(@"Assets\Level\Empty.scene");
                }
            }
            else
            {
                _updatesCounter = 0;
            }
        }

        public string Name { get; } = typeof(GameOverSystem).FullName;
    }
}