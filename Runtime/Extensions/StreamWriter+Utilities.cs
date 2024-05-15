using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class StreamWriterExtensions
    {
        public static async Task WriteAsync(this StreamWriter self, params string[] lines)
        {
            foreach(var line in lines) {
                await self.WriteLineAsync(line);
            }
        }
    }
}