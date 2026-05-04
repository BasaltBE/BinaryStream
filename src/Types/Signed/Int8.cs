namespace Basalt.BinaryStream.Types.Signed;

public class Int8Type : DataType<sbyte>
{
  public static new sbyte Read(BinaryStream stream)
  {
    // Check if there is enough data to read (1 byte for uint8)
    if (stream.offset + 1 > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the byte and convert it to sbyte (since UInt8 is unsigned, we need to handle the conversion properly)
    return (sbyte)stream.buffer[stream.offset++];
  }

  public static new void Write(BinaryStream stream, sbyte value)
  {
    // Ensure the buffer can accommodate the new byte
    if (stream.offset + 1 > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + 1);
    }

    // Write the byte to the buffer
    stream.buffer[stream.offset++] = (byte)value; // Cast sbyte to byte for storage, as the underlying storage is byte array
  }
}