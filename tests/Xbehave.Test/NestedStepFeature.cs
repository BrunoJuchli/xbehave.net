// <copyright file="NestedStepFeature.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave.Test
{
    using System;
    using FluentAssertions;
    using Xbehave.Test.Infrastructure;
    using Xunit.Abstractions;

    // In order to know I made a mistake
    // As a developer
    // I want xBehave to tell me when I've nested steps
    public class NestedStepFeature : Feature
    {
        [Scenario]
        public void FaultyScenario(Type feature, ITestResultMessage[] results)
        {
            "Given a scenario with nested steps because \"the developer made a mistake\""
                .x(() => feature = typeof(AScenarioWithNestedStepsBecauseTheDeveloperMadeAMistake));

            "When I run the scenario"
                .x(() => results = this.Run<ITestResultMessage>(feature));

            "Then there should be one failure"
                .x(() => results.Should().HaveCount(1)
                    .And.Subject.Should().OnlyContain(x => x is ITestFailed));
        }

        private static class AScenarioWithNestedStepsBecauseTheDeveloperMadeAMistake
        {
            [Scenario]
            public static void Scenario() =>
                "Given something"
                    .x(() =>
                    {
                        "When I nest it".x(() => throw new NotImplementedException("Nested steps are not supported."));
                    });
        }
    }
}
