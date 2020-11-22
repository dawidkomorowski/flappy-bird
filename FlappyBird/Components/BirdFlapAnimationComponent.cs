using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Animation.Components;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class BirdFlapAnimationComponent : BehaviorComponent
    {
        private Transform2DComponent _transformComponent = null!;
        private SpriteAnimationComponent _spriteAnimationComponent = null!;
        private const string FlapAnimation = "Flap";

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _transformComponent = Entity.GetComponent<Transform2DComponent>();
            _spriteAnimationComponent = Entity.GetComponent<SpriteAnimationComponent>();

            _spriteAnimationComponent.PlayAnimation(FlapAnimation);
            _spriteAnimationComponent.Stop();
        }

        public override void OnFixedUpdate()
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing)
            {
                if (_spriteAnimationComponent.IsPlaying)
                {
                    if (IsBirdFallingDown())
                    {
                        _spriteAnimationComponent.Stop();
                    }
                }
                else
                {
                    if (!IsBirdFallingDown())
                    {
                        _spriteAnimationComponent.Resume();
                    }
                }
            }
            else
            {
                _spriteAnimationComponent.Stop();
            }
        }

        private bool IsBirdFallingDown()
        {
            var angle = Angle.Rad2Deg(_transformComponent.Rotation);
            return angle < -85;
        }
    }
}