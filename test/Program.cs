using Basalt.BinaryStream;

var stream = new BinaryStream();

int iterations = 9999;

var startTime = DateTime.Now;

for (int i = 0; i < iterations; i++)
{
  // varint encoding and decoding
  var rand = new Random().Next(int.MinValue, int.MaxValue);
  
  // Write the random integer to the stream using variable-length encoding
  stream.WriteInt32(rand);
}

// Reset the stream's offset to the beginning for reading
stream.ResetOffset();

for (int i = 0; i < iterations; i++)
{
  // Read the variable-length encoded integer from the stream
  stream.ReadInt32();
}

var endTime = DateTime.Now;

Console.WriteLine($"Time taken for {iterations} iterations: {(endTime - startTime).TotalSeconds} seconds");