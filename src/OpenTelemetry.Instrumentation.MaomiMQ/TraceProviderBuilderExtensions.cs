// <copyright file="TraceProviderBuilderExtensions.cs" company="Maomi">
// Copyright (c) Maomi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// Github link: https://github.com/whuanle/Maomi.MQ
// </copyright>

using OpenTelemetry.Instrumentation.MaomiMQ;
using OpenTelemetry.Internal;
using OpenTelemetry.Metrics;
using System;

// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace;

/// <summary>
/// Extension methods to simplify registering of dependency instrumentation.
/// </summary>
public static class TraceProviderBuilderExtensions
{
    /// <summary>
    /// Enables the Maomi.MQ automatic data collection for Maomi.MQ.
    /// </summary>
    /// <param name="builder"><see cref="TraceProviderBuilderExtensions"/> being configured.</param>
    /// <returns>The instance of <see cref="TraceProviderBuilderExtensions"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddMaomiMQInstrumentation(
        this TracerProviderBuilder builder) => AddMaomiMQInstrumentation(builder, configure: null);

    /// <summary>
    /// Enables the  Maomi.MQ automatic data collection for Maomi.MQ.
    /// </summary>
    /// <param name="builder"><see cref="TraceProviderBuilderExtensions"/> being configured.</param>
    /// <param name="configure">Maomi.MQ configuration options.</param>
    /// <returns>The instance of <see cref="TraceProviderBuilderExtensions"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddMaomiMQInstrumentation(
        this TracerProviderBuilder builder,
        Action<MaomiMQInstrumentationOptions>? configure)
    {
        Guard.ThrowIfNull(builder);

        var options = new MaomiMQInstrumentationOptions();
        configure?.Invoke(options);

        builder.AddInstrumentation(() => new Instrumentation.MaomiMQ.MaomiMQInstrumentation(options));

        builder.AddSource(Maomi.MQ.Diagnostics.DiagnosticName.MaomiMQ);
        builder.AddSource(Maomi.MQ.Diagnostics.DiagnosticName.EventBus);
        builder.AddSource(Maomi.MQ.Diagnostics.DiagnosticName.Publisher);
        builder.AddSource(Maomi.MQ.Diagnostics.DiagnosticName.Consumer);

        foreach (var item in options.Sources)
        {
            builder.AddSource(item);
        }

        return builder;
    }

    /// <summary>
    /// Enables the Maomi.MQ automatic data collection for Maomi.MQ.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>The instance of <see cref="MeterProviderBuilder"/> to chain the calls.</returns>
    public static MeterProviderBuilder AddMaomiMQInstrumentation(this MeterProviderBuilder builder)
    {
        OpenTelemetry.Internal.Guard.ThrowIfNull(builder, "builder");
        return builder.ConfigureMeters();
    }

    /// <summary>
    /// Configure meters.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>The instance of <see cref="MeterProviderBuilder"/> to chain the calls.</returns>
    internal static MeterProviderBuilder ConfigureMeters(this MeterProviderBuilder builder)
    {
        return builder
            .AddMeter("Maomi.MQ.Publisher")
            .AddMeter("Maomi.MQ.Consumer");
    }
}
