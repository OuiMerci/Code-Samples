using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMatchGameplay : SongGameplay
{
    #region Fields
    public enum Note // Based on how many beats the note lasts
    {
        wait,
        one,
        two,
        three,
        four
    }

    private Queue<Note> _enemyNotes;
    private Note _currentNote;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _enemyNotes = new Queue<Note>();
    }

    private void SetNotes(Note[] notes)
    {
        for(int i = 0; i < notes.Length; i++)
        {
            _enemyNotes.Enqueue(notes[i]);
            Debug.Log("enqueue : " + notes[i]);
        }
        for (int i = 0; i < notes.Length; i++)
        {
            Note note = _enemyNotes.Dequeue();
            Debug.Log("Dequeue : " + note);
        }
    }

    private void SetNextNote()
    {
        _currentNote = _enemyNotes.Dequeue();
        Debug.Log("dequeued : " + _currentNote);
    }

    public void HandlePlayedNote(Note note)
    {
        Debug.Log("Handle played note : " + note);
        if(_currentNote == note)
        {
            Debug.Log("Correct note ! ");
        }
    }
    /// <summary>
    /// ////////////////////// FAIRE ATTENTION A LA CONVERSION ARRAY >> QUEUE
    /// </summary>
    /// <param name="enemy"></param>
    public override void StartGameplay(Enemy enemy)
    {
        RhythmMatchEnemy rmEnemy = enemy.GetComponent<RhythmMatchEnemy>();

        if(rmEnemy == null)
        {
            Debug.LogError("This enemy doesn't have a RhythmMatchEnemy script !!");
            return;
        }

        SetNotes(rmEnemy.Notes);
    }
    #endregion
}
