using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterSoundAttributes : MonoBehaviour
{
    [Header("Sound Identifiers")]
    [Tooltip("Name of this monster's sound theme")]
    public string soundThemeName = "Monster Sound";
    
    [Header("Pitch Settings")]
    [Range(0.1f, 3f)]
    [Tooltip("Base pitch of the monster's sounds")]
    public float basePitch = 1f;
    
    [Range(0f, 1f)]
    [Tooltip("Random pitch variation (+/- range)")]
    public float pitchVariation = 0.1f;
    
    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [Tooltip("Base volume of the monster's sounds")]
    public float baseVolume = 0.8f;
    
    [Range(0f, 1f)]
    [Tooltip("Random volume variation (+/- range)")]
    public float volumeVariation = 0.05f;
    
    [Header("Timing Settings")]
    [Tooltip("Delay before sound plays (in seconds)")]
    public float soundDelay = 0f;
    
    [Range(0.1f, 5f)]
    [Tooltip("Duration to fade in sound")]
    public float fadeInDuration = 0.1f;
    
    [Header("Audio Clips")]
    [Tooltip("Array of sound clips this monster can make")]
    public AudioClip[] monsterSounds;
    
    [Header("Combat Sound Settings")]
    [Tooltip("Sound played when attacking")]
    public AudioClip attackSound;
    
    [Tooltip("Sound played when taking damage")]
    public AudioClip hurtSound;
    
    [Tooltip("Sound played when defeated")]
    public AudioClip defeatSound;
    
    [Header("Musical Properties")]
    [Tooltip("Musical note/key (C, D, E, F, G, A, B)")]
    public MusicalNote baseNote = MusicalNote.C;
    
    [Tooltip("Octave of the base note (1-8)")]
    [Range(1, 8)]
    public int octave = 4;
    
    [Tooltip("Sound rhythm pattern in beats")]
    public float[] rhythmPattern = new float[] { 1f, 0.5f, 0.5f, 1f };
    
    [Header("Audio Effects")]
    [Range(0f, 1f)]
    [Tooltip("Reverb amount (requires AudioReverbFilter)")]
    public float reverbAmount = 0f;
    
    [Range(0f, 1f)]
    [Tooltip("Echo amount (requires AudioEchoFilter)")]
    public float echoAmount = 0f;
    
    // Private reference to AudioSource
    private AudioSource audioSource;
    
    public enum MusicalNote
    {
        C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, A, ASharp, B
    }
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ApplyBaseSettings();
    }
    
    // Apply the base sound settings to the AudioSource
    public void ApplyBaseSettings()
    {
        if (audioSource != null)
        {
            audioSource.pitch = basePitch;
            audioSource.volume = baseVolume;
        }
    }
    
    // Play a random sound from the monster's sound array
    public void PlayRandomSound()
    {
        if (monsterSounds.Length > 0 && audioSource != null)
        {
            AudioClip clip = monsterSounds[Random.Range(0, monsterSounds.Length)];
            PlaySoundWithVariation(clip);
        }
    }
    
    // Play a specific sound with pitch and volume variation
    public void PlaySoundWithVariation(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            float pitch = basePitch + Random.Range(-pitchVariation, pitchVariation);
            float volume = baseVolume + Random.Range(-volumeVariation, volumeVariation);
            
            audioSource.pitch = pitch;
            audioSource.volume = Mathf.Clamp01(volume);
            
            if (soundDelay > 0)
            {
                audioSource.PlayDelayed(soundDelay);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
    
    // Play combat-specific sounds
    public void PlayAttackSound()
    {
        if (attackSound != null)
        {
            PlaySoundWithVariation(attackSound);
        }
    }
    
    public void PlayHurtSound()
    {
        if (hurtSound != null)
        {
            PlaySoundWithVariation(hurtSound);
        }
    }
    
    public void PlayDefeatSound()
    {
        if (defeatSound != null)
        {
            PlaySoundWithVariation(defeatSound);
        }
    }
    
    // Get the frequency for the monster's musical note
    public float GetNoteFrequency()
    {
        // A4 = 440 Hz is the reference
        int semitonesFromA4 = ((octave - 4) * 12) + ((int)baseNote - (int)MusicalNote.A);
        float frequency = 440f * Mathf.Pow(2f, semitonesFromA4 / 12f);
        return frequency;
    }
    
    // Convert frequency to pitch multiplier
    public float GetPitchFromNote()
    {
        float noteFrequency = GetNoteFrequency();
        // Assuming base sound is at 440 Hz (A4)
        return noteFrequency / 440f;
    }
}