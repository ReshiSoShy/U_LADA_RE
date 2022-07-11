using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ReshiSoShy.Main.Data;
namespace ReshiSoShy.Main.Dialogues
{
    public class DialogueController : MonoBehaviour
    {
        // Call this function from player interaction
        // EnqueueNewDialogue(...);
        // If we are not showing anything, do the queue
        // If we are showing something already, wait for player input
        // Take the Dialogue from the queue
        // Check its dialogue type
        // If planeText, grab text, audio, triggers and next action, solve that.
        // If questionForPlayer, grab the same and solve next action with custom QuestionAction();
        // If if plaintext just use solvenextaction(string) target:concept format 
        // Get target by name, this class has to contain all characters that can speak
        // and set the listener as the player ofc
        // Solve that.
    }
}