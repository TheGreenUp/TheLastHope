using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
 public int numberOfNotes = 0;
    public int getNumberOfNotes()
    {
        return numberOfNotes;
    }
    public void addNote()
    {
        numberOfNotes++;
    }

}
