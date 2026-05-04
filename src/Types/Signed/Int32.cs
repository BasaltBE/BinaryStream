using System.Buffers.Binary;

using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Signed;

public class Int32Type : DataType<int>
{
  public static new int Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new int Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (4 bytes for int32)
    if (stream.offset + 4 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the bytes based on the specified endianness
    int value = endian == Endianness.Big
      ? BinaryPrimitives.ReadInt32BigEndian(stream.buffer.AsSpan(stream.offset, 4))
      : BinaryPrimitives.ReadInt32LittleEndian(stream.buffer.AsSpan(stream.offset, 4));

    // Advance the offset by 4 bytes after reading
    stream.offset += 4;

    // Return the read value as int
    return value;
  }

  public static new void Write(BinaryStream stream, int value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, int value, Endianness endian = Endianness.Big)
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
      BinaryPrimitives.WriteInt32BigEndian(stream.buffer.AsSpan(stream.offset, 4), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteInt32LittleEndian(stream.buffer.AsSpan(stream.offset, 4), value);
    }

    // Advance the offset by 4 bytes after writing
    stream.offset += 4;
  }
}