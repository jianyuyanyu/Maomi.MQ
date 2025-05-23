﻿// <copyright file="FCommandAttribute.cs" company="Maomi">
// Copyright (c) Maomi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// Github link: https://github.com/whuanle/Maomi.MQ
// </copyright>

namespace Maomi.MQ;

/// <summary>
/// FastEndpoints message options.<br />
/// FastEndpoints 消息配置.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class FastEndpointsCommandAttribute : Attribute, IConsumerOptions
{
    /// <inheritdoc />
    public string Queue { get; set; }

    /// <inheritdoc />
    public string? DeadExchange { get; set; }

    /// <inheritdoc />
    public string? DeadRoutingKey { get; set; }

    /// <inheritdoc />
    public ushort Qos { get; set; } = 100;

    /// <inheritdoc />
    public bool RetryFaildRequeue { get; set; } = true;

    /// <inheritdoc />
    public int Expiration { get; set; }

    /// <inheritdoc />
    public AutoQueueDeclare AutoQueueDeclare { get; set; }

    /// <inheritdoc />
    public string? BindExchange { get; set; }

    /// <inheritdoc />
    public string? ExchangeType { get; set; }

    /// <inheritdoc />
    public string? RoutingKey { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FastEndpointsCommandAttribute"/> class.
    /// </summary>
    /// <param name="queue">Queue name.</param>
    public FastEndpointsCommandAttribute(string queue)
    {
        ArgumentException.ThrowIfNullOrEmpty(queue, nameof(queue));
        Queue = queue;
    }

    /// <inheritdoc />
    public IConsumerOptions Clone()
    {
        var newOptions = new FastEndpointsCommandAttribute(this.Queue);
        newOptions.CopyFrom(this);
        return newOptions;
    }

    /// <inheritdoc />
    public void CopyFrom(IConsumerOptions options)
    {
        this.Queue = options.Queue;
        this.DeadExchange = options.DeadExchange;
        this.DeadRoutingKey = options.DeadRoutingKey;
        this.Qos = options.Qos;
        this.RetryFaildRequeue = options.RetryFaildRequeue;
        this.Expiration = options.Expiration;
        this.AutoQueueDeclare = options.AutoQueueDeclare;
        this.BindExchange = options.BindExchange;
        this.ExchangeType = options.ExchangeType;
        this.RoutingKey = options.RoutingKey;
    }
}
