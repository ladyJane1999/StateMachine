
using System.Runtime.CompilerServices;

internal class TaskLoop : IAsyncStateMachine
{
    public Action? A { get; set; }
    public int Max { get; set; }
    public Task Task { get; set; }

    private static TaskCompletionSource tcs;

    public Int32 m_state;
    public Task Run()
    {

        tcs = new TaskCompletionSource();
        var awaiterType1 = tcs.Task.GetAwaiter();

        if (!awaiterType1.IsCompleted)
        {
            m_state = 0;
            tcs.Task.ContinueWith(t => MoveNext());   // При завершении Task ContinueWith вызывает MoveNext

        }

        tcs.SetResult();
        Task= tcs.Task;
        return tcs.Task;
    }

    public void MoveNext()
    {
        int state = 0;
        int num = state;
        try
        {
            while (state < Max)
            {
                A.Invoke();
                state++;
            }            
        }
        catch (AggregateException exception)
        {
            foreach (Exception ex in exception.InnerExceptions)
            Console.WriteLine(exception.InnerException);
            return;
        }
           
    }
    void IAsyncStateMachine.MoveNext()
    {
        MoveNext();
    }

    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
       SetStateMachine(stateMachine);
    }

    private void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }
}