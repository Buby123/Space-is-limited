using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

namespace Project.Player
{
    [System.Serializable]
    public class DataControls : OutgameData<DataControls>
    {
        public DataControls()
        {
            Initialize(this, "DataControls");
        }

        #region Objects
        [Tooltip("Standard Keys")]
        [SerializeField] private KeyCode rightKey = KeyCode.D;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode downKey = KeyCode.S;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        [SerializeField] private KeyCode interactionKey = KeyCode.E;
        [SerializeField] private KeyCode specialAbilityKey = KeyCode.Space;
        [SerializeField] private KeyCode menuKey = KeyCode.Escape;
        #endregion

        #region Variables
        public KeyCode RightKey => rightKey;
        public KeyCode LeftKey => leftKey;
        public KeyCode DownKey => downKey;
        public KeyCode JumpKey => jumpKey;
        public KeyCode InteractionKey => interactionKey;
        public KeyCode SpecialAbilityKey => specialAbilityKey;
        public KeyCode MenuKey => menuKey;
        #endregion

        #region Get/Set Functions
        /// <summary>
        /// This function can be called to get the KeyCode of a specific action.
        /// </summary>
        /// <param name="actionName"> Name of the Action that should be replaced </param>
        /// <returns> Keycode of a specific Action</returns>
        public KeyCode GetKeyCodeOfAction(string actionName)
        {
            switch (actionName)
            {
                case "Move_Right":
                    return rightKey;
                case "Move_Left":
                    return leftKey;
                case "Move_Down":
                    return downKey;
                case "Jump":
                    return jumpKey;
                case "Interaction":
                    return interactionKey;
                case "Special_Ability":
                    return specialAbilityKey;
                case "Open/Close_Menu":
                    return menuKey;
                default:
                    Debug.LogError("Action Name was not registered!");
                    return KeyCode.None;
            }
        }

        /// <summary>
        /// This function can be called to assign a new KeyCode to a specific action.
        /// </summary>
        /// <param name="actionName"> Name of the Action that should be replaced </param>
        /// <param name="keyCode"> The new KeyCode for that action </param>
        public void SetKeyCodeOfAction(string actionName, KeyCode keyCode)
        {
            if (!IsKeyCodeAvailable(keyCode))
            {
                Debug.LogError("Assignement of new KeyCode has failed because the Key is alread assigned otherwise!");
                return;
            }

            switch (actionName)
            {
                case "Move_Right":
                    rightKey = keyCode;
                    break;
                case "Move_Left":
                    leftKey = keyCode;
                    break;
                case "Move_Down":
                    downKey = keyCode;
                    break;
                case "Jump":
                    jumpKey = keyCode;
                    break;
                case "Interaction":
                    interactionKey = keyCode;
                    break;
                case "Special_Ability":
                    specialAbilityKey = keyCode;
                    break;
                case "Open/Close_Menu":
                    menuKey = keyCode;
                    break;
                default:
                    Debug.LogError("Action Name was not registered!");
                    break;
            }
            SaveData();
        }
        #endregion

        #region Request Available
        /// <summary>
        /// This function can be used to see if a specific KeyCode is already in use.
        /// Before assigning a new KeyCode there should always be the security that
        /// there are no collisions between KeyCode assignments. This can be done using this function.
        /// </summary>
        /// <param name="keyCode"> The KeyCode that should be tested </param>
        /// <returns> Returns if the given KeyCode is not in use currently </returns>
        public bool IsKeyCodeAvailable(KeyCode keyCode)
        {
            if (keyCode == rightKey || keyCode == leftKey || keyCode == downKey || keyCode == jumpKey ||
                keyCode == interactionKey || keyCode == specialAbilityKey || keyCode == menuKey)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
