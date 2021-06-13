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
        public ToggleGroup toggleGroup;

        private void OnEnable()
        {
            StartCoroutine(GenerateButtons());
        }

        public IEnumerator GenerateButtons()
        {
            //Clear Buttons
            buttonHolder.transform.Clear();

            for (int i = 0; i < playerProfile.decks.Count; i++)
            {
                Deck deck = playerProfile.decks[i];
                var button = Instantiate(buttonPrefab, buttonHolder.transform, false).GetComponent<DeckLibraryButton>();

                button.Initialize(i, deck.deckName, deck.fieldNames, playerProfile, toggleGroup,
                    delegate 
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
