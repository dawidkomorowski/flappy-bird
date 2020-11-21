using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class IntroComponent : BehaviorComponent
    {
        public override void OnFixedUpdate()
        {
            if (GlobalGameState.CurrentPhase == GlobalGameState.Phase.WaitingForPlayer) return;

            Entity.DestroyAfterFullFrame();
        }
    }
}