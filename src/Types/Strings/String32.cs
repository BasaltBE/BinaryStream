using Basalt.BinaryStream.Enums;
using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream.Types.Strings;

public class String32Type : DataType<string>
{
  public static new string Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new string Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Read the length of the string as a uint32
    uint length = UInt32Type.Read(stream, endian);

    // Check if there is enough data to read the string
    if (stream.offset + length > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the string bytes and convert to a string using UTF-8 encoding
    string result = System.Text.Encoding.UTF8.GetString(stream.buffer, stream.offset, (int)length);
    stream.offset += (int)length; // Move the offset past the string data

    return result;
  }

  public static new void Write(BinaryStream stream, string value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, string value, Endianness endian = Endianness.Big)
  {
    // Convert the string to bytes using UTF-8 encoding
    byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(value);

    // Write the length of the string as a uint32
    UInt32Type.Write(stream, (uint)stringBytes.Length, endian);

    // Ensure the buffer can accommodate the new string bytes
    if (stream.offset + stringBytes.Length > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + stringBytes.Length);
    }

    // Write the string bytes to the buffer
    Array.Copy(stringBytes, 0, stream.buffer, stream.offset, stringBytes.Length);
    stream.offset += stringBytes.Length; // Move the offset past the written string data
  }
}