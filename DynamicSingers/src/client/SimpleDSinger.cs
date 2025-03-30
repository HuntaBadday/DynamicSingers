using CSharpSynth.Synthesis;
using JimmysUnityUtilities;
using LogicWorld.Audio;
using LogicWorld.ClientCode.Decorations;
using LogicWorld.Interfaces;
using LogicWorld.References;
using LogicWorld.Rendering.Chunks;
using LogicWorld.Rendering.Components;
using System.Collections;
using LogicWorld.ClientCode;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DynamicSingers {
    public class SimpleDSinger : DSinger {
        private bool currentlyPlaying;
        
        private byte LastPlayedNote = 0;
        
        protected override void FrameUpdate() {
            bool pinState = GetInputState(7);
            byte inNote = ReadNoteIn();
            if (pinState && !currentlyPlaying || pinState && LastPlayedNote != inNote) {
                if (LastPlayedNote != inNote) {
                    StopPlaying();
                }
                StartPlayingNote(inNote);
                currentlyPlaying = true;
            }
            if (!pinState && currentlyPlaying) {
                StopPlaying();
                currentlyPlaying = false;
            }
            LastPlayedNote = inNote;
        }
        
        protected override void DataUpdate() => QueueFrameUpdate();
        
        private byte ReadNoteIn() {
            byte output = 0;
            for (int i = 0; i < 7; i++) {
                output <<= 1;
                output |= (byte)(GetInputState(i) ? 1:0);
            }
            return output;
        }
    }
}
