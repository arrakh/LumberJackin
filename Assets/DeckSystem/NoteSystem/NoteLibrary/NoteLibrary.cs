using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoteView
{
    public class NoteLibrary : MonoBehaviour
    {
        [SerializeField] private GameObject noteViewPrefab;
        [SerializeField] private GameObject noteHolder;

        public Deck deckToView;

        private void OnEnable()
        {
            StartCoroutine(GenerateNotes());
        }

        public IEnumerator GenerateNotes()
        {
            //Clear children
            noteHolder.transform.Clear();

            //For each notes, instantiate note view prefab
            foreach (Note note in deckToView.notes)
            {
                NoteView nv = Instantiate(noteViewPrefab, noteHolder.transform, false).GetComponent<NoteView>();
                nv.Initialize(deckToView, note);
                yield return new WaitForFixedUpdate();
            }

        }
    }
}

