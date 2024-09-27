using System;

namespace Clinic.MySQL.Data.DataSeeding;

/// <summary>
///     Represent set of constant.
/// </summary>
internal static class EnumConstant
{
    public sealed class User
    {
        public static readonly Guid AdminId = Guid.Parse(
            input: "1a6c3e77-4097-40e2-b447-f00d1f82cf78"
        );

        public static readonly Guid StaffId = Guid.Parse(
            input: "1a6c3e77-4097-40e2-b447-f00d1f82cf71"
        );
        public static readonly Guid DoctorId = Guid.Parse(
            input: "1a6c3e77-4097-40e2-b447-f00d1f82cf72"
        );
        public static readonly Guid UserId = Guid.Parse(
            input: "1a6c3e77-4097-40e2-b447-f00d1f82cf73"
        );
    }

    public sealed class Role
    {
        public static readonly Guid DoctorRole = Guid.Parse(
            input: "c39aa1ac-8ded-46be-870c-115b200b09fc"
        );
        public static readonly Guid AdminRole = Guid.Parse(
            input: "c8500b45-b134-4b60-85b7-8e6af1187e0a"
        );
        public static readonly Guid StaffRole = Guid.Parse(
            input: "c8500b41-b134-4b60-85b7-8e6af1187e0b"
        );
        public static readonly Guid PatienRole = Guid.Parse(
            input: "c8500b46-b134-4b60-85b7-8e6af1187e0c"
        );
    }

    public sealed class Position
    {
        public static readonly Guid DOCTOR_ID = new("a20fe992-e3fd-4b30-9af3-2f2d7f5b56ee");
        public static readonly Guid HEALTHCARESTAFF_ID =
            new("a6b2f7a6-5282-4087-9fc4-de280f0017b3");
        public static readonly Guid MASTERDOCTOR_ID = new("1502e11a-e8a1-4604-b1ef-6f206a801ea7");
    }

    public sealed class Gender
    {
        public static readonly Guid MALE = new("976e9fad-fc68-4ce9-99c3-7f4bfe6c6c76");
        public static readonly Guid FEMALE = new("a6283987-8954-4502-8638-d2dd96ff8913");
    }

    public sealed class RetreatmentType
    {
        public static readonly Guid ADDITIONAL_TESTS = new("537c6d2f-61cc-4475-8465-abdcd74ac60f");
        public static readonly Guid LONG_TERM = new("186ccab8-037a-4304-b12d-b2bcf4ede520");
    }

    public sealed class Specialty
    {
        public static readonly Guid CARDIOLOGY = new("b1a0fe09-d214-4fda-900f-1e8d8fa1b5a7");
        public static readonly Guid INFECTIOUS_DISEASES =
            new("b2b0fe09-d214-4fda-900f-1e8d8fa1b5a8");
        public static readonly Guid GENERAL_INTERNAL_MEDICINE =
            new("c3c0fe09-d214-4fda-900f-1e8d8fa1b5a9");
        public static readonly Guid GASTROENTEROLOGY = new("d4d0fe09-d214-4fda-900f-1e8d8fa1b5aa");
        public static readonly Guid UROLOGY = new("e5e0fe09-d214-4fda-900f-1e8d8fa1b5ab");
        public static readonly Guid RHEUMATOLOGY = new("f6f0fe09-d214-4fda-900f-1e8d8fa1b5ac");
    }

    public sealed class AppointmentStatus
    {
        public static readonly Guid PENDING = new("e5c2184a-eb52-4d2c-aa97-f1872dcd77e3");
        public static readonly Guid COMPLETED = new("3b5ffbb7-6986-4ea6-a092-cd95edcae6dc");
        public static readonly Guid NO_SHOW = new("10d8cf8e-d83f-45ad-a2be-74342e6483eb");
    }

    public sealed class MedicineGroup { }

    public sealed class MedicineType { }
}
