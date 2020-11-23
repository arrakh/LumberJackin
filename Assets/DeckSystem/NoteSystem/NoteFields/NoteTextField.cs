using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NoteView
{
    public class NoteTextField : NoteField
    {
        [SerializeField] private TextMeshProUGUI noteText;

        public override void Initialize(string fieldName, object fieldContent)
        {
            base.Initialize(fieldName, fieldContent);
            
            //TODO: Throw Exception for casting error
            noteText.text = (string)fieldContent;
        }
    }
}
