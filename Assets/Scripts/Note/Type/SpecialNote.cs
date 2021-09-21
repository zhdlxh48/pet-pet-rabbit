using UnityEngine;

namespace Rabbit
{
    public class SpecialNote : Note
    {
        public SubObject subObject;

        public override void OrderFix(int fixOrder, Vector3 fixVector)
        {
            subObject.PlayAnimation(listOrder -= fixOrder);
            targetVector -= fixVector;
        }
    }
}