using UnityEngine;

namespace Rabbit
{
    public enum SubObjectType
    {
        Dummy, Eagle, Rock
    }
    
    public class SubObject : MonoBehaviour
    {
        [ReadOnly] public SubObjectType objectType;
        
        [SerializeField] protected float originHeight;
        
        protected virtual void InitializeHeight()
        {
            SetHeight(originHeight);
        }

        public virtual void SetHeight(float height)
        {
            var tempTransform = transform;
            var tempPosition = tempTransform.localPosition;
            
            tempPosition = new Vector3(tempPosition.x, height, tempPosition.z);
            tempTransform.localPosition = tempPosition;
        }

        public virtual void PlayAnimation(int order) { }
        public virtual void PlayAnimation(bool success) { }
    }
}