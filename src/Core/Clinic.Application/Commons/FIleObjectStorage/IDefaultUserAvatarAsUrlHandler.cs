using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Commons.FIleObjectStorage;

/// <summary>
///     Represent the handler for default user avatar as url.
/// </summary>
public interface IDefaultUserAvatarAsUrlHandler
{
    /// <summary>
    ///     Get the default user avatar as url.
    /// </summary>
    string Get();
}
