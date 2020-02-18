using Geisha.Engine.Audio;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class BirdSoundComponent : BehaviorComponent
    {
        private readonly ISound _wingSound;
        private readonly ISound _hitSound;
        private readonly ISound _dieSound;
        private SoundPlayer _soundPlayer;

        public BirdSoundComponent(ISound wingSound, ISound hitSound, ISound dieSound)
        {
            _wingSound = wingSound;
            _hitSound = hitSound;
            _dieSound = dieSound;
        }

        public void PlayWingSound()
        {
            _soundPlayer.Play(_wingSound);
        }

        public void PlayHitSound()
        {
            _soundPlayer.Play(_hitSound);
        }

        public void PlayDieSound()
        {
            _soundPlayer.Play(_dieSound);
        }

        public override void OnStart()
        {
            _soundPlayer = new SoundPlayer(Entity.Scene);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _soundPlayer.Update();
        }
    }
}