namespace Xbehave.Execution
{
    using System;
    using System.Threading;
    using Xbehave.Execution.Extensions;
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


        // doesn't show test name - we're just part of the scenario.
        //public void Success(string stepName) =>
        //    this.messageBus.Queue(
        //        this.scenario, test => new TestPassed(test, default, "fabulous"), this.cancellationTokenSource);

        // the name must begin with the type name!
        public void Success(string stepName) =>
            this.messageBus.Queue(
                new Step(this.scenario, $"{this.scenario.DisplayName} {stepName}"), test => new TestPassed(test, default, null), this.cancellationTokenSource);


        //public void Success(string stepName)
        //{
        //    this.messageBus.QueueMessage(new TestStarting(this.scenario));

        //    var step = new Step(this.scenario, stepName);
        //    this.messageBus.QueueMessage(new TestStarting(step));

        //    var innerStep = new Step(this.scenario, "innerStep");

        //    this.messageBus.QueueMessage(new TestPassed(step, default, "fabulous"));

        //    this.messageBus.QueueMessage(new TestFinished(step, default, null));

        //    this.messageBus.QueueMessage(new TestStarting(innerStep));

        //    this.messageBus.QueueMessage(new TestPassed(innerStep, default, "inner test"));

        //    this.messageBus.QueueMessage(new TestFinished(innerStep, default, "inner test done"));

        //    this.messageBus.QueueMessage(new TestPassed(this.scenario, default, null));

        //    this.messageBus.QueueMessage(new TestFinished(this.scenario, default, null));
        //}

        public void Ignored(string stepName) =>
            this.messageBus.Queue(
                this.CreateStep(stepName), test => new TestSkipped(test, "don't know'"), this.cancellationTokenSource);

        public void Failure(string stepName, Exception exception) =>
            this.messageBus.Queue(
                this.CreateStep(stepName), test => new TestFailed(test, default, "failed", exception), this.cancellationTokenSource);

        private IStep CreateStep(string stepName)
            => new Step(this.scenario, $"{this.scenario.DisplayName} {stepName}");
    }
}
