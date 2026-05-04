using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Unsigned;

public class UInt64Type : DataType<ulong>
{
  public static new ulong Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new ulong Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (8 bytes for uint64)
    if (stream.offset + 8 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Prepare a value to hold the result
    ulong value;

    // Read the bytes based on the specified endianness
    if (endian == Endianness.Big)
    {
      value = ((ulong)stream.buffer[stream.offset] << 56) |
              ((ulong)stream.buffer[stream.offset + 1] << 48) |
              ((ulong)stream.buffer[stream.offset + 2] << 40) |
              ((ulong)stream.buffer[stream.offset + 3] << 32) |
              ((ulong)stream.buffer[stream.offset + 4] << 24) |
              ((ulong)stream.buffer[stream.offset + 5] << 16) |
              ((ulong)stream.buffer[stream.offset + 6] << 8) |
              stream.buffer[stream.offset + 7];
    }
    else // Little Endian
    {
      value = stream.buffer[stream.offset] |
              ((ulong)stream.buffer[stream.offset + 1] << 8) |
              ((ulong)stream.buffer[stream.offset + 2] << 16) |
              ((ulong)stream.buffer[stream.offset + 3] << 24) |
              ((ulong)stream.buffer[stream.offset + 4] << 32) |
              ((ulong)stream.buffer[stream.offset + 5] << 40) |
              ((ulong)stream.buffer[stream.offset + 6] << 48) |
              ((ulong)stream.buffer[stream.offset + 7] << 56);
    }

    // Advance the offset by 8 bytes after reading
    stream.offset += 8;

    // Return the read value as ulong
    return value;
  }

  public static new void Write(BinaryStream stream, ulong value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, ulong value, Endianness endian = Endianness.Big)
  {
    // Ensure the buffer can accommodate the new 8 bytes
    if (stream.offset + 8 > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + 8);
    }

    // Write the bytes to the buffer based on the specified endianness
    if (endian == Endianness.Big)
    {
      stream.buffer[stream.offset] = (byte)(value >> 56); // Highest byte
      stream.buffer[stream.offset + 1] = (byte)((value >> 48) & 0xFF);
      stream.buffer[stream.offset + 2] = (byte)((value >> 40) & 0xFF);
      stream.buffer[stream.offset + 3] = (byte)((value >> 32) & 0xFF);
      stream.buffer[stream.offset + 4] = (byte)((value >> 24) & 0xFF);
      stream.buffer[stream.offset + 5] = (byte)((value >> 16) & 0xFF);
      stream.buffer[stream.offset + 6] = (byte)((value >> 8) & 0xFF);
      stream.buffer[stream.offset + 7] = (byte)(value & 0xFF); // Lowest byte
    }
    else // Little Endian
    {
      stream.buffer[stream.offset] = (byte)(value & 0xFF); // Lowest byte
      stream.buffer[stream.offset + 1] = (byte)((value >> 8) & 0xFF);
      stream.buffer[stream.offset + 2] = (byte)((value >> 16) & 0xFF);
      stream.buffer[stream.offset + 3] = (byte)((value >> 24) & 0xFF);
      stream.buffer[stream.offset + 4] = (byte)((value >> 32) & 0xFF);
      stream.buffer[stream.offset + 5] = (byte)((value >> 40) & 0xFF);
      stream.buffer[stream.offset + 6] = (byte)((value >> 48) & 0xFF);
      stream.buffer[stream.offset + 7] = (byte)(value >> 56); // Highest byte
    }

    // Advance the offset by 8 bytes after writing
    stream.offset += 8;
  }
}