using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NoteView
{
    public class DeckLibrary : MonoBehaviour
    {
        public NoteLibrary noteLibrary;
        public PlayerProfile playerProfile;
        public GameObject buttonHolder;
        public GameObject buttonPrefab;
        public GameObject DeckLibraryGroup;

        private void OnEnable()
        {
            StartCoroutine(GenerateButtons());
        }

        public IEnumerator GenerateButtons()
        {
            //Clear Buttons
            buttonHolder.transform.Clear();

            foreach (Deck deck in playerProfile.decks)
            {
                Button button = Instantiate(buttonPrefab, buttonHolder.transform, false).GetComponent<Button>();
                button.GetComponentInChildren<TextMeshProUGUI>().text = deck.deckName;
                button.onClick.AddListener(delegate
                {
                    ShowNoteLibrary(deck);
                    DeckLibraryGroup.SetActive(false);
                });
            }
            yield return null; 
        }

        public void ShowNoteLibrary(Deck deck)
        {
            noteLibrary.deckToView = deck;
            noteLibrary.gameObject.SetActive(true);
        }
    }

}
