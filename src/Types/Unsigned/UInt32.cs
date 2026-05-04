using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Unsigned;

public class UInt32Type : DataType<uint>
{
  public static new uint Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new uint Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (4 bytes for uint32)
    if (stream.offset + 4 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Prepare a value to hold the result
    uint value;

    // Read the bytes based on the specified endianness
    if (endian == Endianness.Big)
    {
      value = (uint)((stream.buffer[stream.offset] << 24) |
                     (stream.buffer[stream.offset + 1] << 16) |
                     (stream.buffer[stream.offset + 2] << 8) |
                     stream.buffer[stream.offset + 3]);
    }
    else // Little Endian
    {
      value = (uint)(stream.buffer[stream.offset] |
                     (stream.buffer[stream.offset + 1] << 8) |
                     (stream.buffer[stream.offset + 2] << 16) |
                     (stream.buffer[stream.offset + 3] << 24));
    }

    // Advance the offset by 4 bytes after reading
    stream.offset += 4;

    // Return the read value as uint
    return value;
  }

  public static new void Write(BinaryStream stream, uint value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, uint value, Endianness endian = Endianness.Big)
  {
    // Ensure the buffer can accommodate the new 4 bytes
    if (stream.offset + 4 > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + 4);
    }

    // Write the bytes to the buffer based on the specified endianness
    if (endian == Endianness.Big)
    {
      stream.buffer[stream.offset] = (byte)(value >> 24); // Highest byte
      stream.buffer[stream.offset + 1] = (byte)((value >> 16) & 0xFF); // High byte
      stream.buffer[stream.offset + 2] = (byte)((value >> 8) & 0xFF); // Low byte
      stream.buffer[stream.offset + 3] = (byte)(value & 0xFF); // Lowest byte
    }
    else // Little Endian
    {
      stream.buffer[stream.offset] = (byte)(value & 0xFF); // Lowest byte
      stream.buffer[stream.offset + 1] = (byte)((value >> 8) & 0xFF); // Low byte
      stream.buffer[stream.offset + 2] = (byte)((value >> 16) & 0xFF); // High byte
      stream.buffer[stream.offset + 3] = (byte)(value >> 24); // Highest byte
    }

    // Advance the offset by 4 bytes after writing
    stream.offset += 4;
  }
}