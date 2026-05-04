using System.Buffers.Binary;

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

    // Read the bytes based on the specified endianness
    ushort value = endian == Endianness.Big
      ? BinaryPrimitives.ReadUInt16BigEndian(stream.buffer.AsSpan(stream.offset, 2))
      : BinaryPrimitives.ReadUInt16LittleEndian(stream.buffer.AsSpan(stream.offset, 2));

    // Advance the offset by 2 bytes after reading
    stream.offset += 2;

    // Return the read value as ushort
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
      BinaryPrimitives.WriteUInt16BigEndian(stream.buffer.AsSpan(stream.offset, 2), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteUInt16LittleEndian(stream.buffer.AsSpan(stream.offset, 2), value);
    }

    // Advance the offset by 2 bytes after writing
    stream.offset += 2;
  }
}