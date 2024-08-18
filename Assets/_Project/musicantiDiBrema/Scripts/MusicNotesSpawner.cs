using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNotesSpawner : MonoBehaviour
{
    [SerializeField]
    private float timer;

    [SerializeField]
    private float noteSpawnerInterval = 0.5f;

   [SerializeField]
   private MusicNote musicNotePrefab;

   private Queue<MusicNote> musicNotesPool = new Queue<MusicNote>();

    [SerializeField]
    private Vector3 NoteInitialPosition; // posizione in cui far comparire le note


    private void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= this.noteSpawnerInterval)
        {
            this.timer -= this.noteSpawnerInterval;

            MusicNote note = GetMusicNote();
            note.Setup(this);
            note.transform.position = this.transform.position; // + new Vector3(0, 0.1f, 0); // da sistemare

        }
    }

    public MusicNote GetMusicNote()
    {
        
            if (this.musicNotesPool.Count > 0)
            {
                MusicNote musicNote = this.musicNotesPool.Dequeue();
                musicNote.gameObject.SetActive(true);
                
                return musicNote;
            }

            return Instantiate(this.musicNotePrefab, this.transform);
            
    }

    public void ReturnNoteToPool(MusicNote note)
    {
        note.gameObject.SetActive(false);
        this.musicNotesPool.Enqueue(note);
    }


}
