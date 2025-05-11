﻿// <copyright file="DefaultMediatrEventMiddleware{TCommand}.cs" company="Maomi">
// Copyright (c) Maomi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// Github link: https://github.com/whuanle/Maomi.MQ
// </copyright>

using Maomi.MQ.EventBus;
using MediatR;

namespace Maomi.MQ.MediatR;

/// <summary>
/// The default Mediator message consumer middleware.
/// </summary>
/// <typeparam name="TCommand">Command.</typeparam>
public class DefaultMediatrEventMiddleware<TCommand> : IEventMiddleware<TCommand>
    where TCommand : class, IRequest
{
    /// <inheritdoc/>
    public Task ExecuteAsync(MessageHeader messageHeader, TCommand message, EventHandlerDelegate<TCommand> next)
    {
        return next(messageHeader, message, CancellationToken.None);
    }

    /// <inheritdoc/>
    public Task FaildAsync(MessageHeader messageHeader, Exception ex, int retryCount, TCommand? message)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<ConsumerState> FallbackAsync(MessageHeader messageHeader, TCommand? message, Exception? ex)
    {
        return Task.FromResult(ConsumerState.Ack);
    }
}