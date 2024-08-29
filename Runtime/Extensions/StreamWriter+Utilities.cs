using System.IO;
using System.Threading.Tasks;

namespace MartonioJunior.EdKit
{
    public static partial class StreamWriterExtensions
    {
        /**
        <summary>Writes the following text lines to the stream.</summary>
        <param name="lines">The lines to write.</param>
        <returns>A task that represents the asynchronous operation.</returns>
        */
        public static async Task WriteAsync(this StreamWriter self, params string[] lines)
        {
            foreach(var line in lines) {
                await self.WriteLineAsync(line);
            }
        }
    }
}