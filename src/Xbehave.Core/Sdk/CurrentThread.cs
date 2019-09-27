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
        private static List<IStepDefinition> stepDefinitions;

        /// <summary>
        /// Gets the step definitions for the currently executing thread.
        /// </summary>
        public static ICollection<IStepDefinition> StepDefinitions
        {
            get
            {
                EnsureCollecting();
                return stepDefinitions;
            }
        }

        private static bool IsCollecting => stepDefinitions != null;

        /// <summary>
        /// Begin collection of step definitions. <c>This is intended for use by Xbehave only</c>
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void BeginCollection()
        {
            if (IsCollecting)
            {
                throw new InvalidOperationException("Already collecting.");
            }

            stepDefinitions = new List<IStepDefinition>();
        }

        /// <summary>
        /// End collection of step definitions. <c>This is intended for use by Xbehave only</c>
        /// </summary>
        /// <returns>All step definitions collected since collection begun.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static IReadOnlyList<IStepDefinition> EndCollection()
        {
            if (!IsCollecting)
            {
                throw new InvalidOperationException("Not collecting. Collection hasn't been started.");
            }

            var result = stepDefinitions;
            stepDefinitions = null;
            return result;
        }

        private static void EnsureCollecting()
        {
            if (!IsCollecting)
            {
                throw new InvalidOperationException($"Steps may only be added by test methods. Nesting of steps is not supported. Adding steps on other threads is not supported, either."); // TODO: test adding a step on another thread!
            }
        }
    }
}
