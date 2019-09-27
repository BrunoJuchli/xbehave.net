namespace Xbehave.Execution
{
    using System;
    using System.Threading;
    using Xbehave.Sdk;
    using Xunit.Sdk;

    public class StepReporter : IStepReporter
    {
        private readonly IMessageBus messageBus;
        private readonly IScenario scenario;
        private readonly CancellationTokenSource cancellationTokenSource;

        public StepReporter(IMessageBus messageBus, IScenario scenario, CancellationTokenSource cancellationTokenSource)
        {
            this.messageBus = messageBus;
            this.scenario = scenario;
            this.cancellationTokenSource = cancellationTokenSource;
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
