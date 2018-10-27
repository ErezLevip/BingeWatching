using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingeWatching.Client
{
    public static class ContentStateChecker
    {
        public async static Task WaitForCancellation(Func<Task<bool>> f, CancellationToken token)
        {
            while (true)
            {
                var shouldWait = await f();
                if (token.IsCancellationRequested)
                    return;

                if (shouldWait)
                    await Task.Delay(10000);
            }
        }
    }
}
