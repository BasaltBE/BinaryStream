namespace Basalt.BinaryStream.Types.Unsigned;

public class UInt8Type : DataType<byte>
{
  public static new byte Read(BinaryStream stream)
  {
    // Check if there is enough data to read (1 byte for uint8)
    if (stream.offset + 1 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the by and return it as byte (since UInt8 is unsigned, we can directly return the byte value)
    return stream.buffer[stream.offset++];
  }

  public static new void Write(BinaryStream stream, byte value)
  {
    // Ensure the buffer can accommodate the new byte
    if (stream.offset + 1 > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + 1);
    }

    // Write the byte to the buffer
    stream.buffer[stream.offset++] = value;
  }
}