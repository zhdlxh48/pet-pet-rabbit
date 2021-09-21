namespace Rabbit
{
    public interface IHitProcess
    {
        void NoteHitCorrect(Note note);
        void NoteHitFailed(Note note);
    }
}