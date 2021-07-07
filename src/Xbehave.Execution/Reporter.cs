namespace Xbehave.Execution
{
    using System;
    using System.Threading;
    using Xbehave.Execution.Extensions;
    using Xunit.Abstractions;
    using Xunit.Sdk;


    public class Reporter
    {
        private readonly IMessageBus messageBus;
        private readonly CancellationTokenSource cancellationTokenSource;

        public Reporter(IMessageBus messageBus, CancellationTokenSource cancellationTokenSource)
        {
            this.messageBus = messageBus;
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public void Passed(ITest test) =>
            this.messageBus.Queue(test, t => new TestPassed(t, default, null), this.cancellationTokenSource);

        public void Failed(ITest test, Exception exception) =>
            this.messageBus.Queue(test, t => new TestFailed(t, default, null, exception), this.cancellationTokenSource);
    }
}
