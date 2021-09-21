using FMODUnity;

namespace Rabbit
{
    public class SFXManager<T> : SoundManager<T>
    {
        public void PlayOneShot(T id)
        {
            if (eventDic.ContainsKey(id)) {
                RuntimeManager.PlayOneShot(eventDic[id].soundEvent);
            }
        }
    }
}