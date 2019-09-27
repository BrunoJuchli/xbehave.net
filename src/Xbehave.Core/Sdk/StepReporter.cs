namespace Xbehave.Sdk
{
    using System;
    using System.Threading;
    using Xunit.Sdk;

    public class StepReporter
    {
        private readonly IMessageBus messageBus;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly string scenarioName;

        public StepReporter(IMessageBus messageBus, CancellationTokenSource cancellationTokenSource, string scenarioName)
        {
            this.messageBus = messageBus;
            this.cancellationTokenSource = cancellationTokenSource;
            this.scenarioName = scenarioName;
        }


        public void Success(string stepName)
        {

        }

        public void Ignored(string stepName)
        {

        }

        public void Failure(string stepName, Exception exception)
        {

        }
    }
}
