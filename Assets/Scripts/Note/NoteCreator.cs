using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rabbit
{
    public class NoteCreator : MonoBehaviour
    {
        [SerializeField] private NoteDictionary noteDic;

        [SerializeField] private Transform noteParent;
        [SerializeField] private Transform initialPos;

        [SerializeField] private NoteType[] defaultNoteRange;
        [SerializeField] private int defaultNoteCreateCount;

        public INoteListAccess noteListAccessor;

        private NoteType prevType;
        private int redundantCheckCount;
        private int RandomDefaultType => Random.Range(0, defaultNoteRange.Length);

        private void Awake()
        {
            noteListAccessor = FindObjectOfType<NoteHandler>();
        }

        public void NotePooling()
        {
            noteListAccessor.NoteList.Clear();
            
            var tempPos = initialPos.position;

            var randomNoteRange = StageManager.LevelData.types.ToArray();
            var createNum = StageManager.LevelData.count;

            prevType = NoteType.Flower;
            redundantCheckCount = 0;

            for (var i = 0; i < createNum; i++) {
                var tempType = i < defaultNoteCreateCount
                    ? defaultNoteRange[RandomDefaultType]
                    : SubObjectDuplicateCheck(randomNoteRange[Random.Range(0, randomNoteRange.Length)]);
                var tempNote = Instantiate(noteDic[tempType], tempPos, noteParent);
                tempNote.Initialize(i, (tempPos - noteParent.position).normalized * i);
                tempNote.gameObject.SetActive(false);
                noteListAccessor.NoteList.Add(tempNote);
            }
        }

        private NoteType SubObjectDuplicateCheck(NoteType type)
        {
            return IsSubObjectAllow(type) ? ChangeToDefaultNote(type) : type;
        }

        private bool IsSubObjectAllow(NoteType type)
        {
            if (redundantCheckCount > 0) redundantCheckCount--;

            if (IsSubObjectNote(type)) {
                if (IsSubObjectNote(prevType)) // 이전의 노트가 위에 오브젝트가 있어야 하는 노트라면
                {
                    if (redundantCheckCount == 0) // 이미 이전의 노트가 오브젝트 중복 방지를 위해 막아두었다면
                        redundantCheckCount = 2; // 체크카운트를 다시 초기화 해준다
                }
                else {
                    redundantCheckCount = 2; // 이전의 노트가 일반 노트라면 중복을 체크하기 위해 체크 카운트를 초기화해준다
                }

                prevType = type;
                return redundantCheckCount == 1; // 앞에 노트가 오브젝트를 가지고 있다면 그 다음 노트는 오브젝트 생성을 막는다
            }

            redundantCheckCount = 0;
            prevType = type;
            return false; // 일반 노트일 경우 그냥 생성한다
        }

        private static NoteType ChangeToDefaultNote(NoteType type)
        {
            switch (type) {
                case NoteType.Butterfly:
                    return NoteType.ButterflyNonObject;
                case NoteType.WaterDrop:
                    return NoteType.WaterDropNonObject;
                default:
                    return type;
            }
        }

        private static bool IsSubObjectNote(NoteType type)
        {
            return type == NoteType.Butterfly || type == NoteType.WaterDrop;
        }

        private static Note Instantiate(GameObject noteObj, Vector3 spawn, Transform parent)
        {
            return Instantiate(noteObj, spawn, Quaternion.identity, parent).GetComponent<Note>();
        }

        [Serializable]
        private class NoteDictionary : SerializableDictionaryBase<NoteType, GameObject> { }
    }
}