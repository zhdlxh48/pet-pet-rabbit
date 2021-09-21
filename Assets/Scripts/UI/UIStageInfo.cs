using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Rabbit
{
    public class UIStageInfo : MonoBehaviour
    {
        [System.Serializable]
        public class NoteUIInfo
        {
            public Sprite sprite;
            public string key;
        }

        [System.Serializable]
        public class NoteUIInfoSocket
        {
            public GameObject infoObject;
            public Image imageSocket;
            public TMP_Text textSocket;

            public void SetInfo(Sprite sprite, string key)
            {
                imageSocket.sprite = sprite;
                textSocket.text = key;
            }
        }
        
        [System.Serializable]
        public class NoteInfoDictionary : SerializableDictionaryBase<NoteType, NoteUIInfo> { }

        [SerializeField] private GameObject[] stageInfoObject;

        [SerializeField] private UnityEvent stageChangeEvent;

        private void Start()
        {
            StartCoroutine(ShowStageNoteInfo());
        }

        private void Update()
        {
            if (StageManager.GameState != StageState.ShowInfo) return;

            if (Keyboard.current.anyKey.wasPressedThisFrame) {
                stageChangeEvent.Invoke();
            }
        }

        private IEnumerator ShowStageNoteInfo()
        {
            yield return new WaitUntil(() => StageManager.GameState == StageState.ShowInfo);

            SetStageInfo(StageManager.CurrentLevel);
        }

        private void SetStageInfo(int level)
        {
            foreach (var obj in stageInfoObject) {
                obj.SetActive(false);
            }
            stageInfoObject[level].SetActive(true);
        }
    }
}