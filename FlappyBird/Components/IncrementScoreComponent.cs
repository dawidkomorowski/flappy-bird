using System.Linq;
using Geisha.Engine.Audio;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class IncrementScoreComponent : BehaviorComponent
    {
        private readonly ISound _scoreSound;
        private SoundPlayer _soundPlayer;
        private Transform2DComponent _transformComponent;
        private Transform2DComponent _birdTransformComponent;

        public IncrementScoreComponent(ISound scoreSound)
        {
            _scoreSound = scoreSound;
        }

        public override void OnStart()
        {
            _soundPlayer = new SoundPlayer(Entity.Scene);
            _transformComponent = Entity.GetComponent<Transform2DComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<Transform2DComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (_transformComponent.Translation.X < _birdTransformComponent.Translation.X)
            {
                GlobalGameState.Score++;
                _soundPlayer.Play(_scoreSound);
                _soundPlayer.Update();
                Entity.RemoveComponent(this);
            }
        }
    }
}