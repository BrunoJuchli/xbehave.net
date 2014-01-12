﻿// <copyright file="AsyncTestSyncContext.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

using System;
using System.Threading;

namespace Xbehave.Sdk
{
    /// <summary>
    /// Implementation from xUnit 2.0
    /// </summary>
    internal class AsyncTestSyncContext : SynchronizationContext, IDisposable
    {
        readonly ManualResetEvent @event = new ManualResetEvent(initialState: true);
        Exception exception;
        int operationCount;

        public void Dispose()
        {
            ((IDisposable)@event).Dispose();
        }

        public override void OperationCompleted()
        {
            var result = Interlocked.Decrement(ref operationCount);
            if (result == 0)
                @event.Set();
        }

        public override void OperationStarted()
        {
            Interlocked.Increment(ref operationCount);
            @event.Reset();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            // The call to Post() may be the state machine signaling that an exception is
            // about to be thrown, so we make sure the operation count gets incremented
            // before the QUWI, and then decrement the count when the operation is done.
            OperationStarted();

            ThreadPool.QueueUserWorkItem(s =>
            {
                try
                {
                    Send(d, state);
                }
                finally
                {
                    OperationCompleted();
                }
            });
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        public Exception WaitForCompletion()
        {
            @event.WaitOne();
            return exception;
        }
    }
}