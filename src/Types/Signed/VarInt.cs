namespace Basalt.BinaryStream.Types.Unsigned;

public class VarIntType : DataType<int>
{
  public static new int Read(BinaryStream stream)
  {
    // Read the unsigned variable-length integer from the stream
    uint encoded = UVarIntType.Read(stream);

    // Decode the zigzag encoding to get the original signed integer value
    return (int)((encoded >> 1) ^ (-(encoded & 1)));
  }

  public static new void Write(BinaryStream stream, int value)
  {
    // Encode the signed integer using zigzag encoding to get an unsigned value
    uint encoded = (uint)((value << 1) ^ (value >> 31));

    // Write the encoded unsigned variable-length integer to the stream
    UVarIntType.Write(stream, encoded);
  }
}
