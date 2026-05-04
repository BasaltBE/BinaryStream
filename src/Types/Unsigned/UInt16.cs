using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Unsigned;

public class UInt16Type : DataType<ushort>
{
  public static new ushort Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new ushort Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (2 bytes for uint16)
    if (stream.offset + 2 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Prepare a value to hold the result
    ushort value = 0;

    // Read the bytes based on the specified endianness
    if (endian == Endianness.Big)
    {
      value = (ushort)((stream.buffer[stream.offset] << 8) | stream.buffer[stream.offset + 1]);
    }
    else // Little Endian
    {
      value = (ushort)(stream.buffer[stream.offset] | (stream.buffer[stream.offset + 1] << 8));
    }

    // Advance the offset by 2 bytes after reading
    stream.offset += 2;

    // Return the read value as ushort (since UInt16 is unsigned, we can directly return the ushort value)
    return value;
  }

  public static new void Write(BinaryStream stream, ushort value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, ushort value, Endianness endian = Endianness.Big)
  {
    // Ensure the buffer can accommodate the new 2 bytes
    if (stream.offset + 2 > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + 2);
    }

    // Write the bytes to the buffer based on the specified endianness
    if (endian == Endianness.Big)
    {
      stream.buffer[stream.offset] = (byte)(value >> 8); // High byte
      stream.buffer[stream.offset + 1] = (byte)(value & 0xFF); // Low byte
    }
    else // Little Endian
    {
      stream.buffer[stream.offset] = (byte)(value & 0xFF); // Low byte
      stream.buffer[stream.offset + 1] = (byte)(value >> 8); // High byte
    }

    // Advance the offset by 2 bytes after writing
    stream.offset += 2;
  }
}