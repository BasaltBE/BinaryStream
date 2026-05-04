using System.Buffers.Binary;

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

    // Read the bytes based on the specified endianness
    ulong value = endian == Endianness.Big
      ? BinaryPrimitives.ReadUInt64BigEndian(stream.buffer.AsSpan(stream.offset, 8))
      : BinaryPrimitives.ReadUInt64LittleEndian(stream.buffer.AsSpan(stream.offset, 8));

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
      BinaryPrimitives.WriteUInt64BigEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteUInt64LittleEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }

    // Advance the offset by 8 bytes after writing
    stream.offset += 8;
  }
}