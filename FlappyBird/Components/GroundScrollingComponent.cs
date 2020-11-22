using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class GroundScrollingComponent : BehaviorComponent
    {
        private const double Speed = 5;

        public override void OnFixedUpdate()
        {
            if (ShouldScroll())
            {
                Debug.Assert(Entity != null, nameof(Entity) + " != null");

                var transformComponent = Entity.GetComponent<Transform2DComponent>();
                transformComponent.Translation -= new Vector2(Speed, 0);

                if (transformComponent.Translation.X < -1000)
                {
                    const double groundWidth = 336;
                    transformComponent.Translation += new Vector2(groundWidth * 10, 0);
                }
            }
        }

        private bool ShouldScroll()
        {
            return GlobalGameState.CurrentPhase == GlobalGameState.Phase.WaitingForPlayer || GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing;
        }
    }
}