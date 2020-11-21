using System;
using Geisha.Common.Math;
using Geisha.Engine.Audio;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace FlappyBird
{
    public interface IEntityFactory
    {
        Entity CreateCamera();
        Entity CreateBackgroundDay();
        Entity CreateGround();
        Entity CreateBird();
        Entity CreatePipe();
        Entity CreateScore();
        Entity CreateIntro();
        Entity CreateGameOver();
        Entity CreateGameOverVfx();
        Entity CreateDebugBirdCollisionBox();

        IncrementScoreComponent CreateIncrementScoreComponent();
    }

    public sealed class EntityFactory : IEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public EntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        public Entity CreateCamera()
        {
            var entity = new Entity();
            entity.AddComponent(Transform2DComponent.CreateDefault());
            entity.AddComponent(new CameraComponent
            {
                AspectRatioBehavior = AspectRatioBehavior.Overscan,
                ViewRectangle = new Vector2(1280, 720)
            });
            return entity;
        }

        public Entity CreateBackgroundDay()
        {
            var entity = new Entity();
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("01497f1f-7d61-46fa-b2a2-57c152eb88f7"))),
                SortingLayerName = "Background"
            });
            return entity;
        }

        public Entity CreateGround()
        {
            var entity = new Entity();
            entity.AddComponent(Transform2DComponent.CreateDefault());
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("f0b24406-7fce-43e2-98b0-eb903fbc1e76"))),
                SortingLayerName = "Ground"
            });
            entity.AddComponent(new GroundScrollingComponent());
            return entity;
        }

        public Entity CreateBird()
        {
            var entity = new Entity {Name = "Bird"};
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("71f76a03-88d4-454c-b372-eaae11fc120f"))),
                SortingLayerName = "Bird"
            });
            entity.AddComponent(new InputComponent
            {
                InputMapping = new InputMapping
                {
                    ActionMappings =
                    {
                        new ActionMapping
                        {
                            ActionName = "Flap",
                            HardwareActions =
                            {
                                new HardwareAction
                                {
                                    HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Space)
                                }
                            }
                        }
                    }
                }
            });
            entity.AddComponent(new BirdIdleFlyingComponent());
            entity.AddComponent(new BirdFlapAnimationComponent(
                downFlapSprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("478361b7-1ad1-44e9-84f9-0a42253ed334"))),
                midFlapSprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("71f76a03-88d4-454c-b372-eaae11fc120f"))),
                upFlapSprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("1e3111a7-9d0f-46e5-81a0-d4bf32e54256")))
            ));
            entity.AddComponent(new RectangleColliderComponent
            {
                Dimension = new Vector2(32 - 2, 24 - 2)
            });
            entity.AddComponent(new BirdSoundComponent(
                wingSound: _assetStore.GetAsset<ISound>(new AssetId(new Guid("4ee1890b-3b92-45bb-9bee-a81e270f61d6"))),
                hitSound: _assetStore.GetAsset<ISound>(new AssetId(new Guid("7224f1b5-1471-4741-b720-0a10fc99ea53"))),
                dieSound: _assetStore.GetAsset<ISound>(new AssetId(new Guid("d1235819-13d0-419f-a50d-71478c1ad9bd")))));
            return entity;
        }

        public Entity CreatePipe()
        {
            var entity = new Entity();
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("39d74afc-5c19-41ee-b304-e2422aff7e4e"))),
                SortingLayerName = "Pipe"
            });
            entity.AddComponent(new PipeScrollingComponent());
            entity.AddComponent(new RectangleColliderComponent
            {
                Dimension = new Vector2(52, 320)
            });
            return entity;
        }

        public Entity CreateScore()
        {
            var entity = new Entity();
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new ScoreComponent(
                d0Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("dee07c7d-c0df-4472-9fa5-b793c9a44b6d"))),
                d1Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("ef8038dd-4a01-405d-a747-a9cc6f874499"))),
                d2Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("37de19dd-e476-44bf-a313-f70ac0c33051"))),
                d3Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("85933e40-51dc-4569-9051-a4ca61c4fbcf"))),
                d4Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("21a00b80-dbce-42c3-9ce5-a1d0e543927e"))),
                d5Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("89361a96-a9ef-44e6-b189-87cc457321d0"))),
                d6Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("6bea5224-a823-45a6-ac72-be467509fc2d"))),
                d7Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("850eab6d-1fb9-4091-8613-ab72d80327c3"))),
                d8Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("3e9a1113-f104-4fe1-8454-d15c44bf9457"))),
                d9Sprite: _assetStore.GetAsset<Sprite>(new AssetId(new Guid("53305915-2fe5-44af-9027-c220bf2cd8cc")))));
            return entity;
        }

        public Entity CreateIntro()
        {
            var entity = new Entity();
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("11a37972-74d7-4873-b5c4-54344ab87d6b"))),
                SortingLayerName = "UI"
            });
            entity.AddComponent(new IntroComponent());
            return entity;
        }

        public Entity CreateGameOver()
        {
            var entity = new Entity {Name = "GameOver"};
            entity.AddComponent(new Transform2DComponent
            {
                Translation = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(2, 2)
            });
            entity.AddComponent(new SpriteRendererComponent
            {
                Sprite = _assetStore.GetAsset<Sprite>(new AssetId(new Guid("de7e5788-c21c-4897-b014-c79c41ae39dd"))),
                SortingLayerName = "UI"
            });
            return entity;
        }

        public Entity CreateGameOverVfx()
        {
            var entity = new Entity();
            entity.AddComponent(Transform2DComponent.CreateDefault());
            entity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(0, 255, 255, 255),
                FillInterior = true,
                SortingLayerName = "VFX",
                Dimension = new Vector2(1280, 720)
            });
            entity.AddComponent(new GameOverVfxComponent());
            return entity;
        }

        public Entity CreateDebugBirdCollisionBox()
        {
            var entity = new Entity();
            entity.AddComponent(Transform2DComponent.CreateDefault());
            entity.AddComponent(new RectangleRendererComponent
            {
                SortingLayerName = "UI",
                Color = Color.FromArgb(255, 255, 0, 0),
                Dimension = new Vector2(32 - 2, 24 - 2),
            });
            entity.AddComponent(new DebugBirdCollisionBoxComponent());
            return entity;
        }

        public IncrementScoreComponent CreateIncrementScoreComponent()
        {
            return new IncrementScoreComponent(_assetStore.GetAsset<ISound>(new AssetId(new Guid("1b7f42a1-e6e1-4140-ad0f-e11f5814145f"))));
        }
    }
}