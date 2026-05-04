using System.Buffers.Binary;

using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types.Floats;

public class Float64Type : DataType<double>
{
  public static new double Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new double Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Check if there is enough data to read (8 bytes for float64)
    if (stream.offset + 8 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the bytes based on the specified endianness
    double value = endian == Endianness.Big
      ? BinaryPrimitives.ReadDoubleBigEndian(stream.buffer.AsSpan(stream.offset, 8))
      : BinaryPrimitives.ReadDoubleLittleEndian(stream.buffer.AsSpan(stream.offset, 8));

    // Advance the offset by 8 bytes after reading
    stream.offset += 8;

    // Return the read value as double
    return value;
  }

  public static new void Write(BinaryStream stream, double value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, double value, Endianness endian = Endianness.Big)
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
      BinaryPrimitives.WriteDoubleBigEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }
    else // Little Endian
    {
      BinaryPrimitives.WriteDoubleLittleEndian(stream.buffer.AsSpan(stream.offset, 8), value);
    }

    // Advance the offset by 8 bytes after writing
    stream.offset += 8;
  }
}
