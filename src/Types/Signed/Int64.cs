using System.Buffers.Binary;

using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Signed;

public class Int64Type : DataType<long>
{
  public static new long Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new long Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (8 bytes for uint64)
    if (stream.offset + 8 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the bytes based on the specified endianness
    long value = endian == Endianness.Big
      ? BinaryPrimitives.ReadInt64BigEndian(stream.buffer.AsSpan(stream.offset, 8))
      : BinaryPrimitives.ReadInt64LittleEndian(stream.buffer.AsSpan(stream.offset, 8));

    // Advance the offset by 8 bytes after reading
    stream.offset += 8;

    // Return the read value as long
    return value;
  }

  public static new void Write(BinaryStream stream, long value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, long value, Endianness endian = Endianness.Big)
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
      BinaryPrimitives.WriteInt64BigEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteInt64LittleEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }

    // Advance the offset by 8 bytes after writing
    stream.offset += 8;
  }
}