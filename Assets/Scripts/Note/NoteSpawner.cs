using System;
using System.Collections;
using UnityEngine;

namespace Rabbit
{
    public class NoteSpawner : MonoBehaviour
    {
        private INoteListAccess noteListAccessor;

        private NoteMoveController moveController;
        
        [SerializeField] private float spawnInterval;
        
        private WaitForSeconds spawnNoteMoveInterval;
        private Coroutine spawnRoutine;

        private void Awake()
        {
            noteListAccessor = FindObjectOfType<NoteHandler>();
            moveController = FindObjectOfType<NoteMoveController>();
            
            spawnNoteMoveInterval = new WaitForSeconds(spawnInterval / 3.0f);
        }

        private void OnEnable()
        {
            spawnRoutine = StartCoroutine(NoteSpawn());
        }

        private void OnDisable()
        {
            StopCoroutine(spawnRoutine);
        }

        private IEnumerator NoteSpawn()
        {
            yield return new WaitUntil(() => StageManager.GameState == StageState.CountDown);

            var showNoteCount = moveController.screenShowNoteCount;
            for (var i = 0; i < showNoteCount; i++) {
                noteListAccessor.NoteList[i].gameObject.SetActive(true);
                for (var j = 0; j < 3; j++) {
                    moveController.NoteMove(1.0f);
                    yield return spawnNoteMoveInterval;
                }
            }

            for (var i = showNoteCount; i < StageManager.LevelData.count; i++) {
                noteListAccessor.NoteList[i].gameObject.SetActive(true);
            }
            
            gameObject.SetActive(false);
        }
    }
}