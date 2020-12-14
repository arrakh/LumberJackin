using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoteView
{
    [System.Obsolete("Inefficient due to UI chunking. Use the new one instead")]
    public class NoteLibrary_Obsolete : MonoBehaviour
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
                NoteViewScript nv = Instantiate(noteViewPrefab, noteHolder.transform, false).GetComponent<NoteViewScript>();
                nv.Initialize(deckToView, note);
                yield return new WaitForFixedUpdate();
            }

        }
    }
}

