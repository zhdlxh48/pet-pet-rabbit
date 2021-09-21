using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace Rabbit
{
    public class NoteHitController : MonoBehaviour
    {
        private IHitProcess noteHitProcess;
        
        [Header("현재 키를 입력해야 할 노트")]
        [ReadOnly] [SerializeField] private Note triggeredNote;
        
        private const string triggerNoteTag = "Note";
        private bool IsNoteTriggered => triggeredNote != null;
        
        private void Awake()
        {
            noteHitProcess = FindObjectOfType<NoteHandler>();
        }

        /// <summary>
        /// When you pressed the key, triggered note will destroyed
        /// if the argument has the same note type with triggered note
        /// </summary>
        /// <param name="type">Bound Type of pressed key</param>
        public void HitNote(NoteType type)
        {
            if (StageManager.GameState != StageState.Start) return;
            if (IsNoteTriggered) {
                // 키 입력과 TriggeredNote의 타입이 일치하는지 Check
                if (type == triggeredNote.type) {
                    noteHitProcess.NoteHitCorrect(triggeredNote);
                }
                else {
                    noteHitProcess.NoteHitFailed(triggeredNote);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(triggerNoteTag)) { // Triggered Object의 Tag가 Tile일 경우
                triggeredNote = other.GetComponent<Note>(); // TriggeredNote에 저장
            }
        }
    }
}