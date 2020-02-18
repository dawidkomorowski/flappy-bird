using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;

namespace FlappyBird
{
    public sealed class RemoveEntityInNextFrameComponent : BehaviorComponent
    {
        private bool _oneFrameProcessed;

        public override void OnUpdate(GameTime gameTime)
        {
            if (_oneFrameProcessed)
            {
                Entity.Destroy();
            }

            _oneFrameProcessed = true;
        }
    }
}