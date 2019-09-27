// <copyright file="IStepDefinitionCollector.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave.Sdk
{
    using System.Collections.Generic;

    /// <summary>
    /// Maps the lifecycle of step definition collecting.
    /// Fails early in case steps are added outside of collection phase.
    /// Fails early in case phase is begun/ended out of order.
    /// <c>Not thread safe.</c>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public interface IStepDefinitionCollector
    {
        /// <summary>
        /// Begin collection of step definitions. <c>This is intended for use by Xbehave only</c>
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        void BeginCollection();

        /// <summary>
        /// End collection of step definitions. <c>This is intended for use by Xbehave only</c>
        /// </summary>
        /// <returns>All step definitions collected since collection begun.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        IReadOnlyList<IStepDefinition> EndCollection();
    }
}
