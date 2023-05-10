using Hadron.Identifier;
using System.Collections.Immutable;

namespace Hadron;

public record GeneralExceptionId : PrefixedUlid {
    public const int TotalLength = 36;
    public const string Prefix = "exception";
    public const string Separator = "_";

    public override string PrefixWithSeparator => "exception_";

    public static GeneralExceptionId CreateNew() => NewPrefixedUlid<GeneralExceptionId>();

    public static GeneralExceptionId Parse(string id) {
        return Parse<GeneralExceptionId>(id);
    }
}

/// <summary>
///  Represents the general error.
/// </summary>
public class GeneralException : Exception {
    public string? DisplayMessage { get; }

    public PrefixedUlid EventId { get; }

    public object? Payload { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneralException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="displayMessage"></param>
    /// <param name="payload"></param>
    /// <param name="eventId"></param>
    /// <param name="exception"></param>
    public GeneralException(
        string message,
        string? displayMessage = null,
        object? payload = null,
        PrefixedUlid? eventId = null,
        Exception? exception = null
    ) : base(
        message,
        exception
    ) {
        Payload = payload;
        DisplayMessage = displayMessage;
        EventId = eventId ?? GeneralExceptionId.CreateNew();

        // Additional data
        Data.Add(nameof(EventId), EventId.ToString());
        Data.Add(nameof(DisplayMessage), displayMessage);
        Data.Add(nameof(Payload), payload);
    }

    public static GeneralException WithDisplayMessage(
        string displayMessage,
        PrefixedUlid? eventId = null
    ) {
        return new GeneralException<object>(
            null,
            displayMessage,
            displayMessage,
            eventId ?? GeneralExceptionId.CreateNew()
        );
    }

    public static GeneralException<TPayload> WithDisplayMessage<TPayload>(
        TPayload exceptionType,
        string displayMessage,
        PrefixedUlid? eventId = null
    ) {
        return new(
            exceptionType,
            displayMessage,
            displayMessage,
            eventId ?? GeneralExceptionId.CreateNew()
        );
    }

    public static GeneralException WithMessage(
        string message,
        string? displayMessage,
        Exception? ex = null,
        PrefixedUlid? eventId = null
    ) {
        return new GeneralException<object>(
            null,
            message,
            displayMessage,
            eventId ?? GeneralExceptionId.CreateNew(),
            ex!
        );
    }

    public static GeneralException<TPayload> WithMessage<TPayload>(
        TPayload exceptionType,
        string message,
        string? displayMessage,
        Exception? ex = null,
        PrefixedUlid? eventId = null
    ) {
        return new(
            exceptionType,
            message,
            displayMessage,
            eventId ?? GeneralExceptionId.CreateNew(),
            ex!
        );
    }

    public static GeneralException WithException(
        Exception ex,
        string? message = null,
        string? displayMessage = null
    ) {
        return ex switch {
            GeneralException ge => WithChild(
                ge,
                ge.Message,
                ge.DisplayMessage
            ),
            _ => WithMessage(
                message ?? ex.Message,
                displayMessage,
                ex
            )
        };
    }


    public static GeneralException WithChild(
        GeneralException ex,
        string message,
        string? displayMessage = null
    ) {
        return (message, displayMessage) is (null, null)
            ? ex
            : new GeneralException<object>(
                null,
                message ?? ex.Message,
                displayMessage,
                ex.EventId,
                ex
            );
    }

    public static GeneralException<TPayload> WithChild<TPayload>(
        TPayload payload,
        GeneralException ex,
        string message,
        string? displayMessage = null
    ) {
        return new(
            payload,
            message,
            displayMessage,
            ex.EventId,
            ex
        );
    }

    public ImmutableArray<string> FluttenDisplayMessages() {
        var messages = ArrayOf<string>().ToBuilder();

        Exception? ex = this;
        while (ex is not null) {
            if (ex is GeneralException { DisplayMessage: { } message and not "" }) {
                messages.Add(message);
            }

            ex = ex.InnerException;
        }

        return messages.ToImmutable();
    }
}

/// <summary>
/// Represents the general error.
/// </summary>
/// <typeparam name="TError">Error info.</typeparam>
public class GeneralException<TPayload> : GeneralException {
    public GeneralException(
        TPayload? payload,
        string message,
        string? displayMessage = null,
        PrefixedUlid? eventId = null,
        Exception? error = null
    ) : base(
        message,
        displayMessage,
        payload,
        eventId,
        error
    ) { }

    public new TPayload? Payload => (TPayload?)base.Payload;
}