using Geisha.Common.Math;
using Geisha.Engine.Core.Components;

namespace FlappyBird.Components
{
    public sealed class PipeScrollingComponent : BehaviorComponent
    {
        private const double Speed = 5;

        public override void OnFixedUpdate()
        {
            if (ShouldScroll())
            {
                var transformComponent = Entity.GetComponent<Transform2DComponent>();
                transformComponent.Translation -= new Vector2(Speed, 0);

                if (transformComponent.Translation.X < -1000)
                {
                    Entity.DestroyAfterFullFrame();
                }
            }
        }

        private bool ShouldScroll()
        {
            return GlobalGameState.CurrentPhase == GlobalGameState.Phase.WaitingForPlayer || GlobalGameState.CurrentPhase == GlobalGameState.Phase.Playing;
        }
    }
}