using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Helpers
{
    public static class StreamHelper
    {
        public static async Task CopyToAsync(this Stream source, Stream destination, int totalBytes, IProgress<int> progress, CancellationToken cancellationToken = default(CancellationToken), int bufferSize = 0x1000)
        {
            var buffer = new byte[bufferSize];
            int bytesRead;
            int totalRead = 0;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                totalRead += bytesRead;
                progress.Report((int)((totalRead / (float)totalBytes) *100));
            }
        }
    }
}
