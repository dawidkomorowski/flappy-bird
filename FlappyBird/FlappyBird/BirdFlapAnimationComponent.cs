using Geisha.Engine.Core.Components;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public sealed class BirdFlapAnimationComponent : BehaviorComponent
    {
        private readonly Sprite[] _animationFrames;
        private SpriteRendererComponent _spriteRendererComponent;

        private int _updateCounter;
        private int _animationFrameCounter;
        private const int AnimationDuration = 5;

        public BirdFlapAnimationComponent(Sprite downFlapSprite, Sprite midFlapSprite, Sprite upFlapSprite)
        {
            _animationFrames = new Sprite[4];
            _animationFrames[0] = downFlapSprite;
            _animationFrames[1] = midFlapSprite;
            _animationFrames[2] = upFlapSprite;
            _animationFrames[3] = midFlapSprite;
        }

        public override void OnStart()
        {
            _spriteRendererComponent = Entity.GetComponent<SpriteRendererComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing)
            {
                _updateCounter++;

                if (_updateCounter % AnimationDuration == 0)
                {
                    _animationFrameCounter++;
                }

                var frameIndex = _animationFrameCounter % 4;
                _spriteRendererComponent.Sprite = _animationFrames[frameIndex];
            }
        }
    }
}