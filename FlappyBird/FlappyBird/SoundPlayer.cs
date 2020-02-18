using System.Collections.Generic;
using Geisha.Engine.Audio;
using Geisha.Engine.Audio.Components;
using Geisha.Engine.Core.SceneModel;

namespace FlappyBird
{
    public sealed class SoundPlayer
    {
        private readonly Queue<ISound> _soundsQueuedToPlay = new Queue<ISound>();
        private readonly Scene _scene;

        public SoundPlayer(Scene scene)
        {
            _scene = scene;
        }

        public void Play(ISound sound)
        {
            _soundsQueuedToPlay.Enqueue(sound);
        }

        public void Update()
        {
            while (_soundsQueuedToPlay.Count > 0)
            {
                var entity = new Entity();
                entity.AddComponent(new AudioSourceComponent {Sound = _soundsQueuedToPlay.Dequeue()});
                entity.AddComponent(new RemoveEntityInNextFrameComponent());
                _scene.AddEntity(entity);
            }
        }
    }
}