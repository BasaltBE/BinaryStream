using System.Buffers.Binary;

using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Floats;

public class Float32Type : DataType<float>
{
  public static new float Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new float Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (4 bytes for float32)
    if (stream.offset + 4 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the bytes based on the specified endianness
    float value = endian == Endianness.Big
      ? BinaryPrimitives.ReadSingleBigEndian(stream.buffer.AsSpan(stream.offset, 4))
      : BinaryPrimitives.ReadSingleLittleEndian(stream.buffer.AsSpan(stream.offset, 4));

    // Advance the offset by 4 bytes after reading
    stream.offset += 4;

    // Return the read value as float
    return value;
  }

  public static new void Write(BinaryStream stream, float value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, float value, Endianness endian = Endianness.Big)
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
      BinaryPrimitives.WriteSingleBigEndian(stream.buffer.AsSpan(stream.offset, 4), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteSingleLittleEndian(stream.buffer.AsSpan(stream.offset, 4), value);
    }

    // Advance the offset by 4 bytes after writing
    stream.offset += 4;
  }
}
