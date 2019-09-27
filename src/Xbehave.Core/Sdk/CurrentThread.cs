// <copyright file="CurrentThread.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave.Sdk
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the currently executing thread.
    /// </summary>
    public static class CurrentThread
    {
        [ThreadStatic]
        private static StepDefinitionCollector stepDefinitions;

        /// <summary>
        /// Gets the step definitions for the currently executing thread.
        /// </summary>
        public static ICollection<IStepDefinition> StepDefinitions => GetStepDefinitionsCollector();

        /// <summary>
        /// Gets the lifecycle managing backing object. <c>This is intended for use by Xbehave only</c>.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static IStepDefinitionCollector StepDefinitionCollector => GetStepDefinitionsCollector();

        private static StepDefinitionCollector GetStepDefinitionsCollector() =>
            stepDefinitions ?? (stepDefinitions = new StepDefinitionCollector());
    }
}
