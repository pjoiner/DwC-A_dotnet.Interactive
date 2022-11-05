using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Events;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace DwC_A.Interactive.Extensions
{
    //Shamelessly copied from the dotnet.interactive test utilities.
    //See https://github.com/dotnet/interactive/blob/3c5637d513f0f3c406b449435b245e699f82a989/src/Microsoft.DotNet.Interactive.Tests/Utility/KernelExtensions.cs
    internal static class KernelExtensions
    {
        public static async Task<(bool success, ValueProduced valueProduced)> TryRequestValueAsync(this Kernel kernel, string valueName)
        {
            if (kernel.SupportsCommandType(typeof(RequestValue)))
            {
                var commandResult = await kernel.SendAsync(new RequestValue(valueName));

                if (await commandResult.KernelEvents.OfType<ValueProduced>().FirstOrDefaultAsync() is { } valueProduced)
                {
                    return (true, valueProduced);
                }
            }

            return (false, default);
        }
    }
}
