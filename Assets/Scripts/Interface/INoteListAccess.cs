using System.Collections.Generic;

namespace Rabbit
{
    public interface INoteListAccess
    {
        List<Note> NoteList { get; set; }
    }
}