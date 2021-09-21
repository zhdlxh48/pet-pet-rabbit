using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Rabbit
{
    public class GameInput : MonoBehaviour
    {
        private GameControls inputSystem;

        [SerializeField] private Dictionary<NoteType, InputAction> inputActionDictionary;

        public static NoteType pressedType;
        public UnityEvent<NoteType> pressedEvent;
        
        private void Awake()
        {
            inputSystem = new GameControls();
            inputActionDictionary = new Dictionary<NoteType, InputAction>();

            InitializeDictionary();
        }

        private void OnEnable()
        {
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            inputSystem.Disable();
        }

        private void InitializeDictionary()
        {
            inputActionDictionary[NoteType.Flower] = inputSystem.Note.FlowerHit;
            inputActionDictionary[NoteType.Leaf] = inputSystem.Note.LeafHit;
            inputActionDictionary[NoteType.Butterfly] = inputSystem.Note.ButterflyHit;
            inputActionDictionary[NoteType.WaterDrop] = inputSystem.Note.WaterDropHit;
            inputActionDictionary[NoteType.Rabbit] = inputSystem.Note.RabbitHit;
            
            foreach (var pair in inputActionDictionary) {
                print($"[SYSTEM] Key Registered : {pair.Key}");
                pair.Value.performed += (ctx) => pressedEvent.Invoke(pressedType = pair.Key);
                //pair.Value.performed += (ctx) => print($"[DEBUG] Key Pressed : {pair.Key}"); // DEBUG
            }
        }

        public void SetBindKey(NoteType type, Key key)
        {
            inputActionDictionary[type].ChangeBinding(Keyboard.current[key].path);
        }
    }
}