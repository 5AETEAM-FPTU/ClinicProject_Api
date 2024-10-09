using Clinic.Domain.Features.Repositories.VNPays.CreatePaymentLink;
using Clinic.MySQL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.VNPays.CreatePaymentLink;

public class CreatePaymentLinkRepository : ICreatePaymentLinkRepository
{
    private readonly ClinicContext _context;

    public CreatePaymentLinkRepository(ClinicContext context)
    {
        _context = context;
    }
}
