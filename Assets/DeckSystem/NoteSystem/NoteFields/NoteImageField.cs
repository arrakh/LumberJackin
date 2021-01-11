using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoteView
{
    public class NoteImageField : NoteField
    {
        [SerializeField] private Image image;
        public override void Initialize(string fieldName, object fieldContent)
        {
            base.Initialize(fieldName, fieldContent);
            Debug.Log(fieldContent == null);
            image.sprite = (Sprite)fieldContent;
        }
    }
}
