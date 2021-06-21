#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace BlazorGE.Input
{
    public struct KeyboardState
    {
        #region Private Properties

        private IDictionary<Keys, KeyState> KeyStateDictionary;

        #endregion

        #region Public Properties

        public KeyState this[Keys key]
        {
            get
            {
                return KeyStateDictionary[key];
            }
        }

        public bool CapsLock { get; }
        public bool NumLock { get; }

        #endregion

        #region Constructors

        public KeyboardState(Keys[] keys, bool capsLock = false, bool numLock = false)
        {
            CapsLock = capsLock;
            NumLock = numLock;
            KeyStateDictionary = Enum.GetValues<Keys>().ToDictionary(key => key, val => KeyState.Up);
            
            foreach (var key in keys)
            {
                KeyStateDictionary[key] = KeyState.Down;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an array of values holding keys that are currently being pressed.
        /// </summary>
        /// <returns></returns>
        public Keys[] GetPressedKeys() => KeyStateDictionary.Where(x => x.Value == KeyState.Down).Select(x => x.Key).ToArray();

        /// <summary>
        /// Returns true if the specified key is currently down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key) => KeyStateDictionary[key] == KeyState.Down;

        /// <summary>
        /// Returns true if the specified key is currently up
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyUp(Keys key) => !(KeyStateDictionary[key] == KeyState.Up);

        /// <summary>
        /// Set the specified key state
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyState"></param>
        public void SetKeyState(Keys key, KeyState keyState)
        {
            KeyStateDictionary[key] = keyState;
        }

        #endregion
    }
}
