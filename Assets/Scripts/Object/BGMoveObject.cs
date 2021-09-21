using UnityEngine;

namespace Rabbit
{
    public class BGMoveObject : MonoBehaviour
    {
        [Header("움직이는 Object")]
        [SerializeField] private GameObject moveObject;

        public void MoveLeft()
        {
            moveObject.transform.Translate(Vector3.left, Space.Self);
        }
    }
}