using Basalt.BinaryStream;

var stream = new BinaryStream();

int iterations = 9999999;

var startTime = DateTime.Now;

for (int i = 0; i < iterations; i++)
{
  stream.WriteUInt8(123);
  stream.WriteUInt16(45678);
  stream.WriteUInt32(1234567890);
  stream.WriteUInt64(12345678901234567890UL);

  stream.ResetOffset();

  byte uint8Value = stream.ReadUInt8();
  ushort uint16Value = stream.ReadUInt16();
  uint uint32Value = stream.ReadUInt32();
  ulong uint64Value = stream.ReadUInt64();

  if (uint8Value != 123 || uint16Value != 45678 || uint32Value != 1234567890 || uint64Value != 12345678901234567890UL)
    throw new Exception("Data mismatch");
}

var endTime = DateTime.Now;

Console.WriteLine($"Time taken for {iterations} iterations: {(endTime - startTime).TotalSeconds} seconds");