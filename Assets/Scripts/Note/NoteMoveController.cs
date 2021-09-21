using System;
using System.Linq;
using UnityEngine;

namespace Rabbit
{
    public class NoteMoveController : MonoBehaviour
    {
        private INoteListAccess noteListAccessor;

        [ReadOnly] public int screenShowNoteCount = 17;
        [SerializeField] private float noteSpeed;

        private void Awake()
        {
            noteListAccessor = FindObjectOfType<NoteHandler>();
        }

        public void NoteMove()
        {
            foreach (var note in noteListAccessor.NoteList.Where(MoveCondition)) {
                note.Move(noteSpeed * Time.deltaTime);
            }
        }
        public void NoteMove(float speed)
        {
            foreach (var note in noteListAccessor.NoteList.Where(MoveCondition)) {
                note.Move(speed);
            }
        }

        private bool MoveCondition(Note note, int index)
        {
            return index < screenShowNoteCount && note.gameObject.activeSelf;
        }
    }
}
