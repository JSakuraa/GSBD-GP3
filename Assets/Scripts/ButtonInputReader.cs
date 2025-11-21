using BattleDefinitions;
using MusicDefinitions;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ButtonInputReader : MonoBehaviour
{
    [Header("UI References")]
    public TMPro.TMP_Text displayText;
    public TMPro.TMP_Text turnIndicatorText;
    public TMPro.TMP_Text chordDisplayText;
    public TMPro.TMP_Text melodyDisplayText;
    public GameObject noteButtonPanel;
    public GameObject confirmButton;

    [Header("Battle State")]
    public Battlestate bs;

    // Track current turn and input state
    private enum TurnPhase { Player1Chord, Player1Melody, Player2Chord, Player2Melody, BattleResolution }
    private TurnPhase currentPhase = TurnPhase.Player1Chord;

    // Store selected notes
    private List<MusicalNote> currentChordNotes = new List<MusicalNote>();
    private List<MusicalNote> currentMelodyNotes = new List<MusicalNote>();

    // Store complete actions
    private Action player1Action;
    private Action player2Action;

    // Constants
    private const int CHORD_SIZE = 3;
    private const int MELODY_SIZE = 4;

    void Start()
    {
        UpdateDisplay();
        UpdateTurnIndicator();
    }

    // Called by note buttons (C, D, E, F, G, A, B)
    public void OnNoteButtonClicked(string noteName)
    {
        MusicalNote selectedNote = Translations.note_from_char(noteName[0]);

        if (currentPhase == TurnPhase.Player1Chord || currentPhase == TurnPhase.Player2Chord)
        {
            // Adding to chord
            if (currentChordNotes.Count < CHORD_SIZE)
            {
                currentChordNotes.Add(selectedNote);
                UpdateChordDisplay();

                // Auto-advance when chord is complete
                if (currentChordNotes.Count == CHORD_SIZE)
                {
                    confirmButton.SetActive(true);
                }
            }
        }
        else if (currentPhase == TurnPhase.Player1Melody || currentPhase == TurnPhase.Player2Melody)
        {
            // Adding to melody
            if (currentMelodyNotes.Count < MELODY_SIZE)
            {
                currentMelodyNotes.Add(selectedNote);
                UpdateMelodyDisplay();

                // Auto-advance when melody is complete
                if (currentMelodyNotes.Count == MELODY_SIZE)
                {
                    confirmButton.SetActive(true);
                }
            }
        }
    }

    // Called by Clear button
    public void OnClearButtonClicked()
    {
        if (currentPhase == TurnPhase.Player1Chord || currentPhase == TurnPhase.Player2Chord)
        {
            currentChordNotes.Clear();
            UpdateChordDisplay();
        }
        else if (currentPhase == TurnPhase.Player1Melody || currentPhase == TurnPhase.Player2Melody)
        {
            currentMelodyNotes.Clear();
            UpdateMelodyDisplay();
        }

        confirmButton.SetActive(false);
    }

    // Called by Confirm button
    public void OnConfirmButtonClicked()
    {
        switch (currentPhase)
        {
            case TurnPhase.Player1Chord:
                if (currentChordNotes.Count == CHORD_SIZE)
                {
                    // Move to melody phase, keep chord notes stored
                    currentPhase = TurnPhase.Player1Melody;
                    UpdateTurnIndicator();
                    confirmButton.SetActive(false);
                }
                break;

            case TurnPhase.Player1Melody:
                if (currentMelodyNotes.Count == MELODY_SIZE)
                {
                    // Create Player 1's action using the stored chord and melody
                    player1Action = new Action(
                        new Chord(currentChordNotes.ToArray()),
                        new Melody(currentMelodyNotes.ToArray()),
                        bs.player1
                    );

                    // Move to Player 2's turn, clear everything
                    currentPhase = TurnPhase.Player2Chord;
                    currentChordNotes.Clear();
                    currentMelodyNotes.Clear();
                    chordDisplayText.text = "Chord: (0/3)";
                    melodyDisplayText.text = "Melody: (0/4)";
                    UpdateTurnIndicator();
                    confirmButton.SetActive(false);
                }
                break;

            case TurnPhase.Player2Chord:
                if (currentChordNotes.Count == CHORD_SIZE)
                {
                    // Move to melody phase
                    currentPhase = TurnPhase.Player2Melody;
                    UpdateTurnIndicator();
                    confirmButton.SetActive(false);
                }
                break;

            case TurnPhase.Player2Melody:
                if (currentMelodyNotes.Count == MELODY_SIZE)
                {
                    // Create Player 2's action
                    player2Action = new Action(
                        new Chord(currentChordNotes.ToArray()),
                        new Melody(currentMelodyNotes.ToArray()),
                        bs.player2
                    );

                    // Execute battle
                    ResolveBattle();
                    confirmButton.SetActive(false);
                }
                break;
        }
    }

    private void ResolveBattle()
    {
        currentPhase = TurnPhase.BattleResolution;

        // Execute the battle
        Battlestate.battle(player1Action, player2Action);

        // Update health display
        UpdateDisplay();

        // Check for game over
        if (bs.player1.health <= 0 || bs.player2.health <= 0)
        {
            HandleGameOver();
        }
        else
        {
            // Start next turn after a delay
            Invoke("StartNextTurn", 2f);
        }
    }

    private void StartNextTurn()
    {
        currentPhase = TurnPhase.Player1Chord;
        currentChordNotes.Clear();
        currentMelodyNotes.Clear();
        chordDisplayText.text = "Chord: ";
        melodyDisplayText.text = "Melody: ";
        UpdateTurnIndicator();
    }

    private void HandleGameOver()
    {
        noteButtonPanel.SetActive(false);
        confirmButton.SetActive(false);

        if (bs.player1.health <= 0 && bs.player2.health <= 0)
        {
            turnIndicatorText.text = "Draw!";
        }
        else if (bs.player1.health <= 0)
        {
            turnIndicatorText.text = "Player 2 Wins!";
        }
        else
        {
            turnIndicatorText.text = "Player 1 Wins!";
        }
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"P1 Health: {bs.player1.health} | P2 Health: {bs.player2.health}";
        }
    }

    private void UpdateTurnIndicator()
    {
        if (turnIndicatorText == null) return;

        switch (currentPhase)
        {
            case TurnPhase.Player1Chord:
                turnIndicatorText.text = "Player 1: Select Chord (3 notes)";
                break;
            case TurnPhase.Player1Melody:
                turnIndicatorText.text = "Player 1: Select Melody (4 notes)";
                break;
            case TurnPhase.Player2Chord:
                turnIndicatorText.text = "Player 2: Select Chord (3 notes)";
                break;
            case TurnPhase.Player2Melody:
                turnIndicatorText.text = "Player 2: Select Melody (4 notes)";
                break;
            case TurnPhase.BattleResolution:
                turnIndicatorText.text = "Battle Resolution...";
                break;
        }
    }

    private void UpdateChordDisplay()
    {
        if (chordDisplayText == null) return;

        string display = "Chord: ";
        foreach (MusicalNote note in currentChordNotes)
        {
            display += note.ToString() + " ";
        }
        display += $"({currentChordNotes.Count}/{CHORD_SIZE})";

        chordDisplayText.text = display;
    }

    private void UpdateMelodyDisplay()
    {
        if (melodyDisplayText == null) return;

        string display = "Melody: ";
        foreach (MusicalNote note in currentMelodyNotes)
        {
            display += note.ToString() + " ";
        }
        display += $"({currentMelodyNotes.Count}/{MELODY_SIZE})";

        melodyDisplayText.text = display;
    }
}