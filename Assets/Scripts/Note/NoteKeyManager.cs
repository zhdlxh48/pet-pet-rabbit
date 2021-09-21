using System;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rabbit
{
    public class NoteKeyManager : MonoBehaviour
    {
        [System.Serializable]
        public class NoteKeyDictionary : SerializableDictionaryBase<NoteType, Key>
        {
            public bool ChangeKeyFromNote(NoteType type, Key key)
            {
                foreach (var noteType in Keys.Where(linqKey => linqKey == type)) {
                    this[noteType] = key;
                    return true;
                }
                return false;
            }
        }

        [SerializeField] private GameInput gameInput;

        [SerializeField] private NoteKeyDictionary keyDic;

        private void Start()
        {
            foreach (var pair in keyDic) {
                gameInput.SetBindKey(pair.Key, pair.Value);
            }
        }

        private void Reset()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            keyDic.Clear();
            
            keyDic.Add(NoteType.Flower, Key.S);
            keyDic.Add(NoteType.Leaf, Key.K);
            keyDic.Add(NoteType.Butterfly, Key.D);
            keyDic.Add(NoteType.WaterDrop, Key.J);
            keyDic.Add(NoteType.Rabbit, Key.Space);
        }
        
        public bool ChangeBindKey(NoteType type, Key key)
        {
            return keyDic.ChangeKeyFromNote(type, key);
        }

        // TODO: Fix - Cause Crash
        private void KeyChange(NoteType type)
        {
            var allKeys = Keyboard.current.allKeys;
            var isPressedKey = false;
        
            while (!isPressedKey) {
                foreach (var key in allKeys.Where(key => key.wasPressedThisFrame)) {
                    print($"[SYSTEM] Key Bind Changed : Previous - {keyDic[type]} => Current - {key.keyCode}");
                    ChangeBindKey(type, key.keyCode);
                    isPressedKey = true;
                }
            }
        }
    }
}