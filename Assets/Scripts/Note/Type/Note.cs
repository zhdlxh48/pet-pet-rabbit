using System;
using UnityEngine;

namespace Rabbit
{
    public enum NoteCategory
    {
        DefaultNote, SubObjectNote
    }
    
    public enum NoteType
    {
        Dummy, DummyObject, Leaf, Flower, Butterfly, ButterflyNonObject, WaterDrop, WaterDropNonObject, Rabbit
    }
    
    public class Note : MonoBehaviour
    {
        public NoteCategory mainCategory;
        public NoteType type;
        
        [ReadOnly] [SerializeField] protected int listOrder;
        [ReadOnly] [SerializeField] protected Vector3 targetVector;

        public virtual void Initialize(int order, Vector3 target)
        {
            listOrder = order;
            targetVector = target;
        }

        public virtual void Move(float speed)
        {
            if (Vector3.Distance(transform.localPosition, targetVector) > 0.01f) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetVector, speed);
            }
            else {
                if (Mathf.Round(transform.localPosition.x - targetVector.x) != 0.0f) {
                    transform.localPosition = targetVector;
                }
            }
        }

        public virtual void OrderFix(int fixOrder, Vector3 fixVector)
        {
            listOrder -= fixOrder;
            targetVector -= fixVector;
        }
    }
}
