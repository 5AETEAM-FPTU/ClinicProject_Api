using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Features.Repositories.OnlinePayment.CreateNewOnlinePayment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayment.CreateNewOnlinePayment;

internal class CreateNewOnlinePaymentRepository : ICreateNewOnlinePaymentRepository
{
    private readonly ClinicContext _context;

    public CreateNewOnlinePaymentRepository(ClinicContext context)
    {
        _context = context;
    }
}
