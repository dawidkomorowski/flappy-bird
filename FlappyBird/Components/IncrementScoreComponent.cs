﻿using System.Linq;
using Geisha.Engine.Audio;
using Geisha.Engine.Audio.Backend;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class IncrementScoreComponent : BehaviorComponent
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly ISound _scoreSound;
        private Transform2DComponent _transformComponent;
        private Transform2DComponent _birdTransformComponent;

        public IncrementScoreComponent(IAudioPlayer audioPlayer, ISound scoreSound)
        {
            _audioPlayer = audioPlayer;
            _scoreSound = scoreSound;
        }

        public override void OnStart()
        {
            _transformComponent = Entity.GetComponent<Transform2DComponent>();
            _birdTransformComponent = Entity.Scene.RootEntities.Single(e => e.Name == "Bird").GetComponent<Transform2DComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (_transformComponent.Translation.X < _birdTransformComponent.Translation.X)
            {
                GlobalGameState.Score++;
                _audioPlayer.PlayOnce(_scoreSound);
                Entity.RemoveComponent(this);
            }
        }
    }
}