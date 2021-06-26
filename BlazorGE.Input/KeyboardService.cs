namespace BlazorGE.Input
{
    public class KeyboardService
    {
        #region Protected Static Properties

        protected KeyboardState KeyboardState = new KeyboardState(new Keys[] { });

        #endregion

        #region Public Methods

        public KeyboardState GetState() => KeyboardState;

        #endregion
    }
}
