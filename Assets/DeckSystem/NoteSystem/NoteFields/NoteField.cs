using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NoteView
{
    public class NoteField : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI fieldTypeText;

        public virtual void Initialize(string fieldName, object fieldContent) 
        {
            fieldTypeText.text += fieldName;
        }
    }

}
