namespace Basalt.BinaryStream.Types.Unsigned;

public class UVarLongType : DataType<ulong>
{
  public static new ulong Read(BinaryStream stream)
  {
    // Prepare to read the unsigned variable-length long integer from the stream
    ulong value = 0;
    int shift = 0;

    while (true)
    {
      // Read a byte from the stream
      byte b = UInt8Type.Read(stream);

      // Add the lower 7 bits of the byte to the value, shifted by the current shift amount
      value |= (ulong)(b & 0x7F) << shift;

      // If the most significant bit of the byte is not set, we have reached the end of the variable-length integer
      if ((b & 0x80) == 0) break;

      // Increment the shift amount for the next byte
      shift += 7;
    }

    // Return the decoded unsigned variable-length long integer value
    return value;
  }

  public static new void Write(BinaryStream stream, ulong value)
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
