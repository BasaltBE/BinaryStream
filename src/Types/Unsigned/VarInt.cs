namespace Basalt.BinaryStream.Types.Unsigned;

public class VarIntType : DataType<uint>
{
  public const int SIZE = 5;

  public static new uint Read(BinaryStream stream)
  {
    byte[] buffer = stream.buffer;
    int offset = stream.offset;

    if (offset >= buffer.Length)
      throw new Exception("Not enough data to read VarInt");

    uint b = buffer[offset++];

    if ((b & 0x80) == 0)
    {
      stream.offset = offset;
      return b;
    }

    uint value = b & 0x7F;

    if (offset >= buffer.Length)
      throw new Exception("Not enough data to read VarInt");

    b = buffer[offset++];
    value |= (b & 0x7F) << 7;

    if ((b & 0x80) == 0)
    {
      stream.offset = offset;
      return value;
    }

    if (offset >= buffer.Length)
      throw new Exception("Not enough data to read VarInt");

    b = buffer[offset++];
    value |= (b & 0x7F) << 14;

    if ((b & 0x80) == 0)
    {
      stream.offset = offset;
      return value;
    }

    if (offset >= buffer.Length)
      throw new Exception("Not enough data to read VarInt");

    b = buffer[offset++];
    value |= (b & 0x7F) << 21;

    if ((b & 0x80) == 0)
    {
      stream.offset = offset;
      return value;
    }

    if (offset >= buffer.Length)
      throw new Exception("Not enough data to read VarInt");

    b = buffer[offset++];
    value |= (b & 0x7F) << 28;

    if ((b & 0x80) == 0)
    {
      stream.offset = offset;
      return value;
    }

    throw new Exception("VarInt exceeds maximum size");
  }

  public static new void Write(BinaryStream stream, uint value)
  {
    EnsureCapacity(stream, SIZE);

    byte[] buffer = stream.buffer;
    int offset = stream.offset;

    while (value >= 0x80)
    {
      buffer[offset++] = (byte)(value | 0x80);
      value >>= 7;
    }

    buffer[offset++] = (byte)value;

    stream.offset = offset;
  }

  private static void EnsureCapacity(BinaryStream stream, int additionalBytes)
  {
    int requiredLength = stream.offset + additionalBytes;

    if (requiredLength > stream.buffer.Length)
    {
      Array.Resize(ref stream.buffer, requiredLength);
    }
  }
}
