using System.Linq;
using Geisha.Engine.Audio;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class IncrementScoreComponent : BehaviorComponent
    {
        private readonly ISound _scoreSound;
        private SoundPlayer _soundPlayer;
        private TransformComponent _transformComponent;
        private TransformComponent _birdTransformComponent;

        public IncrementScoreComponent(ISound scoreSound)
        {
            _scoreSound = scoreSound;
        }

        public override void OnStart()
        {
            _soundPlayer = new SoundPlayer(Entity.Scene);
            _transformComponent = Entity.GetComponent<TransformComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<TransformComponent>();
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