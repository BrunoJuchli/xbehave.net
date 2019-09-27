using System;

namespace Xbehave.Sdk
{
    public interface IStepReporter
    {
        void Success(string stepName);
        void Ignored(string stepName);
        void Failure(string stepName, Exception exception);
    }
}
