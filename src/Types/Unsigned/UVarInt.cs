namespace Basalt.BinaryStream.Types.Unsigned;

public class UVarIntType : DataType<uint>
{
  public static new uint Read(BinaryStream stream)
  {
    // Prepare to read a variable-length integer (VarInt) from the stream
    uint value = 0;
    int shift = 0;

    while (true)
    {
      // Read a byte from the stream
      byte b = UInt8Type.Read(stream);

      // Add the lower 7 bits of the byte to the value, shifted by the current shift amount
      value |= (uint)(b & 0x7F) << shift;

      // If the continuation bit (the highest bit) is not set, we are done
      if ((b & 0x80) == 0) break;

      // Move to the next 7 bits
      shift += 7;

      // If we've shifted more than 28 bits, it means the VarInt is too large for a uint
      if (shift >= 35) throw new Exception("VarInt is too large to fit in a uint");
    }

    // Return the decoded VarInt value
    return value;
  }

  public static new void Write(BinaryStream stream, uint value)
  {
    // Continue writing bytes until the value is fully encoded
    while (value >= 0x80)
    {
      // Write the lower 7 bits of the value with the continuation bit set
      UInt8Type.Write(stream, (byte)((value & 0x7F) | 0x80));

      // Shift the value right by 7 bits to process the next part
      value >>= 7;
    }

    // Write the final byte (the last 7 bits without the continuation bit)
    UInt8Type.Write(stream, (byte)value);
  }
}
