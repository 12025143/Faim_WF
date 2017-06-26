
using System;

namespace F_Enums
{
    [Serializable]
    enum eThreadState
    {
        Free = -1,
        // 摘要:
        //     线程已启动，它未被阻塞，并且没有挂起的 System.Threading.ThreadAbortException。
        Running = 0,
        //
        // 摘要:
        //     正在请求线程停止。这仅用于内部。
        StopRequested = 1,
        //
        // 摘要:
        //     正在请求线程挂起。
        SuspendRequested = 2,
        //
        // 摘要:
        //     线程正作为后台线程执行（相对于前台线程而言）。此状态可以通过设置 System.Threading.Thread.IsBackground 属性来控制。
        Background = 4,
        //
        // 摘要:
        //     尚未对线程调用 System.Threading.Thread.Start() 方法。
        Unstarted = 8,
        //
        // 摘要:
        //     线程已停止。
        Stopped = 16,
        //
        // 摘要:
        //     线程已被阻止。这可能是因为：调用 System.Threading.Thread.Sleep(System.Int32) 或 System.Threading.Thread.Join()、请求锁定（例如通过调用
        //     System.Threading.Monitor.Enter(System.Object) 或 System.Threading.Monitor.Wait(System.Object,System.Int32,System.Boolean)）或等待线程同步对象（例如
        //     System.Threading.ManualResetEvent）。
        WaitSleepJoin = 32,
        //
        // 摘要:
        //     线程已挂起。
        Suspended = 64,
        //
        // 摘要:
        //     已对线程调用了 System.Threading.Thread.Abort(System.Object) 方法，但线程尚未收到试图终止它的挂起的
        //     System.Threading.ThreadAbortException。
        AbortRequested = 128,
        //
        // 摘要:
        //     线程状态包括 System.Threading.ThreadState.AbortRequested 并且该线程现在已死，但其状态尚未更改为 System.Threading.ThreadState.Stopped。
        Aborted = 256,
    }
}
