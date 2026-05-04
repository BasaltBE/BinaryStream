using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream.Types;

public class BoolType : DataType<bool>
{
  public static new bool Read(BinaryStream stream)
  {
    // Read a byte from the stream and interpret it as a boolean value
    byte value = UInt8Type.Read(stream);
    return value != 0; // Any non-zero value is considered true
  }

  public static new void Write(BinaryStream stream, bool value)
  {
    // Write a byte to the stream representing the boolean value (1 for true, 0 for false)
    UInt8Type.Write(stream, (byte)(value ? 1 : 0));
  }
}