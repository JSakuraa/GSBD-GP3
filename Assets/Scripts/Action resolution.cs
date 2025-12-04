using BattleDefinitions;
using MusicDefinitions;
using UnityEngine;

public class Actionresolution : MonoBehaviour
{
    //fixing note effect to match mapping
    public static NoteEffect[] note_effects = {
        NoteEffect.Heal,
        NoteEffect.LifeSteal,
        NoteEffect.LifeSteal,
        NoteEffect.Heal,
        NoteEffect.Heal,
        NoteEffect.Damage,
        NoteEffect.Damage
    };

    //fixed matrix to match the note enum in definitions
    static double[,] base_resolution_matrix = {
        // C   D   E   F   G   A   B
        {  0, -1,  0, -1,  1,  0,  1}, //C 
        {  1,  0,  1,  0, -1,  1, -1}, //D 
        {  0, -1,  0, -1,  1,  0,  1}, //E 
        {  1,  0,  1,  0, -1,  1, -1}, //F 
        { -1,  1, -1,  1,  0, -1,  0}, //G 
        {  0, -1,  0, -1,  1,  0,  1}, //A 
        { -1,  1, -1,  1,  0, -1,  0}  //B 
    };


    public static double[] resolve_actions(Action a1, Action a2)
    {
        // Use base resolution matrix directly (no chord modifications)
        double[,] temp_res_matrix = base_resolution_matrix.Clone() as double[,];

        Debug.Log($"Using base resolution matrix (no chord effects):\n{GenerateArrayDefinitionString(temp_res_matrix)}");

        double[] outputs = resolve_melodies(a1.melody, a2.melody, temp_res_matrix);

        return outputs;
    }

    public static double resolve_notes(MusicalNote n1, MusicalNote n2, double[,] matrix)
    {
        return matrix[(int)n1, (int)n2];
    }

    public static double[] resolve_melodies(Melody m1, Melody m2, double[,] matrix)
    {
        double[] outcomes = new double[m1.notes.Length];
        for (int i = 0; i < m1.notes.Length; i++)
        {
            outcomes[i] = matrix[(int)m2.notes[i], (int)m1.notes[i]];
        }
        return outcomes;
    }

    public static string GenerateArrayDefinitionString<T>(T[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        var sb = new System.Text.StringBuilder();

        sb.Append("{");

        for (int i = 0; i < rows; i++)
        {
            sb.Append("{");

            for (int j = 0; j < cols; j++)
            {
                sb.Append(matrix[i, j]);
                if (j < cols - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("}");

            if (i < rows - 1)
            {
                sb.Append(",\n ");
            }
        }

        sb.Append("}");

        return sb.ToString();
    }

    public static string GenerateArrayDefinitionString1D<T>(T[] array)
    {
        if (array == null)
        {
            return "null";
        }

        string joinedElements = System.String.Join(", ", array);
        return $"{{{joinedElements}}}";
    }

    [ContextMenu("Perform a quick functionality test")]
    void quicktest()
    {
        Debug.Log("functionality test");

        // Simple test without chords
        Melody m1 = new Melody(MusicalNote.C, MusicalNote.E, MusicalNote.G, MusicalNote.A);
        Melody m2 = new Melody(MusicalNote.D, MusicalNote.F, MusicalNote.A, MusicalNote.C);

        Action a1 = new Action(m1);
        Action a2 = new Action(m2);

        Debug.Log($"Action 1: {a1}");
        Debug.Log($"Action 2: {a2}");

        double[] results = resolve_actions(a1, a2);
        Debug.Log($"Battle results: {GenerateArrayDefinitionString1D(results)}");
    }
}