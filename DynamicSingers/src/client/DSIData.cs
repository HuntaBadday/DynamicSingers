using LogicWorld.ClientCode;

namespace DynamicSingers;
public interface DSIData : Singer.IData {
    byte[] noteOnList { get; set; }
    byte dataUpdate { get; set; }
}