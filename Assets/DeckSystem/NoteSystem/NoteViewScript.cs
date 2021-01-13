using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoteView
{
    public class NoteViewScript : MonoBehaviour
    {
        public Deck deck;
        public Note note;
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject noteFieldPrefab_Text;
        [SerializeField] private GameObject noteFieldPrefab_Image;
        [SerializeField] private GameObject noteFieldPrefab_Sound;
        [SerializeField] private GameObject noteFieldPrefab_Cloze;

        //private void Start()
        //{
        //    GenerateNoteFields();
        //}

        public void Initialize(Deck deckReference, Note noteToView)
        {
            //Set references
            deck = deckReference;
            note = noteToView;

            //Generate Fields
            GenerateNoteFields();
        }

        void GenerateNoteFields()
        {
            //Clear Children
            contentHolder.transform.Clear();

            for (int i = 0; i < deck.fieldNames.Count; i++)
            {
                NoteField nf = Instantiate(GetFieldPrefab(deck.fieldTypes[i]), contentHolder.transform, false).GetComponent<NoteField>();
                nf.Initialize(deck.fieldNames[i], GetFieldObject(deck.fieldTypes[i], note.Fields[i]));
            }

        }

        GameObject GetFieldPrefab(FieldType type)
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

        object GetFieldObject(FieldType type, string value)
        {
            switch (type)
            {
                case FieldType.Text:
                    return value;
                case FieldType.Cloze:
                    return value;
                case FieldType.Image:
                    //Debug.Log(value);
                    return Resources.Load<Sprite>(deck.deckMediaPath + "/" + value);
                case FieldType.Sound:
                    //Debug.Log(value);
                    return Resources.Load<AudioClip>(deck.deckMediaPath + "/" + value);
            }
            return null;
        }
    }

}

