namespace Xbehave.Sdk
{
    using System;

    public interface IStepReporter
    {
        void Begin(string stepName);

        void Failed(Exception exception);

        void Passed();
    }
}
