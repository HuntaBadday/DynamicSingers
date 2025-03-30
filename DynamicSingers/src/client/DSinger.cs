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
    public class DSinger : ComponentClientCode<DSIData> {
        protected Synthesizer Synthesizer = new Synthesizer();
        protected MusicComponentSoundPlayer SoundPlayer;
        
        bool[] isPlaying = new bool[128];
        
        protected override void SetDataDefaultValues() {
            Data.Note = 60;
            Data.Velocity = 100;
            Data.InstrumentTextID = "Tung.AcousticGrandPiano";
            Data.noteOnList = new byte[128];
        }
        
        protected override IDecoration[] GenerateDecorations(Transform parentToCreateDecorationsUnder) {
            GameObject gameObject1 = Object.Instantiate<GameObject>(Prefabs.ComponentDecorations.MusicComponentSoundPlayer, parentToCreateDecorationsUnder);
            SoundPlayer = gameObject1.GetComponent<MusicComponentSoundPlayer>();
            SoundPlayer.SetSynth(this.Synthesizer);
            return (IDecoration[]) new Decoration[1] {
                new Decoration() {
                        LocalPosition = new Vector3(0.0f, 0.8f, 0.0f),
                        DecorationObject = gameObject1,
                        ShouldBeOutlined = false
                },
            };
        }
        
        protected void StartPlayingNote(byte note) {
            if (note >= 128) return;
            if (isPlaying[note]) return;
            isPlaying[note] = true;
            SoundPlayer.EnableAudio();
            Synthesizer.SoundOn(Data.InstrumentTextID, note, Data.Velocity);
            MusicPlayer.MusicComponentPlayed();
        }
        
        protected void StopPlayingNote(byte note) {
            if (note >= 128) return;
            isPlaying[note] = false;
            Synthesizer.SoundOff(note);
        }
        
        protected void StopPlaying() {
            for (int i = 0; i < 128; i++)
                isPlaying[i] = false;
            SoundPlayer.DisableAudio();
            Synthesizer.SoundOffAll();
        }
        
        public void TestSound(float seconds = 0.5f) {}
    }
}
