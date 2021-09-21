using RotaryHeart.Lib.SerializableDictionary;

namespace Rabbit
{
    [System.Serializable]
    public class ObjectDictionary<T1, T2> : SerializableDictionaryBase<T1, T2> where T1 : System.Enum where T2 : UnityEngine.Object
    {
        
    }
}