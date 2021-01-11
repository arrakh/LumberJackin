using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoteView
{
    public class NoteSoundField : NoteField
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button playButton;
        public override void Initialize(string fieldName, object fieldContent)
        {
            base.Initialize(fieldName, fieldContent);
            playButton.onClick.AddListener(delegate { audioSource.PlayOneShot((AudioClip)fieldContent); });
        }
    }
}
