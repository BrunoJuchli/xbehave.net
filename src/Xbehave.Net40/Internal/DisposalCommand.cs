﻿// <copyright file="DisposalCommand.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace Xbehave.Internal
{
    using System;
    using System.Collections.Generic;
    using Xbehave.Infra;
    using Xunit.Sdk;

    internal class DisposalCommand : CommandBase
    {
        private readonly IDisposer disposer = new Disposer();
        private readonly IEnumerable<IDisposable> disposables;

        public DisposalCommand(MethodCall call, int ordinal, string context, IEnumerable<IDisposable> disposables)
            : base(call, ordinal, "Disposal", context)
        {
            this.disposables = disposables;
        }

        public override MethodResult Execute(object testClass)
        {
            this.disposer.Dispose(this.disposables);
            return new PassedResult(this.testMethod, this.DisplayName);
        }
    }
}