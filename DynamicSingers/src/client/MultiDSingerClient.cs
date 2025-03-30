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
    public class MultiDSingerClient : DSinger {
        protected override void FrameUpdate() {
            if (Data.dataUpdate == 2) {
                StopPlaying();
                Data.dataUpdate = 0;
                return;
            }
            if (Data.dataUpdate == 1) {
                Data.dataUpdate = 0;
                for (int i = 0; i < 128; i++) {
                    if (Data.noteOnList[i] == 1) {
                        StartPlayingNote((byte)(i));
                    } else {
                        StopPlayingNote((byte)(i));
                    }
                }
            }
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
