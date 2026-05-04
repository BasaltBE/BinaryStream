using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream.Types.Strings;

public class VarStringType : DataType<string>
{
  public static new string Read(BinaryStream stream)
  {
    // Read the length of the string as a variable-length integer
    uint length = UVarIntType.Read(stream);

    // Check if there is enough data to read the string
    if (stream.offset + length > stream.buffer.Length)
      throw new Exception("Not enough data to read");

    // Read the string bytes and convert to a string using UTF-8 encoding
    string result = System.Text.Encoding.UTF8.GetString(stream.buffer, stream.offset, (int)length);
    stream.offset += (int)length; // Move the offset past the string data

    return result;
  }

  public static new void Write(BinaryStream stream, string value)
  {
    // Convert the string to bytes using UTF-8 encoding
    byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(value);

    // Write the length of the string as a variable-length integer
    UVarIntType.Write(stream, (uint)stringBytes.Length);

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