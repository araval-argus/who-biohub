using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{

    internal static Laboratory[] Laboratories => new Laboratory[]
    {
        new()
        {
            Id = LaboratoryId1,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Italy")?.Id,
            Name = "National Institute for Infectious Diseases Lazzaro Spallanzani",
            Abbreviation = "INMI",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 4")?.Id,
            Address = "National Institute for Infectious Diseases L. Spallanzani, Laboratory of Virology Baglivi Pavillion, Via Giacomo Folchi 6, 00149, Rome, Italy",
            IsActive = true,
            Latitude = 41.86689897,
            Longitude = 12.45446176,
        },
        new()
        {
            Id = LaboratoryId2,
            CountryId = Countries.FirstOrDefault(x => x.Name == "South Africa")?.Id,
            Name = "National Institute for Communicable Diseases",
            Abbreviation = "NICD",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 4")?.Id,
            Address = "NHLS/NICD, Centre for Respiratory Disease and Meningitis (CRDM), Lower North Wing, SAVP building, 1 Modderfontein Rd, Sandringham, Johannesburg, 2131 South Africa",
            IsActive = true,
            Latitude = -26.13238549,
            Longitude = 28.11653965,
        },
        new()
        {
            Id = LaboratoryId3,
            CountryId = Countries.FirstOrDefault(x => x.Name == "India")?.Id,
            Name = "National Institute of Virology",
            Abbreviation = "ICMR",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 4")?.Id,
            Address = "ICMR - National Institute of Virology, Indian Council of Medical Research, Ministry of Health & Family Welfare, 20 A Dr Ambedkar Road, Pune, 411001, India",
            IsActive = true,
            Latitude = 18.52066419,
            Longitude = 73.87267965,
        },
        new()
        {
            Id = LaboratoryId4,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Thailand")?.Id,
            Name = "Department of Medical Sciences, Ministry of Public Health",
            Abbreviation = "DMSC",
            BSLLevelId = null,
            Address = "88/7 Tiwanon Road, Nonthaburi 11000, Thailand",
            IsActive = true,
            Latitude = 13.86098661,
            Longitude = 100.5214554,
        },
        new()
        {
            Id = LaboratoryId5,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Thailand")?.Id,
            Name = "Armed Forces Research Institute of Medical Science",
            Abbreviation = "AFRIMS",
            BSLLevelId = null,
            Address = "American Embassy Bangkok, ATTN: USAMD-AFRIMS Virology, 120 Wireless Road, Pathumwan, Bangkok, 10400, Thailand",
            IsActive = true,
            Latitude = 13.76644,
            Longitude = 100.5357774,
        },
        new()
        {
            Id = LaboratoryId6,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Japan")?.Id,
            Name = "Institute of Tropical Medicine, Nagasaki University",
            Abbreviation = "NEKKEN",
            BSLLevelId = null,
            Address = "Sakamoto 1-12-4, Nagasaki-shi, 852-8523, Nagasaki, Japan",
            IsActive = true,
            Latitude = 32.77296555,
            Longitude = 129.8693128,
        },
        new()
        {
            Id = LaboratoryId7,
            CountryId = Countries.FirstOrDefault(x => x.Name == "India")?.Id,
            Name = "Serum Institute of India",
            Abbreviation = "SII",
            BSLLevelId = null,
            Address = "Serum Institute of India, 212/2, Hadapsar, Off Soli Poonawalla Road, Pune, 411028, India",
            IsActive = true,
            Latitude = 18.50522896,
            Longitude = 73.94533807,
        },
        new()
        {
            Id = LaboratoryId8,
            CountryId = Countries.FirstOrDefault(x => x.Name == "South Africa")?.Id,
            Name = "The Biovac Institute",
            Abbreviation = "BIOVAC",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 3")?.Id,
            Address = "15 Alexandra Rd, Pinelands, Cape Town, 7405, South Africa",
            IsActive = true,
            Latitude = -33.93735702,
            Longitude = 18.48997546,
        },
        new()
        {
            Id = LaboratoryId9,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Luxembourg")?.Id,
            Name = "Laboratoire national de santé",
            Abbreviation = "LNS",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 3")?.Id,
            Address = "Laboratoire National de Sante, 1 Rue Louis Rech, Department of Microbiology, Dudelange L-3555, Luxembourg",
            IsActive = true,
            Latitude = 49.49890298,
            Longitude = 6.085378159,
        },
        new()
        {
            Id = LaboratoryId10,
            CountryId = Countries.FirstOrDefault(x => x.Name == "United Kingdom")?.Id,
            Name = "UK Health Security Agency",
            Abbreviation = "UKHSA",
            BSLLevelId = null,
            Address = "UKHSA, Manor Farm Road, Porton Down, Department: Dispatch, Salisbury, WILTSHIRE. SP4 0HG, United Kingdom",
            IsActive = true,
            Latitude = 51.12968075,
            Longitude = -1.708808928,
        },
        new()
        {
            Id = LaboratoryId11,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Portugal")?.Id,
            Name = "Instituto de Medicina Molecular",
            Abbreviation = "IMM",
            BSLLevelId = BSLLevels.FirstOrDefault(x => x.Name == "BSL 3")?.Id,
            Address = "Ediffeio Egas Moniz, Av. Professor Egas Moniz,1649-028 Lisboa, Portugal",
            IsActive = true,
            Latitude = 38.74653736,
            Longitude = -9.161395243,
        },
        new()
        {
            Id = LaboratoryId12,
            CountryId = Countries.FirstOrDefault(x => x.Name == "South Africa")?.Id,
            Name = "African Health Research Institute",
            Abbreviation = "AHRI",
            BSLLevelId = null,
            Address = "",
            IsActive = true,
            Latitude = 29.8818762,
            Longitude = 30.9876754,
        },
        new()
        {
            Id = LaboratoryId13,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Germany")?.Id,
            Name = "National Consultant Laboratory for Coronaviruses",
            Abbreviation = "",
            BSLLevelId = null,
            Address = "",
            IsActive = true,
            Latitude = 52.52703536,
            Longitude = 13.37799462,
        },
        new()
        {
            Id = LaboratoryId14,
            CountryId = Countries.FirstOrDefault(x => x.Name == "Germany")?.Id,
            Name = "Robert Koch-Institute",
            Abbreviation = "RKI",
            BSLLevelId = null,
            Address = "",
            IsActive = true,
            Latitude = 52.5434477,
            Longitude = 13.3397038,
        },
    };

    internal static Guid LaboratoryId1 => Guid.Parse("11709f57-91d7-4b16-af4c-1ff67401cd34");
    internal static Guid LaboratoryId2 => Guid.Parse("9a78a4e7-353e-4783-8051-b2acb97a246f");
    internal static Guid LaboratoryId3 => Guid.Parse("dcf22962-d3ba-4371-9279-8da6290c0966");
    internal static Guid LaboratoryId4 => Guid.Parse("a7b8bc84-e51f-4c8f-885c-4f1bff458a8b");
    internal static Guid LaboratoryId5 => Guid.Parse("9a998adb-1fb0-4c2e-b738-9a610e09aebf");
    internal static Guid LaboratoryId6 => Guid.Parse("c4654f30-299d-4c82-9664-a6024e54277d");
    internal static Guid LaboratoryId7 => Guid.Parse("ada979e7-b72e-4555-a557-4edf1bf80c80");
    internal static Guid LaboratoryId8 => Guid.Parse("91784c32-3abd-4899-9d46-2016e678669a");
    internal static Guid LaboratoryId9 => Guid.Parse("d328a70c-ce70-4184-a96f-eb43fab91cbe");
    internal static Guid LaboratoryId10 => Guid.Parse("6feeb702-9c66-4344-bb3e-93886286e440");
    internal static Guid LaboratoryId11 => Guid.Parse("059490db-fa72-49b5-b73d-c12ba4c82023");
    internal static Guid LaboratoryId12 => Guid.Parse("57d6c92f-08c5-44f5-a0a7-8309ed49b11e");
    internal static Guid LaboratoryId13 => Guid.Parse("4e5a8cfc-5f3a-4115-8be8-abe63517d0d6");
    internal static Guid LaboratoryId14 => Guid.Parse("f165dc4c-68f7-4dfa-a44b-17913d283a3e");


    private async Task AddLaboratories(CancellationToken cancellationToken)
    {
        await _db.AddRangeAsync(Laboratories, cancellationToken: cancellationToken);
    }
}
