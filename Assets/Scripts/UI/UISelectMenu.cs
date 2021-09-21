using System;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Rabbit
{
    public class UISelectMenu : MonoBehaviour
    {
        [System.Serializable]
        public class MenuElementObject
        {
            public Image caption;
            public TMP_Text content;
        }
        
        [System.Serializable]
        public class SelectMenuElement
        {
            public MenuElementObject menuObject;
            public UnityEvent menuEvent;
        }
        
        [System.Serializable]
        public class SelectMenuDictionary : SerializableDictionaryBase<int, SelectMenuElement> { }

        [SerializeField] private SelectMenuDictionary selMenuDic;

        private int selIndex;

        private void OnEnable()
        {
            selIndex = 0;
            SetSelectMenu(selIndex);
        }

        private void Update()
        {
            SelectMenuInputProcess();
        }

        private void SelectMenuInputProcess()
        {
            if (Keyboard.current[Key.UpArrow].wasPressedThisFrame) {
                SetSelectMenu(selIndex - 1);
            }
            else if (Keyboard.current[Key.DownArrow].wasPressedThisFrame) {
                SetSelectMenu(selIndex + 1);
            }
            else if (Keyboard.current[Key.Enter].wasPressedThisFrame) {
                selMenuDic[selIndex].menuEvent.Invoke();
            }
        }

        private void SetSelectMenu(int index)
        {
            foreach (var menu in selMenuDic.Values) {
                menu.menuObject.caption.enabled = false;
            }

            selIndex = Mathf.Clamp(index, 0, selMenuDic.Count - 1);
            selMenuDic[selIndex].menuObject.caption.enabled = true;
        }
    }
}