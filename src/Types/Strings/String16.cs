using Basalt.BinaryStream.Enums;
using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream.Types.Strings;

public class String16Type : DataType<string>
{
  public static new string Read(BinaryStream stream) => Read(stream, Endianness.Big);

  public static new string Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    // Read the length of the string as a 16-bit unsigned integer
    ushort length = UInt16Type.Read(stream, endian);

    // Check if there is enough data to read the string
    if (stream.offset + length > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the bytes for the string and convert them to a UTF-8 string
    string result = System.Text.Encoding.UTF8.GetString(stream.buffer, stream.offset, length);
    stream.offset += length; // Move the offset past the string data

    return result;
  }

  public static new void Write(BinaryStream stream, string value) => Write(stream, value, Endianness.Big);

  public static new void Write(BinaryStream stream, string value, Endianness endian = Endianness.Big)
  {
    // Convert the string to UTF-8 bytes
    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);

    // Write the length of the string as a 16-bit unsigned integer
    UInt16Type.Write(stream, (ushort)bytes.Length, endian);

    // Ensure the buffer can accommodate the new bytes
    if (stream.offset + bytes.Length > stream.buffer.Length)
    {
      // Expand the buffer if necessary
      Array.Resize(ref stream.buffer, stream.buffer.Length + bytes.Length);
    }

    // Write the bytes to the buffer
    Array.Copy(bytes, 0, stream.buffer, stream.offset, bytes.Length);
    stream.offset += bytes.Length; // Move the offset past the string data
  }
}