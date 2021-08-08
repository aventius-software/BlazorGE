#region Namespaces

using BlazorGE.Game;
using BlazorGE.Game.Components;
using BlazorGE.Game.Entities;
using BlazorGE.Graphics2D.Services;
using BlazorGE.Input;
using DemoPongStyleGame.Game.Components;
using System.Numerics;

#endregion

namespace DemoPongStyleGame.Game.Factories
{
    public class PlayerFactory
    {
        #region Protected Constants

        protected const int DefaultHeight = 100;
        protected const int DefaultWidth = 50;

        #endregion

        #region Protected Properties

        protected GameWorld GameWorld;
        protected IGraphicsService2D GraphicsService2D;
        protected IKeyboardService KeyboardService;

        #endregion

        #region Constructors

        public PlayerFactory(GameWorld gameWorld, IGraphicsService2D graphicsService2D, IKeyboardService keyboardService)
        {
            GameWorld = gameWorld;
            GraphicsService2D = graphicsService2D;
            KeyboardService = keyboardService;
        }

        #endregion

        #region Public Methods

        public GameEntity CreatePlayer()
        {
            // Generate starting position for the player
            var position = new Vector2(GraphicsService2D.CanvasWidth - DefaultWidth, GraphicsService2D.CanvasHeight / 2);

            // Create entity
            var entity = GameWorld.CreateGameEntity();
            entity.AttachGameComponent(new PlayerMovementComponent(KeyboardService, GraphicsService2D));
            entity.AttachGameComponent(new Transform2DComponent { Width = DefaultWidth, Height = DefaultHeight, Position = position });
            entity.AttachGameComponent(new PlayerDrawComponent(GraphicsService2D));
            entity.Activate();

            return entity;
        }

        #endregion
    }
}
