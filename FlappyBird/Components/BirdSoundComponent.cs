using Geisha.Engine.Audio;
using Geisha.Engine.Audio.Backend;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class BirdSoundComponent : BehaviorComponent
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly ISound _wingSound;
        private readonly ISound _hitSound;
        private readonly ISound _dieSound;

        public BirdSoundComponent(IAudioPlayer audioPlayer, ISound wingSound, ISound hitSound, ISound dieSound)
        {
            _audioPlayer = audioPlayer;
            _wingSound = wingSound;
            _hitSound = hitSound;
            _dieSound = dieSound;
        }

        public void PlayWingSound()
        {
            _audioPlayer.PlayOnce(_wingSound);
        }

        public void PlayHitSound()
        {
            _audioPlayer.PlayOnce(_hitSound);
        }

        public void PlayDieSound()
        {
            _audioPlayer.PlayOnce(_dieSound);
        }
    }
}