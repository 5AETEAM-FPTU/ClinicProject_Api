﻿namespace Clinic.Application.Commons.SMS;

/// <summary>
///     Interface for SMS Handler
/// </summary>
public interface ISmsHandler
{
    /// <summary>
    ///     Send SMS Notifications.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    bool SendNotification(string to, string body);
}