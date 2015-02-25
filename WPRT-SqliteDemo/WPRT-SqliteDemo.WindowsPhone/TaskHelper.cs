using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPRT_SqliteDemo
{
    public class TaskHelper
    {
        private static Queue<Task> _taskQueue = null;
        private static bool _running = false;
        private static Task _runningTask = null;
        private static Dictionary<int, CancellationToken> _dicToken = null;

        static TaskHelper()
        {
            _taskQueue = new Queue<Task>();
            _dicToken = new Dictionary<int, CancellationToken>();
        }

        public static void AddTask(Action action, out int taskId)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = new Task(action, cts.Token);
            _dicToken.Add(task.Id, cts.Token);
            taskId = task.Id;
            _taskQueue.Enqueue(task);
        }

        public static void Start()
        {
            if (_running) return;
            if (_taskQueue.Count > 0)
            {
                var task = _taskQueue.Dequeue();
                task.Start();
                _running = true;
                task.ContinueWith((t) =>
                {
                    _dicToken.Remove(t.Id);
                    _running = false;
                    Start();
                });
            }
        }

        public static void Cancel(int taskId)
        {
            if (_dicToken.ContainsKey(taskId))
            {
                _dicToken[taskId].ThrowIfCancellationRequested();
                //_dicToken.Remove(taskId);
            }
        }
    }
}
