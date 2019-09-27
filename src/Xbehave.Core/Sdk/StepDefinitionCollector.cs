// <copyright file="StepDefinitionCollector.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave.Sdk
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class StepDefinitionCollector : ICollection<IStepDefinition>, IStepDefinitionCollector
    {
        private List<IStepDefinition> stepDefinitions;

        public int Count => this.SafeStepDefinitions.Count;

        public bool IsReadOnly => this.SafeStepDefinitions.IsReadOnly;

        private bool IsCollecting => this.stepDefinitions != null;

        private ICollection<IStepDefinition> SafeStepDefinitions
        {
            get
            {
                if (!this.IsCollecting)
                {
                    throw new InvalidOperationException($"Steps may only be added by test methods. Nesting of steps is not supported. Adding steps on other threads is not supported, either.");
                }

                return this.stepDefinitions;
            }
        }

        public IEnumerator<IStepDefinition> GetEnumerator() => this.SafeStepDefinitions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Add(IStepDefinition item) => this.SafeStepDefinitions.Add(item);

        public void Clear() => this.SafeStepDefinitions.Clear();

        public bool Contains(IStepDefinition item) => this.SafeStepDefinitions.Contains(item);

        public void CopyTo(IStepDefinition[] array, int arrayIndex) =>
            this.SafeStepDefinitions.CopyTo(array, arrayIndex);

        public bool Remove(IStepDefinition item) => this.SafeStepDefinitions.Remove(item);

        public void BeginCollection()
        {
            if (this.IsCollecting)
            {
                throw new InvalidOperationException("Already collecting.");
            }

            this.stepDefinitions = new List<IStepDefinition>();
        }

        public IReadOnlyList<IStepDefinition> EndCollection()
        {
            if (!this.IsCollecting)
            {
                throw new InvalidOperationException("Collection hasn't been started or has already ended.");
            }

            var result = this.stepDefinitions;
            this.stepDefinitions = null;
            return result;
        }
    }
}
