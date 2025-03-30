namespace DynamicSingers;
public interface DSIData {
    byte Note { get; set; }
    int Velocity { get; set; }
    string InstrumentTextID { get; set; }
    byte[] noteOnList { get; set; }
    byte dataUpdate { get; set; }
}