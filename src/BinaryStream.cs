using Basalt.BinaryStream.Enums;
using Basalt.BinaryStream.Types.Signed;
using Basalt.BinaryStream.Types.Unsigned;

namespace Basalt.BinaryStream;

public class BinaryStream
{
  // Buffer needs to dynamically grow for writing, but for reading it should be fixed to the size of the data being read
  internal byte[] buffer = Array.Empty<byte>();

  internal int offset = 0;

  public BinaryStream() { }

  public BinaryStream(byte[] data)
  {
    buffer = data;
  }

  public void ResetOffset()
  {
    offset = 0;
  }

  public byte ReadUInt8()
  {
    return UInt8Type.Read(this);
  }

  public void WriteUInt8(byte value)
  {
    UInt8Type.Write(this, value);
  }

  public ushort ReadUInt16(Endianness endian = Endianness.Big)
  {
    return UInt16Type.Read(this, endian);
  }

  public void WriteUInt16(ushort value, Endianness endian = Endianness.Big)
  {
    UInt16Type.Write(this, value, endian);
  }

  public uint ReadUInt32(Endianness endian = Endianness.Big)
  {
    return UInt32Type.Read(this, endian);
  }

  public void WriteUInt32(uint value, Endianness endian = Endianness.Big)
  {
    UInt32Type.Write(this, value, endian);
  }

  public ulong ReadUInt64(Endianness endian = Endianness.Big)
  {
    return UInt64Type.Read(this, endian);
  }

  public void WriteUInt64(ulong value, Endianness endian = Endianness.Big)
  {
    UInt64Type.Write(this, value, endian);
  }

  public sbyte ReadInt8()
  {
    return Int8Type.Read(this);
  }

  public void WriteInt8(sbyte value)
  {
    Int8Type.Write(this, value);
  }

  public short ReadInt16(Endianness endian = Endianness.Big)
  {
    return Int16Type.Read(this, endian);
  }

  public void WriteInt16(short value, Endianness endian = Endianness.Big)
  {
    Int16Type.Write(this, value, endian);
  }

  public int ReadInt32(Endianness endian = Endianness.Big)
  {
    return Int32Type.Read(this, endian);
  }

  public void WriteInt32(int value, Endianness endian = Endianness.Big)
  {
    Int32Type.Write(this, value, endian);
  }

  public long ReadInt64(Endianness endian = Endianness.Big)
  {
    return Int64Type.Read(this, endian);
  }

  public void WriteInt64(long value, Endianness endian = Endianness.Big)
  {
    Int64Type.Write(this, value, endian);
  }
}
