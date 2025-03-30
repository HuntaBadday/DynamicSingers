using System.Timers;
using LogicWorld.Server.Circuitry;
using Timer = System.Timers.Timer;

namespace DynamicSingers {
    public class MultiDSinger : LogicComponent<DSIData> {
        const int NOTEIN = 0;
        const int STATE = 7;
        const int WRITE = 8;
        const int RESET = 9;
        const int OFFSET = 10;
        
        protected override void SetDataDefaultValues() {
            Data.Note = 60;
            Data.Velocity = 100;
            Data.InstrumentTextID = "Tung.AcousticGrandPiano";
            Data.noteOnList = new byte[128];
        }
        Timer updateTimer;
        byte[] noteOnList = new byte[128];
        bool isDirty = false;
        bool isReset = false;

        protected override void Initialize() {
            updateTimer = new Timer(30);
            updateTimer.Elapsed += OnTimerElapsed;
            updateTimer.AutoReset = true;
            updateTimer.Start();
        }

        public void OnTimerElapsed(object source, ElapsedEventArgs args) {
            if (!isDirty) return;
            isDirty = false;
            for (int i = 0; i < 128; i++) {
                Data.noteOnList[i] = noteOnList[i];
            }
            Data.dataUpdate = (byte)(isReset ? 2:1);
            isReset = false;
        }
        
        protected override void DoLogicUpdate() {
            if (Inputs[RESET].On) {
                for (int i = 0; i < 128; i++)
                    noteOnList[i] = 0;
                isDirty = true;
                isReset = true;
                return;
            }
            if (Inputs[WRITE].On) {
                int index = (ReadNoteIn()+ReadOffset()) & 0x7f;
                noteOnList[index] = (byte)(Inputs[STATE].On ? 1:0);
                isDirty = true;
            }
        }
        
        byte ReadNoteIn() {
            byte output = 0;
            for (int i = 0; i < 7; i++) {
                output <<= 1;
                output |= (byte)(Inputs[NOTEIN+i].On ? 1:0);
            }
            return output;
        }
        
        byte ReadOffset() {
            byte output = 0;
            for (int i = 6; i >= 0; i--) {
                output <<= 1;
                output |= (byte)(Inputs[OFFSET+i].On ? 1:0);
            }
            return output;
        }
    }
}