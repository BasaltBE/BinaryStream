using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream.Types.Signed;

public class VarLongType : DataType<long>
{
  public static new long Read(BinaryStream stream)
  {
    // Read the unsigned variable-length long integer from the stream
    ulong encoded = UVarLongType.Read(stream);

    // Decode the zigzag encoding to get the original signed long integer value
    return (long)((encoded >> 1) ^ ((encoded & 1) == 0 ? 0 : ~0UL));
  }

  public static new void Write(BinaryStream stream, long value)
  {
    // Encode the signed longeger using zigzag encoding to get an unsigned value
    ulong encoded = (ulong)((value << 1) ^ (value >> 63));

    // Write the encoded unsigned variable-length longeger to the stream
    UVarLongType.Write(stream, encoded);
  }
}
