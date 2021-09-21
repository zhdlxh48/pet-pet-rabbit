using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rabbit
{
    public class NoteHandler : MonoBehaviour, INoteListAccess, IHitProcess
    {
        [Header("Note 관련 컴포넌트")]
        [SerializeField] private NoteCreator noteCreator;
        [SerializeField] private NoteMoveController moveController;
        [SerializeField] private NoteEffectManager effectManager;
        [SerializeField] private InputSFXManager sfxManager;
        [SerializeField] private TargetSpriteChanger targetChanger;
        
        [Header("Rabbit 관련 컴포넌트")]
        [SerializeField] private RabbitPictureManager rabbitPicManager;
        [SerializeField] private RabbitPlayManager rabbitPlayManager;
        
        [Header("Game Status 관련 컴포넌트")]
        [SerializeField] private GameCombo comboSystem;
        [SerializeField] private GameScore scoreSystem;
        [SerializeField] private GameTime timeSystem;
        [SerializeField] private GameHealth healthSystem;

        [Header("Play Area Background Object")]
        [SerializeField] private LoopObject flowerLoopObject;
        [SerializeField] private BGMoveObject grassMoveObject;

        [Header("UI Update Event")] 
        [SerializeField] private UnityEvent uiUpdateEvent;
 
        public List<Note> NoteList { get; set; }

        private Note NextNote => NoteList[1];
        public bool IsNoteExist => NoteList.Count != 0;

        private void Awake()
        {
            NoteList = new List<Note>();
            
            noteCreator = FindObjectOfType<NoteCreator>();
            moveController = FindObjectOfType<NoteMoveController>();
            
            rabbitPlayManager = FindObjectOfType<RabbitPlayManager>();
            rabbitPicManager = FindObjectOfType<RabbitPictureManager>();
        }

        private void Start()
        {
            noteCreator.NotePooling();
        }

        private void Update()
        {
            if (StageManager.GameState == StageState.Start) {
                moveController.NoteMove();
            }
        }

        public void NoteHitCorrect(Note note)
        {
            sfxManager.PlayOneShot("Press");
            
            targetChanger.SetSprite(TargetState.Success);
            
            comboSystem.ComboSuccess();
            scoreSystem.GetScore(note.type, comboSystem.Combo);
            healthSystem.Heal(note.type);

            PlayAreaObjectMove();
            
            if (NoteList.Count > 1) { // Check Is List Empty? (Game End)
                if (NextNote.mainCategory == NoteCategory.SubObjectNote) {
                    rabbitPlayManager.SetStateBySubObject(((SpecialNote)NextNote).subObject.objectType);
                }
                else {
                    rabbitPlayManager.RabbitWalk();
                }
            }
            else { // List Empty
                rabbitPlayManager.SetState(RabbitPlayState.Idle);
            }
            rabbitPicManager.ComboOnGoingAnimation();
            
            effectManager.EffectShot(note.type);
            
            NoteDestroy(note);
            NoteOrderFix();
            
            uiUpdateEvent.Invoke();
        }

        public void NoteHitFailed(Note note)
        {
            targetChanger.SetSprite(TargetState.Failed);
            
            comboSystem.BreakCombo();
            timeSystem.ReduceTime(note.type);
            healthSystem.Damage(note.type);

            if (NoteList.Count > 1) {
                if (NextNote.mainCategory == NoteCategory.SubObjectNote) { // 앞에 SubObject가 존재하는 Note인 경우
                    var tempObject = ((SpecialNote)NextNote).subObject;
                    tempObject.PlayAnimation(false);
                    rabbitPlayManager.SetStateBySubObject(tempObject.objectType, false);
                }
            }
            rabbitPicManager.ComboBreakAnimation();
            
            uiUpdateEvent.Invoke();
        }

        private bool NoteDestroy(Note note)
        {
            try {
                if (!NoteList.Remove(note)) {
                    throw new Exception("[ERROR] NoteDestroy function has error");
                }
            }
            catch (Exception _ex) {
                Debug.LogError(_ex);
                throw;
            }
            
            DestroyImmediate(note.gameObject);
            return true;
        }
        
        private void NoteOrderFix()
        {
            foreach (var item in NoteList) {
                item.OrderFix(1, Vector3.Normalize(item.transform.position - transform.position));
            }
        }

        private void PlayAreaObjectMove()
        {
            flowerLoopObject.MoveLeftLoop();
            if (StageManager.LevelData.count == NoteList.Count || NoteList.Count == 1) {
                grassMoveObject.MoveLeft();
            }
        }
    }
}