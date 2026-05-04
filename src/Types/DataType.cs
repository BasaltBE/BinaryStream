using Basalt.BinaryStream.Enums;

namespace Basalt.BinaryStream.Types;

public abstract class DataType<T>
{
  protected DataType() { }

  public static T Read(BinaryStream stream)
  {
    throw new NotImplementedException("Read method must be implemented in derived classes");
  }

  public static T Read(BinaryStream stream, Endianness endian = Endianness.Big)
  {
    throw new NotImplementedException("Read method with endianness must be implemented in derived classes");
  }

  public static void Write(BinaryStream stream, T value)
  {
    throw new NotImplementedException("Write method must be implemented in derived classes");
  }

  public static void Write(BinaryStream stream, T value, Endianness endian = Endianness.Big)
  {
    throw new NotImplementedException("Write method with endianness must be implemented in derived classes");
  }
}
