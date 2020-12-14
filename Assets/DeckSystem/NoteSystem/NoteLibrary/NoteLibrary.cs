using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoteView
{
    public class NoteLibrary : MonoBehaviour
    {
        [SerializeField] private NoteViewScript noteView;

        public Deck deckToView;

        private int index = 0;

        private void OnEnable()
        {
            noteView.deck = deckToView;
            SetNoteView(0);
        }

        public void CycleIndex(int offset)
        {
            index += offset;

            if ( index < 0 )
            {
                index = 0;
            } 
            else if (index >= deckToView.notes.Count - 1)
            {
                index = deckToView.notes.Count;
            }

            SetNoteView(index);
        }

        public void SetNoteView(int index)
        {
            noteView.Initialize(deckToView, deckToView.notes[index]);
        }

    }
}

