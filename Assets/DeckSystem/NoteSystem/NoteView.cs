using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoteView
{
    public class NoteView : MonoBehaviour
    {
        public Deck deck;
        public Note note;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject noteFieldPrefab_Text;
        [SerializeField] private GameObject noteFieldPrefab_Image;
        [SerializeField] private GameObject noteFieldPrefab_Sound;
        [SerializeField] private GameObject noteFieldPrefab_Cloze;
        [SerializeField] private Button button;

        //private void Start()
        //{
        //    GenerateNoteFields();
        //}

        public void Initialize(Deck deckReference, Note noteToView, UnityAction onClickEvent)
        {
            //Set references
            deck = deckReference;
            note = noteToView;

            //Add button OnClick event; 
            button.onClick.RemoveAllListeners();
            if(onClickEvent != null) button.onClick.AddListener(onClickEvent);

            //Generate Fields
            GenerateNoteFields();
        }

        void GenerateNoteFields()
        {
            //Clear Children
            contentHolder.transform.Clear();

            for (int i = 0; i < deck.fieldNames.Count; i++)
            {
                NoteField nf = Instantiate(getFieldPrefab(deck.fieldTypes[i]), contentHolder.transform, false).GetComponent<NoteField>();
                nf.Initialize(deck.fieldNames[i], note.Fields[i]);
            }

        }

        GameObject getFieldPrefab(FieldType type)
        {
            switch (type)
            {
                case FieldType.Cloze:
                    return noteFieldPrefab_Cloze;
                case FieldType.Image:
                    return noteFieldPrefab_Image;
                case FieldType.Sound:
                    return noteFieldPrefab_Sound;
                case FieldType.Text:
                    return noteFieldPrefab_Text;
                default:
                    return noteFieldPrefab_Text;
            }
        }
    }

}

