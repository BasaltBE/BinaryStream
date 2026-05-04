using Basalt.BinaryStream;

var stream = new BinaryStream();

stream.WriteUInt8(255);
stream.WriteUInt16(65535);
stream.WriteUInt32(4294967295);
stream.WriteUInt64(18446744073709551615);

stream.ResetOffset();

Console.WriteLine(stream.ReadUInt8()); // 255
Console.WriteLine(stream.ReadUInt16()); // 65535
Console.WriteLine(stream.ReadUInt32()); // 4294967295
Console.WriteLine(stream.ReadUInt64()); // 18446744073709551615
