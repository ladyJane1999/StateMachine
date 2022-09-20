using System.Runtime.CompilerServices;

internal class TaskLoop 
{
    public Action? A { get; set; }
    public int Max { get; set; }
    public Task Task { get; set; }
    private static TaskCompletionSource tcs;

    public Task Run()
    {
        int state = 0;
        tcs = new TaskCompletionSource();
        Action<int> body = null;
       
            body = state =>
            {
                switch (state)
            {
                case 0:
                    Task = tcs.Task;
                    tcs.Task.ContinueWith(t => body(1));
                    break;
                case 1:
                    while (state <= Max)
                    {
                    A.Invoke();
                      state++;
                   }
                    Task = tcs.Task;
                    tcs.Task.ContinueWith(t => body(2));
                    break;
                case 2:
                    Task = tcs.Task;
                    break;
        }
        };
        body(0);
        tcs.SetResult();
       
        return tcs.Task;
    }
}
