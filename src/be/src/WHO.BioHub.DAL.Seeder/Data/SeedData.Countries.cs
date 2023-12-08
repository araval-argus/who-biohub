﻿using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static Country[] Countries => AllCountries.DistinctBy(x => x.Uncode).ToArray();

    private static Country[] AllCountries => new Country[]
    {
        new()
        {
            Id = Guid.Parse("f6213bcf-60e7-4a70-a48b-b8519ddd737b"),
            Uncode = "004",
            Name = "Afghanistan",
            FullName = "Afghanistan",
            Iso2 = "AF",
            Iso3 = "AFG",
            Latitude = 33,
            Longitude = 65,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("64685f1c-5e02-4940-937b-3e3bcfe27be6"),
            Uncode = "008",
            Name = "Albania",
            FullName = "Albania",
            Iso2 = "AL",
            Iso3 = "ALB",
            Latitude = 41,
            Longitude = 20,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3a985212-ce8f-48b7-85cb-2ed36389ec61"),
            Uncode = "012",
            Name = "Algeria",
            FullName = "Algeria",
            Iso2 = "DZ",
            Iso3 = "DZA",
            Latitude = 28,
            Longitude = 3,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8b6bdad6-52a0-4e13-b274-e56e163d1f17"),
            Uncode = "016",
            Name = "American Samoa",
            FullName = "American Samoa",
            Iso2 = "AS",
            Iso3 = "ASM",
            Latitude = -14.3333,
            Longitude = -170,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5bb89f90-b3aa-4bb6-a8eb-a321e26b9a9a"),
            Uncode = "020",
            Name = "Andorra",
            FullName = "Andorra",
            Iso2 = "AD",
            Iso3 = "AND",
            Latitude = 42.5,
            Longitude = 1.6,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9b2eaaa0-935f-4d64-8116-65f34a15b462"),
            Uncode = "024",
            Name = "Angola",
            FullName = "Angola",
            Iso2 = "AO",
            Iso3 = "AGO",
            Latitude = -12.5,
            Longitude = 18.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("89e9a139-ac34-45d1-9d34-44a41177ae48"),
            Uncode = "660",
            Name = "Anguilla",
            FullName = "Anguilla",
            Iso2 = "AI",
            Iso3 = "AIA",
            Latitude = 18.25,
            Longitude = -63.1667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("791673e4-b2a2-473c-898a-92da1951e660"),
            Uncode = "010",
            Name = "Antarctica",
            FullName = "Antarctica",
            Iso2 = "AQ",
            Iso3 = "ATA",
            Latitude = -90,
            Longitude = 0,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3ce6f8c5-8bea-43cd-9b63-a78703cffe64"),
            Uncode = "028",
            Name = "Antigua and Barbuda",
            FullName = "Antigua and Barbuda",
            Iso2 = "AG",
            Iso3 = "ATG",
            Latitude = 17.05,
            Longitude = -61.8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("909af0bd-91f1-47e5-83e6-67da8a1cfbf8"),
            Uncode = "032",
            Name = "Argentina",
            FullName = "Argentina",
            Iso2 = "AR",
            Iso3 = "ARG",
            Latitude = -34,
            Longitude = -64,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("cee43511-f1db-4a3b-bed0-e2df8f59ef87"),
            Uncode = "051",
            Name = "Armenia",
            FullName = "Armenia",
            Iso2 = "AM",
            Iso3 = "ARM",
            Latitude = 40,
            Longitude = 45,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("071ef1a0-02af-4ccc-8c8b-34e8b7bc1eb4"),
            Uncode = "533",
            Name = "Aruba",
            FullName = "Aruba",
            Iso2 = "AW",
            Iso3 = "ABW",
            Latitude = 12.5,
            Longitude = -69.9667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4e7ff559-748e-456f-a0cf-93584e2c376d"),
            Uncode = "036",
            Name = "Australia",
            FullName = "Australia",
            Iso2 = "AU",
            Iso3 = "AUS",
            Latitude = -27,
            Longitude = 133,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("cf4bbd55-63fa-469d-82c0-c372f13a2056"),
            Uncode = "040",
            Name = "Austria",
            FullName = "Austria",
            Iso2 = "AT",
            Iso3 = "AUT",
            Latitude = 47.3333,
            Longitude = 13.3333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("eaddbb9c-04ed-444c-a8ff-b0672ab7074e"),
            Uncode = "031",
            Name = "Azerbaijan",
            FullName = "Azerbaijan",
            Iso2 = "AZ",
            Iso3 = "AZE",
            Latitude = 40.5,
            Longitude = 47.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("aaf0c95e-0f1d-414f-bff7-72209c2f2d69"),
            Uncode = "044",
            Name = "Bahamas",
            FullName = "Bahamas",
            Iso2 = "BS",
            Iso3 = "BHS",
            Latitude = 24.25,
            Longitude = -76,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4131cd17-587c-4648-8a2f-90f66b4a250e"),
            Uncode = "048",
            Name = "Bahrain",
            FullName = "Bahrain",
            Iso2 = "BH",
            Iso3 = "BHR",
            Latitude = 26,
            Longitude = 50.55,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("2045724e-7fcb-4489-bb7d-c7b0c01e8e68"),
            Uncode = "050",
            Name = "Bangladesh",
            FullName = "Bangladesh",
            Iso2 = "BD",
            Iso3 = "BGD",
            Latitude = 24,
            Longitude = 90,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a5e02e1b-2817-4e4c-95df-4bc037589167"),
            Uncode = "052",
            Name = "Barbados",
            FullName = "Barbados",
            Iso2 = "BB",
            Iso3 = "BRB",
            Latitude = 13.1667,
            Longitude = -59.5333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8dfd4a05-4774-4254-9eb0-177111237082"),
            Uncode = "112",
            Name = "Belarus",
            FullName = "Belarus",
            Iso2 = "BY",
            Iso3 = "BLR",
            Latitude = 53,
            Longitude = 28,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("21860474-cf18-4168-ab06-e77269a57636"),
            Uncode = "056",
            Name = "Belgium",
            FullName = "Belgium",
            Iso2 = "BE",
            Iso3 = "BEL",
            Latitude = 50.8333,
            Longitude = 4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f8616173-f204-490e-b7fc-4f1529458da1"),
            Uncode = "084",
            Name = "Belize",
            FullName = "Belize",
            Iso2 = "BZ",
            Iso3 = "BLZ",
            Latitude = 17.25,
            Longitude = -88.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9df0e524-e6c1-4372-bc5d-07efd39d4092"),
            Uncode = "204",
            Name = "Benin",
            FullName = "Benin",
            Iso2 = "BJ",
            Iso3 = "BEN",
            Latitude = 9.5,
            Longitude = 2.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("0d9298bc-5e95-4cd3-97ca-9ed8bf97139c"),
            Uncode = "060",
            Name = "Bermuda",
            FullName = "Bermuda",
            Iso2 = "BM",
            Iso3 = "BMU",
            Latitude = 32.3333,
            Longitude = -64.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("38331f7f-92bb-415d-b7ff-d9dd1fd4dbcb"),
            Uncode = "064",
            Name = "Bhutan",
            FullName = "Bhutan",
            Iso2 = "BT",
            Iso3 = "BTN",
            Latitude = 27.5,
            Longitude = 90.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f0cf8f78-cbbf-4f4c-bd7d-4f12c9afbe26"),
            Uncode = "068",
            Name = "Bolivia",
            FullName = "Bolivia",
            Iso2 = "BO",
            Iso3 = "BOL",
            Latitude = -17,
            Longitude = -65,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("dfda95d4-e752-4b20-9356-67829aa69a21"),
            Uncode = "070",
            Name = "Bosnia and Herzegovina",
            FullName = "Bosnia and Herzegovina",
            Iso2 = "BA",
            Iso3 = "BIH",
            Latitude = 44,
            Longitude = 18,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6aec3730-b0fc-4f92-8d09-e7e869fad054"),
            Uncode = "072",
            Name = "Botswana",
            FullName = "Botswana",
            Iso2 = "BW",
            Iso3 = "BWA",
            Latitude = -22,
            Longitude = 24,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("41dd688a-d5c9-4566-800c-ec9acecc556d"),
            Uncode = "074",
            Name = "Bouvet Island",
            FullName = "Bouvet Island",
            Iso2 = "BV",
            Iso3 = "BVT",
            Latitude = -54.4333,
            Longitude = 3.4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("153c8d12-b867-4109-99f4-6e6dec9982d3"),
            Uncode = "076",
            Name = "Brazil",
            FullName = "Brazil",
            Iso2 = "BR",
            Iso3 = "BRA",
            Latitude = -10,
            Longitude = -55,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b1074fc7-d850-4468-a06f-a0779985a5a2"),
            Uncode = "086",
            Name = "British Indian Ocean Territory",
            FullName = "British Indian Ocean Territory",
            Iso2 = "IO",
            Iso3 = "IOT",
            Latitude = -6,
            Longitude = 71.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9ac0dbe9-aa7b-4759-8730-c759183842ac"),
            Uncode = "096",
            Name = "Brunei",
            FullName = "Brunei",
            Iso2 = "BN",
            Iso3 = "BRN",
            Latitude = 4.5,
            Longitude = 114.6667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7dd1f7ff-9e3c-451b-86c4-b4beb0be64ec"),
            Uncode = "100",
            Name = "Bulgaria",
            FullName = "Bulgaria",
            Iso2 = "BG",
            Iso3 = "BGR",
            Latitude = 43,
            Longitude = 25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("96dd02d5-54a9-42b4-b000-d3a72281653f"),
            Uncode = "854",
            Name = "Burkina Faso",
            FullName = "Burkina Faso",
            Iso2 = "BF",
            Iso3 = "BFA",
            Latitude = 13,
            Longitude = -2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b6d37085-3152-44d3-bc71-6dca400d3860"),
            Uncode = "108",
            Name = "Burundi",
            FullName = "Burundi",
            Iso2 = "BI",
            Iso3 = "BDI",
            Latitude = -3.5,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b2c73a46-7c2f-442f-821b-ece19d8c1e86"),
            Uncode = "116",
            Name = "Cambodia",
            FullName = "Cambodia",
            Iso2 = "KH",
            Iso3 = "KHM",
            Latitude = 13,
            Longitude = 105,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("564e2a13-fdf6-4568-984c-8905681bd733"),
            Uncode = "120",
            Name = "Cameroon",
            FullName = "Cameroon",
            Iso2 = "CM",
            Iso3 = "CMR",
            Latitude = 6,
            Longitude = 12,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6723e89b-56e4-41bc-a53c-508b2bee5e66"),
            Uncode = "124",
            Name = "Canada",
            FullName = "Canada",
            Iso2 = "CA",
            Iso3 = "CAN",
            Latitude = 60,
            Longitude = -95,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7ce5635f-12cf-4e6e-ab8f-81e3fa830443"),
            Uncode = "132",
            Name = "Cape Verde",
            FullName = "Cape Verde",
            Iso2 = "CV",
            Iso3 = "CPV",
            Latitude = 16,
            Longitude = -24,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("63199a23-986d-4444-a899-14d95df5c074"),
            Uncode = "136",
            Name = "Cayman Islands",
            FullName = "Cayman Islands",
            Iso2 = "KY",
            Iso3 = "CYM",
            Latitude = 19.5,
            Longitude = -80.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("66e303f3-79b5-414d-8a82-6b9b5d5e3f35"),
            Uncode = "140",
            Name = "Central African Republic",
            FullName = "Central African Republic",
            Iso2 = "CF",
            Iso3 = "CAF",
            Latitude = 7,
            Longitude = 21,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b059d961-c9f0-45f9-be4b-303edbfe82ab"),
            Uncode = "148",
            Name = "Chad",
            FullName = "Chad",
            Iso2 = "TD",
            Iso3 = "TCD",
            Latitude = 15,
            Longitude = 19,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e0f1d358-6a67-4ad0-a4cd-4b0477ad1a5e"),
            Uncode = "152",
            Name = "Chile",
            FullName = "Chile",
            Iso2 = "CL",
            Iso3 = "CHL",
            Latitude = -30,
            Longitude = -71,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e72733bc-42ff-4f6a-97a0-08aec61b556b"),
            Uncode = "156",
            Name = "China",
            FullName = "China",
            Iso2 = "CN",
            Iso3 = "CHN",
            Latitude = 35,
            Longitude = 105,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7b26898c-ebb5-453b-9aef-05d34c77bf3a"),
            Uncode = "162",
            Name = "Christmas Island",
            FullName = "Christmas Island",
            Iso2 = "CX",
            Iso3 = "CXR",
            Latitude = -10.5,
            Longitude = 105.6667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6029d35b-c898-47df-903d-3d8dcfeaade7"),
            Uncode = "166",
            Name = "Cocos (Keeling) Islands",
            FullName = "Cocos (Keeling) Islands",
            Iso2 = "CC",
            Iso3 = "CCK",
            Latitude = -12.5,
            Longitude = 96.8333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d97ca5be-0ef7-409c-8630-aa88ca00ffbe"),
            Uncode = "170",
            Name = "Colombia",
            FullName = "Colombia",
            Iso2 = "CO",
            Iso3 = "COL",
            Latitude = 4,
            Longitude = -72,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("97c2b08e-8a19-468e-aeec-0c0e15164c78"),
            Uncode = "174",
            Name = "Comoros",
            FullName = "Comoros",
            Iso2 = "KM",
            Iso3 = "COM",
            Latitude = -12.1667,
            Longitude = 44.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("352889a4-1785-4807-b859-1e95a5a7c82d"),
            Uncode = "178",
            Name = "Congo",
            FullName = "Congo",
            Iso2 = "CG",
            Iso3 = "COG",
            Latitude = -1,
            Longitude = 15,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9764a505-207e-40de-acfd-ee0655552245"),
            Uncode = "180",
            Name = "Congo, the Democratic Republic of the",
            FullName = "Congo, the Democratic Republic of the",
            Iso2 = "CD",
            Iso3 = "COD",
            Latitude = 0,
            Longitude = 25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5d2dad62-92ba-4b3e-8e42-6e9b752a99e7"),
            Uncode = "184",
            Name = "Cook Islands",
            FullName = "Cook Islands",
            Iso2 = "CK",
            Iso3 = "COK",
            Latitude = -21.2333,
            Longitude = -159.7667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d1997679-d823-4274-8d64-e4634ba04366"),
            Uncode = "188",
            Name = "Costa Rica",
            FullName = "Costa Rica",
            Iso2 = "CR",
            Iso3 = "CRI",
            Latitude = 10,
            Longitude = -84,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a0c51991-9faf-4d1f-9a95-98e9304c8313"),
            Uncode = "384",
            Name = "Côte d'Ivoire",
            FullName = "Côte d'Ivoire",
            Iso2 = "CI",
            Iso3 = "CIV",
            Latitude = 8,
            Longitude = -5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("aaa3bccd-bfdc-4179-ae76-04f2c7f436b1"),
            Uncode = "191",
            Name = "Croatia",
            FullName = "Croatia",
            Iso2 = "HR",
            Iso3 = "HRV",
            Latitude = 45.1667,
            Longitude = 15.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("dd19b1ba-fafa-475f-b8f5-7bbaaf427939"),
            Uncode = "192",
            Name = "Cuba",
            FullName = "Cuba",
            Iso2 = "CU",
            Iso3 = "CUB",
            Latitude = 21.5,
            Longitude = -80,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a860b3a1-a0e7-41ea-b79c-181f7b3ce168"),
            Uncode = "196",
            Name = "Cyprus",
            FullName = "Cyprus",
            Iso2 = "CY",
            Iso3 = "CYP",
            Latitude = 35,
            Longitude = 33,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7bcda025-d2a2-4cd8-86c9-c801f9d4d394"),
            Uncode = "203",
            Name = "Czech Republic",
            FullName = "Czech Republic",
            Iso2 = "CZ",
            Iso3 = "CZE",
            Latitude = 49.75,
            Longitude = 15.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4cf796c3-91e7-4630-9f2b-d04007968f18"),
            Uncode = "208",
            Name = "Denmark",
            FullName = "Denmark",
            Iso2 = "DK",
            Iso3 = "DNK",
            Latitude = 56,
            Longitude = 10,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("615a19ae-84c8-401d-961c-b7a8872eb162"),
            Uncode = "262",
            Name = "Djibouti",
            FullName = "Djibouti",
            Iso2 = "DJ",
            Iso3 = "DJI",
            Latitude = 11.5,
            Longitude = 43,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("39d74e34-f761-4d4a-9448-5ab010f3c526"),
            Uncode = "212",
            Name = "Dominica",
            FullName = "Dominica",
            Iso2 = "DM",
            Iso3 = "DMA",
            Latitude = 15.4167,
            Longitude = -61.3333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8f9013cc-0212-4d7a-b173-d8211f7d2ce6"),
            Uncode = "214",
            Name = "Dominican Republic",
            FullName = "Dominican Republic",
            Iso2 = "DO",
            Iso3 = "DOM",
            Latitude = 19,
            Longitude = -70.6667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4d2acbce-d4e0-456f-ba4a-44c9b7604765"),
            Uncode = "218",
            Name = "Ecuador",
            FullName = "Ecuador",
            Iso2 = "EC",
            Iso3 = "ECU",
            Latitude = -2,
            Longitude = -77.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("327571dc-1ea3-497f-b171-7ebcb73cc29c"),
            Uncode = "818",
            Name = "Egypt",
            FullName = "Egypt",
            Iso2 = "EG",
            Iso3 = "EGY",
            Latitude = 27,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b217c0d5-be61-4c27-9674-82793dcf1196"),
            Uncode = "222",
            Name = "El Salvador",
            FullName = "El Salvador",
            Iso2 = "SV",
            Iso3 = "SLV",
            Latitude = 13.8333,
            Longitude = -88.9167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6393c33d-68f7-43bc-a22d-2f848b7c1a42"),
            Uncode = "226",
            Name = "Equatorial Guinea",
            FullName = "Equatorial Guinea",
            Iso2 = "GQ",
            Iso3 = "GNQ",
            Latitude = 2,
            Longitude = 10,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("182121bb-0725-4fda-96d8-b12fc5c7bffd"),
            Uncode = "232",
            Name = "Eritrea",
            FullName = "Eritrea",
            Iso2 = "ER",
            Iso3 = "ERI",
            Latitude = 15,
            Longitude = 39,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7136339d-b826-40cf-aad9-467b1b96803e"),
            Uncode = "233",
            Name = "Estonia",
            FullName = "Estonia",
            Iso2 = "EE",
            Iso3 = "EST",
            Latitude = 59,
            Longitude = 26,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("37cb5599-4550-426b-8a0d-050b9d56fab2"),
            Uncode = "231",
            Name = "Ethiopia",
            FullName = "Ethiopia",
            Iso2 = "ET",
            Iso3 = "ETH",
            Latitude = 8,
            Longitude = 38,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("981ed1d7-d76a-4663-94bd-0621f82b36ae"),
            Uncode = "238",
            Name = "Falkland Islands (Malvinas)",
            FullName = "Falkland Islands (Malvinas)",
            Iso2 = "FK",
            Iso3 = "FLK",
            Latitude = -51.75,
            Longitude = -59,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("44954b2a-f903-4d52-a0cb-2c2f6305e89b"),
            Uncode = "234",
            Name = "Faroe Islands",
            FullName = "Faroe Islands",
            Iso2 = "FO",
            Iso3 = "FRO",
            Latitude = 62,
            Longitude = -7,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f924dc24-36c0-429f-8a18-51dd1c6f8eae"),
            Uncode = "242",
            Name = "Fiji",
            FullName = "Fiji",
            Iso2 = "FJ",
            Iso3 = "FJI",
            Latitude = -18,
            Longitude = 175,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b1aca013-7d4e-41c0-a864-e79c1b9f8e40"),
            Uncode = "246",
            Name = "Finland",
            FullName = "Finland",
            Iso2 = "FI",
            Iso3 = "FIN",
            Latitude = 64,
            Longitude = 26,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("dbc6cf08-ebb6-4807-bb45-ffe6bf1015b8"),
            Uncode = "250",
            Name = "France",
            FullName = "France",
            Iso2 = "FR",
            Iso3 = "FRA",
            Latitude = 46,
            Longitude = 2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("db01ebad-ceaa-4623-b32f-51bd55783730"),
            Uncode = "254",
            Name = "French Guiana",
            FullName = "French Guiana",
            Iso2 = "GF",
            Iso3 = "GUF",
            Latitude = 4,
            Longitude = -53,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("41f1cebf-6bcb-401c-b597-a247464f0085"),
            Uncode = "258",
            Name = "French Polynesia",
            FullName = "French Polynesia",
            Iso2 = "PF",
            Iso3 = "PYF",
            Latitude = -15,
            Longitude = -140,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("27358df9-6b4c-4579-9759-635fa01877a0"),
            Uncode = "260",
            Name = "French Southern Territories",
            FullName = "French Southern Territories",
            Iso2 = "TF",
            Iso3 = "ATF",
            Latitude = -43,
            Longitude = 67,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3f04bd8a-6afa-4564-9869-e791b762f2ca"),
            Uncode = "266",
            Name = "Gabon",
            FullName = "Gabon",
            Iso2 = "GA",
            Iso3 = "GAB",
            Latitude = -1,
            Longitude = 11.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("ada19d67-1993-431a-8b11-31b87fc47835"),
            Uncode = "270",
            Name = "Gambia",
            FullName = "Gambia",
            Iso2 = "GM",
            Iso3 = "GMB",
            Latitude = 13.4667,
            Longitude = -16.5667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("906c4854-0da9-4ab3-b9f3-5da4221ac0cc"),
            Uncode = "268",
            Name = "Georgia",
            FullName = "Georgia",
            Iso2 = "GE",
            Iso3 = "GEO",
            Latitude = 42,
            Longitude = 43.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("961678c3-5929-41ef-bd18-4e9a35368e10"),
            Uncode = "276",
            Name = "Germany",
            FullName = "Germany",
            Iso2 = "DE",
            Iso3 = "DEU",
            Latitude = 51,
            Longitude = 9,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("37331232-7d52-4021-98f0-6e3a1f596c52"),
            Uncode = "288",
            Name = "Ghana",
            FullName = "Ghana",
            Iso2 = "GH",
            Iso3 = "GHA",
            Latitude = 8,
            Longitude = -2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("43664780-71aa-438c-ab2d-d75fd8c30736"),
            Uncode = "292",
            Name = "Gibraltar",
            FullName = "Gibraltar",
            Iso2 = "GI",
            Iso3 = "GIB",
            Latitude = 36.1833,
            Longitude = -5.3667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("43ce0e68-ada5-484d-bfdd-dc55255a3031"),
            Uncode = "300",
            Name = "Greece",
            FullName = "Greece",
            Iso2 = "GR",
            Iso3 = "GRC",
            Latitude = 39,
            Longitude = 22,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("0f4afd01-35e8-4b1c-9b19-4e7e056982e4"),
            Uncode = "304",
            Name = "Greenland",
            FullName = "Greenland",
            Iso2 = "GL",
            Iso3 = "GRL",
            Latitude = 72,
            Longitude = -40,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("96dfab4e-b1c5-431d-b0db-275a3a8d9adf"),
            Uncode = "308",
            Name = "Grenada",
            FullName = "Grenada",
            Iso2 = "GD",
            Iso3 = "GRD",
            Latitude = 12.1167,
            Longitude = -61.6667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a0146f78-752b-4659-9ab4-efc78ad5a7f1"),
            Uncode = "312",
            Name = "Guadeloupe",
            FullName = "Guadeloupe",
            Iso2 = "GP",
            Iso3 = "GLP",
            Latitude = 16.25,
            Longitude = -61.5833,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("095272d7-00e7-4251-a8cf-f366e510a650"),
            Uncode = "316",
            Name = "Guam",
            FullName = "Guam",
            Iso2 = "GU",
            Iso3 = "GUM",
            Latitude = 13.4667,
            Longitude = 144.7833,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("59683fa5-227b-4aa0-b7c9-5fe1dc7717c7"),
            Uncode = "320",
            Name = "Guatemala",
            FullName = "Guatemala",
            Iso2 = "GT",
            Iso3 = "GTM",
            Latitude = 15.5,
            Longitude = -90.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("30752f5b-a534-4af9-a608-5aacabb215d2"),
            Uncode = "831",
            Name = "Guernsey",
            FullName = "Guernsey",
            Iso2 = "GG",
            Iso3 = "GGY",
            Latitude = 49.5,
            Longitude = -2.56,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d2feaf13-3b98-49a7-8b0f-7e813c066b0d"),
            Uncode = "324",
            Name = "Guinea",
            FullName = "Guinea",
            Iso2 = "GN",
            Iso3 = "GIN",
            Latitude = 11,
            Longitude = -10,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f9f653c5-cd78-4b48-be46-509022a3fdb4"),
            Uncode = "624",
            Name = "Guinea-Bissau",
            FullName = "Guinea-Bissau",
            Iso2 = "GW",
            Iso3 = "GNB",
            Latitude = 12,
            Longitude = -15,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("70eab003-6e2b-4274-bf42-159002b90c94"),
            Uncode = "328",
            Name = "Guyana",
            FullName = "Guyana",
            Iso2 = "GY",
            Iso3 = "GUY",
            Latitude = 5,
            Longitude = -59,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("ac311cd5-04b3-4c70-af2b-5742c84c6416"),
            Uncode = "332",
            Name = "Haiti",
            FullName = "Haiti",
            Iso2 = "HT",
            Iso3 = "HTI",
            Latitude = 19,
            Longitude = -72.4167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("cad2aaa2-629a-44c3-baa0-b53a581848a1"),
            Uncode = "334",
            Name = "Heard Island and McDonald Islands",
            FullName = "Heard Island and McDonald Islands",
            Iso2 = "HM",
            Iso3 = "HMD",
            Latitude = -53.1,
            Longitude = 72.5167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("18fa7746-1b98-43fe-b952-2de6cd46c5ee"),
            Uncode = "336",
            Name = "Holy See (Vatican City State)",
            FullName = "Holy See (Vatican City State)",
            Iso2 = "VA",
            Iso3 = "VAT",
            Latitude = 41.9,
            Longitude = 12.45,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("93065247-39c6-41e6-bf07-404cb319e422"),
            Uncode = "340",
            Name = "Honduras",
            FullName = "Honduras",
            Iso2 = "HN",
            Iso3 = "HND",
            Latitude = 15,
            Longitude = -86.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4f7ab0fc-540a-4bf1-8cc4-55047524146a"),
            Uncode = "344",
            Name = "Hong Kong",
            FullName = "Hong Kong",
            Iso2 = "HK",
            Iso3 = "HKG",
            Latitude = 22.25,
            Longitude = 114.1667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d10e4c2e-36bf-4328-a168-b24e93507aca"),
            Uncode = "348",
            Name = "Hungary",
            FullName = "Hungary",
            Iso2 = "HU",
            Iso3 = "HUN",
            Latitude = 47,
            Longitude = 20,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("92628cb3-db05-4eac-90d1-42227730ed9d"),
            Uncode = "352",
            Name = "Iceland",
            FullName = "Iceland",
            Iso2 = "IS",
            Iso3 = "ISL",
            Latitude = 65,
            Longitude = -18,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("2b70ab8b-d4c1-44b7-b6c3-bd7ea4d75f4e"),
            Uncode = "356",
            Name = "India",
            FullName = "India",
            Iso2 = "IN",
            Iso3 = "IND",
            Latitude = 20,
            Longitude = 77,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3f7e5807-0909-4778-b547-177f8ae36d1b"),
            Uncode = "360",
            Name = "Indonesia",
            FullName = "Indonesia",
            Iso2 = "ID",
            Iso3 = "IDN",
            Latitude = -5,
            Longitude = 120,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("32da3ad1-325b-42e2-94d9-92f1670aaaf7"),
            Uncode = "364",
            Name = "Iran, Islamic Republic of",
            FullName = "Iran, Islamic Republic of",
            Iso2 = "IR",
            Iso3 = "IRN",
            Latitude = 32,
            Longitude = 53,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f65c0438-a144-4265-8bed-0819af9497e0"),
            Uncode = "368",
            Name = "Iraq",
            FullName = "Iraq",
            Iso2 = "IQ",
            Iso3 = "IRQ",
            Latitude = 33,
            Longitude = 44,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e15bca01-c021-42cd-bf69-cbff26ad8db1"),
            Uncode = "372",
            Name = "Ireland",
            FullName = "Ireland",
            Iso2 = "IE",
            Iso3 = "IRL",
            Latitude = 53,
            Longitude = -8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("02e399fd-4e4c-43e8-9f4c-54dcbda58567"),
            Uncode = "833",
            Name = "Isle of Man",
            FullName = "Isle of Man",
            Iso2 = "IM",
            Iso3 = "IMN",
            Latitude = 54.23,
            Longitude = -4.55,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("94017750-d84d-4318-bc05-84b1a961c1af"),
            Uncode = "376",
            Name = "Israel",
            FullName = "Israel",
            Iso2 = "IL",
            Iso3 = "ISR",
            Latitude = 31.5,
            Longitude = 34.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("c821e0de-4e1f-4217-9bd5-a4fef045d77a"),
            Uncode = "380",
            Name = "Italy",
            FullName = "Italy",
            Iso2 = "IT",
            Iso3 = "ITA",
            Latitude = 42.8333,
            Longitude = 12.8333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a8909a87-9071-4310-838b-582ef0eb40c1"),
            Uncode = "388",
            Name = "Jamaica",
            FullName = "Jamaica",
            Iso2 = "JM",
            Iso3 = "JAM",
            Latitude = 18.25,
            Longitude = -77.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7119263f-92e2-490a-b549-8e7abfaef389"),
            Uncode = "392",
            Name = "Japan",
            FullName = "Japan",
            Iso2 = "JP",
            Iso3 = "JPN",
            Latitude = 36,
            Longitude = 138,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("de19ae90-2231-4c5f-8b7b-9f683d01f6bd"),
            Uncode = "832",
            Name = "Jersey",
            FullName = "Jersey",
            Iso2 = "JE",
            Iso3 = "JEY",
            Latitude = 49.21,
            Longitude = -2.13,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("46490731-1c73-45c3-849b-156f3d849a2a"),
            Uncode = "400",
            Name = "Jordan",
            FullName = "Jordan",
            Iso2 = "JO",
            Iso3 = "JOR",
            Latitude = 31,
            Longitude = 36,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b5c9c131-2bb9-4086-8df5-932496ad43d6"),
            Uncode = "398",
            Name = "Kazakhstan",
            FullName = "Kazakhstan",
            Iso2 = "KZ",
            Iso3 = "KAZ",
            Latitude = 48,
            Longitude = 68,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("2ecc2667-ebdb-4ff0-865b-f6704375b92e"),
            Uncode = "404",
            Name = "Kenya",
            FullName = "Kenya",
            Iso2 = "KE",
            Iso3 = "KEN",
            Latitude = 1,
            Longitude = 38,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("c891c957-2073-46fb-aadf-32fe2e179c42"),
            Uncode = "296",
            Name = "Kiribati",
            FullName = "Kiribati",
            Iso2 = "KI",
            Iso3 = "KIR",
            Latitude = 1.4167,
            Longitude = 173,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3cd0389b-4adb-4ae0-91b7-9c167faf1b59"),
            Uncode = "408",
            Name = "Korea, Democratic People's Republic of",
            FullName = "Korea, Democratic People's Republic of",
            Iso2 = "KP",
            Iso3 = "PRK",
            Latitude = 40,
            Longitude = 127,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("0c86fe1e-712e-4ac6-a68a-72f6168c7878"),
            Uncode = "410",
            Name = "Korea, Republic of",
            FullName = "Korea, Republic of",
            Iso2 = "KR",
            Iso3 = "KOR",
            Latitude = 37,
            Longitude = 127.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("005a565c-9d69-4944-85dc-09d6b6bf8f2d"),
            Uncode = "414",
            Name = "Kuwait",
            FullName = "Kuwait",
            Iso2 = "KW",
            Iso3 = "KWT",
            Latitude = 29.3375,
            Longitude = 47.6581,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d4ad9393-8b6b-4032-a298-67dc0ee9c34f"),
            Uncode = "417",
            Name = "Kyrgyzstan",
            FullName = "Kyrgyzstan",
            Iso2 = "KG",
            Iso3 = "KGZ",
            Latitude = 41,
            Longitude = 75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3ac2dee7-6a19-4fad-abdb-4b634098ae3c"),
            Uncode = "418",
            Name = "Lao People's Democratic Republic",
            FullName = "Lao People's Democratic Republic",
            Iso2 = "LA",
            Iso3 = "LAO",
            Latitude = 18,
            Longitude = 105,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4b6360e5-8189-49f9-a0b0-48560939d214"),
            Uncode = "428",
            Name = "Latvia",
            FullName = "Latvia",
            Iso2 = "LV",
            Iso3 = "LVA",
            Latitude = 57,
            Longitude = 25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("416c635e-3d8c-4804-9e68-86acee929f9c"),
            Uncode = "422",
            Name = "Lebanon",
            FullName = "Lebanon",
            Iso2 = "LB",
            Iso3 = "LBN",
            Latitude = 33.8333,
            Longitude = 35.8333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8c6060ef-fbc0-4e3b-b1df-58ab04255b0a"),
            Uncode = "426",
            Name = "Lesotho",
            FullName = "Lesotho",
            Iso2 = "LS",
            Iso3 = "LSO",
            Latitude = -29.5,
            Longitude = 28.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6168e151-95e2-4f23-9fe4-898400d81f28"),
            Uncode = "430",
            Name = "Liberia",
            FullName = "Liberia",
            Iso2 = "LR",
            Iso3 = "LBR",
            Latitude = 6.5,
            Longitude = -9.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5df27ea0-c781-4167-a2ed-a610b425a70d"),
            Uncode = "434",
            Name = "Libyan Arab Jamahiriya",
            FullName = "Libyan Arab Jamahiriya",
            Iso2 = "LY",
            Iso3 = "LBY",
            Latitude = 25,
            Longitude = 17,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },



        new()
        {
            Id = Guid.Parse("d6c0619a-a2bd-4ba5-9023-859cea7c07ee"),
            Uncode = "438",
            Name = "Liechtenstein",
            FullName = "Liechtenstein",
            Iso2 = "LI",
            Iso3 = "LIE",
            Latitude = 47.1667,
            Longitude = 9.5333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4d77a19e-008f-4bdb-a258-d3b8f2fc3f1d"),
            Uncode = "440",
            Name = "Lithuania",
            FullName = "Lithuania",
            Iso2 = "LT",
            Iso3 = "LTU",
            Latitude = 56,
            Longitude = 24,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("2d74a8c3-fcb3-4246-b834-c28e92c7dd35"),
            Uncode = "442",
            Name = "Luxembourg",
            FullName = "Luxembourg",
            Iso2 = "LU",
            Iso3 = "LUX",
            Latitude = 49.75,
            Longitude = 6.1667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d837e527-8c64-4f69-8553-9b6ea222910c"),
            Uncode = "446",
            Name = "Macao",
            FullName = "Macao",
            Iso2 = "MO",
            Iso3 = "MAC",
            Latitude = 22.1667,
            Longitude = 113.55,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("1ca57fe7-032e-4166-9610-faf25b64bd87"),
            Uncode = "807",
            Name = "Macedonia, the former Yugoslav Republic of",
            FullName = "Macedonia, the former Yugoslav Republic of",
            Iso2 = "MK",
            Iso3 = "MKD",
            Latitude = 41.8333,
            Longitude = 22,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("63eed09b-1571-4635-85ba-5db09e3bc602"),
            Uncode = "450",
            Name = "Madagascar",
            FullName = "Madagascar",
            Iso2 = "MG",
            Iso3 = "MDG",
            Latitude = -20,
            Longitude = 47,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("162a0820-5aa7-4b63-9a6c-b8ae784029cc"),
            Uncode = "454",
            Name = "Malawi",
            FullName = "Malawi",
            Iso2 = "MW",
            Iso3 = "MWI",
            Latitude = -13.5,
            Longitude = 34,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("1c2fd592-d8d4-4a3e-acf8-40923806f16c"),
            Uncode = "458",
            Name = "Malaysia",
            FullName = "Malaysia",
            Iso2 = "MY",
            Iso3 = "MYS",
            Latitude = 2.5,
            Longitude = 112.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("409a81b4-b0b3-478e-b3ac-ba9786bc020a"),
            Uncode = "462",
            Name = "Maldives",
            FullName = "Maldives",
            Iso2 = "MV",
            Iso3 = "MDV",
            Latitude = 3.25,
            Longitude = 73,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e5f685fc-1281-41d8-a8af-c57853bc7ba1"),
            Uncode = "466",
            Name = "Mali",
            FullName = "Mali",
            Iso2 = "ML",
            Iso3 = "MLI",
            Latitude = 17,
            Longitude = -4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("c3475345-7f97-4944-b119-204e5ecf8478"),
            Uncode = "470",
            Name = "Malta",
            FullName = "Malta",
            Iso2 = "MT",
            Iso3 = "MLT",
            Latitude = 35.8333,
            Longitude = 14.5833,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e883c412-f4ef-40c9-8bf1-f9328da498e6"),
            Uncode = "584",
            Name = "Marshall Islands",
            FullName = "Marshall Islands",
            Iso2 = "MH",
            Iso3 = "MHL",
            Latitude = 9,
            Longitude = 168,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("84a2ce65-f383-4c51-9101-78267769c363"),
            Uncode = "474",
            Name = "Martinique",
            FullName = "Martinique",
            Iso2 = "MQ",
            Iso3 = "MTQ",
            Latitude = 14.6667,
            Longitude = -61,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("49e02060-bdcf-4ac4-a426-a8cbae9e16b1"),
            Uncode = "478",
            Name = "Mauritania",
            FullName = "Mauritania",
            Iso2 = "MR",
            Iso3 = "MRT",
            Latitude = 20,
            Longitude = -12,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("675c186e-2642-4272-afd6-8e946c00a545"),
            Uncode = "480",
            Name = "Mauritius",
            FullName = "Mauritius",
            Iso2 = "MU",
            Iso3 = "MUS",
            Latitude = -20.2833,
            Longitude = 57.55,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("ddea5fa2-8c83-4010-9838-70068e60c0bd"),
            Uncode = "175",
            Name = "Mayotte",
            FullName = "Mayotte",
            Iso2 = "YT",
            Iso3 = "MYT",
            Latitude = -12.8333,
            Longitude = 45.1667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e66e717b-d806-477e-b443-01e44800bb6c"),
            Uncode = "484",
            Name = "Mexico",
            FullName = "Mexico",
            Iso2 = "MX",
            Iso3 = "MEX",
            Latitude = 23,
            Longitude = -102,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("573ecc5a-ea26-4948-bad4-a0884b280a9e"),
            Uncode = "583",
            Name = "Micronesia, Federated States of",
            FullName = "Micronesia, Federated States of",
            Iso2 = "FM",
            Iso3 = "FSM",
            Latitude = 6.9167,
            Longitude = 158.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3d199311-8d01-4543-9e6b-92bd93b80674"),
            Uncode = "498",
            Name = "Moldova, Republic of",
            FullName = "Moldova, Republic of",
            Iso2 = "MD",
            Iso3 = "MDA",
            Latitude = 47,
            Longitude = 29,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("fde3e9f5-4138-4714-90dd-de61ca60bdba"),
            Uncode = "492",
            Name = "Monaco",
            FullName = "Monaco",
            Iso2 = "MC",
            Iso3 = "MCO",
            Latitude = 43.7333,
            Longitude = 7.4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("925dfcb8-2e0f-4c38-b873-abba9c984dc2"),
            Uncode = "496",
            Name = "Mongolia",
            FullName = "Mongolia",
            Iso2 = "MN",
            Iso3 = "MNG",
            Latitude = 46,
            Longitude = 105,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("1063423d-b57d-42eb-83ed-1e118805d67f"),
            Uncode = "499",
            Name = "Montenegro",
            FullName = "Montenegro",
            Iso2 = "ME",
            Iso3 = "MNE",
            Latitude = 42,
            Longitude = 19,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("896139b3-84d1-4c41-ab4c-b998d286b16e"),
            Uncode = "500",
            Name = "Montserrat",
            FullName = "Montserrat",
            Iso2 = "MS",
            Iso3 = "MSR",
            Latitude = 16.75,
            Longitude = -62.2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8354e3ff-0add-475c-8067-094243a2390f"),
            Uncode = "504",
            Name = "Morocco",
            FullName = "Morocco",
            Iso2 = "MA",
            Iso3 = "MAR",
            Latitude = 32,
            Longitude = -5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("065be982-99e6-4f7b-ae95-3b83360f817c"),
            Uncode = "508",
            Name = "Mozambique",
            FullName = "Mozambique",
            Iso2 = "MZ",
            Iso3 = "MOZ",
            Latitude = -18.25,
            Longitude = 35,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("278fdab4-10b9-4b31-baa0-e25b0657553a"),
            Uncode = "104",
            Name = "Burma",
            FullName = "Burma",
            Iso2 = "MM",
            Iso3 = "MMR",
            Latitude = 22,
            Longitude = 98,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3813164c-c1a9-4443-b0a7-bf0a90d6cd6e"),
            Uncode = "516",
            Name = "Namibia",
            FullName = "Namibia",
            Iso2 = "NA",
            Iso3 = "NAM",
            Latitude = -22,
            Longitude = 17,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9da40d04-597b-4d65-8c5b-f6f65008f24c"),
            Uncode = "520",
            Name = "Nauru",
            FullName = "Nauru",
            Iso2 = "NR",
            Iso3 = "NRU",
            Latitude = -0.5333,
            Longitude = 166.9167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("72014d6f-2fc1-43f6-a7c0-5f1aab624b2a"),
            Uncode = "524",
            Name = "Nepal",
            FullName = "Nepal",
            Iso2 = "NP",
            Iso3 = "NPL",
            Latitude = 28,
            Longitude = 84,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7183b067-8c41-48a6-936b-e45304b6a341"),
            Uncode = "528",
            Name = "Netherlands",
            FullName = "Netherlands",
            Iso2 = "NL",
            Iso3 = "NLD",
            Latitude = 52.5,
            Longitude = 5.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d0e8f3a7-8911-4a32-8e54-ed86e79626af"),
            Uncode = "530",
            Name = "Netherlands Antilles",
            FullName = "Netherlands Antilles",
            Iso2 = "AN",
            Iso3 = "ANT",
            Latitude = 12.25,
            Longitude = -68.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("31217fae-f13e-414a-abb5-552c97a47fa0"),
            Uncode = "540",
            Name = "New Caledonia",
            FullName = "New Caledonia",
            Iso2 = "NC",
            Iso3 = "NCL",
            Latitude = -21.5,
            Longitude = 165.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b4aded3f-b266-47c4-8e06-4de5e4b82a84"),
            Uncode = "554",
            Name = "New Zealand",
            FullName = "New Zealand",
            Iso2 = "NZ",
            Iso3 = "NZL",
            Latitude = -41,
            Longitude = 174,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("905afc55-cdf6-469e-ac15-3f2d537ff4c0"),
            Uncode = "558",
            Name = "Nicaragua",
            FullName = "Nicaragua",
            Iso2 = "NI",
            Iso3 = "NIC",
            Latitude = 13,
            Longitude = -85,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("06ac06e6-139a-4590-b0e4-4c02cf5fcca3"),
            Uncode = "562",
            Name = "Niger",
            FullName = "Niger",
            Iso2 = "NE",
            Iso3 = "NER",
            Latitude = 16,
            Longitude = 8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("66547f93-1dd5-4fd1-8a9b-d396896f8511"),
            Uncode = "566",
            Name = "Nigeria",
            FullName = "Nigeria",
            Iso2 = "NG",
            Iso3 = "NGA",
            Latitude = 10,
            Longitude = 8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("aa9854c9-57c2-47e6-ba7f-c74a5b0e08f0"),
            Uncode = "570",
            Name = "Niue",
            FullName = "Niue",
            Iso2 = "NU",
            Iso3 = "NIU",
            Latitude = -19.0333,
            Longitude = -169.8667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3c99b2b7-6561-4d41-bc45-8db1b27a28cf"),
            Uncode = "574",
            Name = "Norfolk Island",
            FullName = "Norfolk Island",
            Iso2 = "NF",
            Iso3 = "NFK",
            Latitude = -29.0333,
            Longitude = 167.95,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("02345ff3-0362-41bc-a6da-53c78cd9886c"),
            Uncode = "580",
            Name = "Northern Mariana Islands",
            FullName = "Northern Mariana Islands",
            Iso2 = "MP",
            Iso3 = "MNP",
            Latitude = 15.2,
            Longitude = 145.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b3559bd0-659e-45fc-a3cb-ed27decc44cc"),
            Uncode = "578",
            Name = "Norway",
            FullName = "Norway",
            Iso2 = "NO",
            Iso3 = "NOR",
            Latitude = 62,
            Longitude = 10,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("85bf3ffc-083e-4756-af3e-43db53ef99b7"),
            Uncode = "512",
            Name = "Oman",
            FullName = "Oman",
            Iso2 = "OM",
            Iso3 = "OMN",
            Latitude = 21,
            Longitude = 57,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("ee61dfd6-18cc-4b6a-ac6e-abb2ccbd446d"),
            Uncode = "586",
            Name = "Pakistan",
            FullName = "Pakistan",
            Iso2 = "PK",
            Iso3 = "PAK",
            Latitude = 30,
            Longitude = 70,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9cfa6332-502c-4eac-b5d5-cc74bc1ac482"),
            Uncode = "585",
            Name = "Palau",
            FullName = "Palau",
            Iso2 = "PW",
            Iso3 = "PLW",
            Latitude = 7.5,
            Longitude = 134.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("af58f313-02db-4939-9d3b-de1378e6d080"),
            Uncode = "275",
            Name = "Palestinian Territory, Occupied",
            FullName = "Palestinian Territory, Occupied",
            Iso2 = "PS",
            Iso3 = "PSE",
            Latitude = 32,
            Longitude = 35.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5c4cbeae-bb4a-45e2-9505-5772e8915b4c"),
            Uncode = "591",
            Name = "Panama",
            FullName = "Panama",
            Iso2 = "PA",
            Iso3 = "PAN",
            Latitude = 9,
            Longitude = -80,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("603b8d05-a5ba-497b-86f1-9605346d78de"),
            Uncode = "598",
            Name = "Papua New Guinea",
            FullName = "Papua New Guinea",
            Iso2 = "PG",
            Iso3 = "PNG",
            Latitude = -6,
            Longitude = 147,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5edf5b3c-02df-46bd-8dd1-1e60c46ca9d6"),
            Uncode = "600",
            Name = "Paraguay",
            FullName = "Paraguay",
            Iso2 = "PY",
            Iso3 = "PRY",
            Latitude = -23,
            Longitude = -58,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b7fd5616-1062-4afb-9950-c7078f3cd8aa"),
            Uncode = "604",
            Name = "Peru",
            FullName = "Peru",
            Iso2 = "PE",
            Iso3 = "PER",
            Latitude = -10,
            Longitude = -76,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f08c2d40-4cba-4c7e-9ecd-905e2cc8f1ec"),
            Uncode = "608",
            Name = "Philippines",
            FullName = "Philippines",
            Iso2 = "PH",
            Iso3 = "PHL",
            Latitude = 13,
            Longitude = 122,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f3f9c03e-4cbe-416e-88c6-5b51002a94a3"),
            Uncode = "612",
            Name = "Pitcairn",
            FullName = "Pitcairn",
            Iso2 = "PN",
            Iso3 = "PCN",
            Latitude = -24.7,
            Longitude = -127.4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b5169ce8-545b-4157-8938-f7d88076c5c1"),
            Uncode = "616",
            Name = "Poland",
            FullName = "Poland",
            Iso2 = "PL",
            Iso3 = "POL",
            Latitude = 52,
            Longitude = 20,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("5f7c2d0d-f8d6-4263-9872-89c14173915f"),
            Uncode = "620",
            Name = "Portugal",
            FullName = "Portugal",
            Iso2 = "PT",
            Iso3 = "PRT",
            Latitude = 39.5,
            Longitude = -8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9179560f-1ddd-484b-bdfa-cb8594b8907e"),
            Uncode = "630",
            Name = "Puerto Rico",
            FullName = "Puerto Rico",
            Iso2 = "PR",
            Iso3 = "PRI",
            Latitude = 18.25,
            Longitude = -66.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8150f1c1-cd20-437d-aec5-fc14ae5b938a"),
            Uncode = "634",
            Name = "Qatar",
            FullName = "Qatar",
            Iso2 = "QA",
            Iso3 = "QAT",
            Latitude = 25.5,
            Longitude = 51.25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("aadc78b3-042d-4853-af45-fa54460ea82d"),
            Uncode = "638",
            Name = "Réunion",
            FullName = "Réunion",
            Iso2 = "RE",
            Iso3 = "REU",
            Latitude = -21.1,
            Longitude = 55.6,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("abcbdab4-2531-44ba-a29f-6e22b0a629cd"),
            Uncode = "642",
            Name = "Romania",
            FullName = "Romania",
            Iso2 = "RO",
            Iso3 = "ROU",
            Latitude = 46,
            Longitude = 25,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("86b975c6-763b-4856-81d5-914d66632250"),
            Uncode = "643",
            Name = "Russian Federation",
            FullName = "Russian Federation",
            Iso2 = "RU",
            Iso3 = "RUS",
            Latitude = 60,
            Longitude = 100,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f0be5666-a01d-4673-8a2e-0edcf7c9668c"),
            Uncode = "646",
            Name = "Rwanda",
            FullName = "Rwanda",
            Iso2 = "RW",
            Iso3 = "RWA",
            Latitude = -2,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("65e016a0-f2cc-47a2-8ab1-04704b300241"),
            Uncode = "654",
            Name = "Saint Helena, Ascension and Tristan da Cunha",
            FullName = "Saint Helena, Ascension and Tristan da Cunha",
            Iso2 = "SH",
            Iso3 = "SHN",
            Latitude = -15.9333,
            Longitude = -5.7,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a75a5f8c-87d5-4209-9ff1-3aabbfa47d2e"),
            Uncode = "659",
            Name = "Saint Kitts and Nevis",
            FullName = "Saint Kitts and Nevis",
            Iso2 = "KN",
            Iso3 = "KNA",
            Latitude = 17.3333,
            Longitude = -62.75,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("247c1409-7ecd-416b-ab00-19da692a19e4"),
            Uncode = "662",
            Name = "Saint Lucia",
            FullName = "Saint Lucia",
            Iso2 = "LC",
            Iso3 = "LCA",
            Latitude = 13.8833,
            Longitude = -61.1333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("c15699da-944e-4dd6-b14d-0b3c7091b61b"),
            Uncode = "666",
            Name = "Saint Pierre and Miquelon",
            FullName = "Saint Pierre and Miquelon",
            Iso2 = "PM",
            Iso3 = "SPM",
            Latitude = 46.8333,
            Longitude = -56.3333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a91afe45-1fe6-4dc2-85cf-5139f03985a0"),
            Uncode = "670",
            Name = "Saint Vincent and the Grenadines",
            FullName = "Saint Vincent and the Grenadines",
            Iso2 = "VC",
            Iso3 = "VCT",
            Latitude = 13.25,
            Longitude = -61.2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("bada8a43-c365-494a-b35a-7c639ea9a2b8"),
            Uncode = "882",
            Name = "Samoa",
            FullName = "Samoa",
            Iso2 = "WS",
            Iso3 = "WSM",
            Latitude = -13.5833,
            Longitude = -172.3333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("c9b1513c-fa27-4761-b92e-66fe6f42da27"),
            Uncode = "674",
            Name = "San Marino",
            FullName = "San Marino",
            Iso2 = "SM",
            Iso3 = "SMR",
            Latitude = 43.7667,
            Longitude = 12.4167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("93fcce2d-ce56-47f8-82c8-cec2869b9d70"),
            Uncode = "678",
            Name = "Sao Tome and Principe",
            FullName = "Sao Tome and Principe",
            Iso2 = "ST",
            Iso3 = "STP",
            Latitude = 1,
            Longitude = 7,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d41134df-3260-4261-92c1-383bf4a3f130"),
            Uncode = "682",
            Name = "Saudi Arabia",
            FullName = "Saudi Arabia",
            Iso2 = "SA",
            Iso3 = "SAU",
            Latitude = 25,
            Longitude = 45,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("51ab83a3-0d80-4caa-90b4-2a4f14de30ca"),
            Uncode = "686",
            Name = "Senegal",
            FullName = "Senegal",
            Iso2 = "SN",
            Iso3 = "SEN",
            Latitude = 14,
            Longitude = -14,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("383449dc-8265-4aa8-b1eb-a966833ed1eb"),
            Uncode = "688",
            Name = "Serbia",
            FullName = "Serbia",
            Iso2 = "RS",
            Iso3 = "SRB",
            Latitude = 44,
            Longitude = 21,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a42be160-fa1e-43c8-b07b-84b2c20d7b4a"),
            Uncode = "690",
            Name = "Seychelles",
            FullName = "Seychelles",
            Iso2 = "SC",
            Iso3 = "SYC",
            Latitude = -4.5833,
            Longitude = 55.6667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7661397b-60f0-40f1-8586-b6b7134d4ce1"),
            Uncode = "694",
            Name = "Sierra Leone",
            FullName = "Sierra Leone",
            Iso2 = "SL",
            Iso3 = "SLE",
            Latitude = 8.5,
            Longitude = -11.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d52122b3-8990-4ba9-b055-19288130aabd"),
            Uncode = "702",
            Name = "Singapore",
            FullName = "Singapore",
            Iso2 = "SG",
            Iso3 = "SGP",
            Latitude = 1.3667,
            Longitude = 103.8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3583c9be-0d3f-41fb-96e1-4aff0ad5491c"),
            Uncode = "703",
            Name = "Slovakia",
            FullName = "Slovakia",
            Iso2 = "SK",
            Iso3 = "SVK",
            Latitude = 48.6667,
            Longitude = 19.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7cfb16b4-4abb-478d-9e69-a3681fca8b10"),
            Uncode = "705",
            Name = "Slovenia",
            FullName = "Slovenia",
            Iso2 = "SI",
            Iso3 = "SVN",
            Latitude = 46,
            Longitude = 15,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b0d41dda-9a7c-4f85-8a6c-83ed03715182"),
            Uncode = "090",
            Name = "Solomon Islands",
            FullName = "Solomon Islands",
            Iso2 = "SB",
            Iso3 = "SLB",
            Latitude = -8,
            Longitude = 159,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("309176d1-fbea-4f4f-852f-0ea4e80fda31"),
            Uncode = "706",
            Name = "Somalia",
            FullName = "Somalia",
            Iso2 = "SO",
            Iso3 = "SOM",
            Latitude = 10,
            Longitude = 49,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("cc2eb286-ac20-477a-ad69-081de6fb8bcc"),
            Uncode = "710",
            Name = "South Africa",
            FullName = "South Africa",
            Iso2 = "ZA",
            Iso3 = "ZAF",
            Latitude = -29,
            Longitude = 24,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("4ff43f3b-e800-4a62-aef0-3c95a8098e12"),
            Uncode = "239",
            Name = "South Georgia and the South Sandwich Islands",
            FullName = "South Georgia and the South Sandwich Islands",
            Iso2 = "GS",
            Iso3 = "SGS",
            Latitude = -54.5,
            Longitude = -37,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("134b49a9-9d5e-45c3-803c-b9441ceaffdb"),
            Uncode = "728",
            Name = "South Sudan",
            FullName = "South Sudan",
            Iso2 = "SS",
            Iso3 = "SSD",
            Latitude = 8,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("dbc51b7a-2884-4919-860d-8f183e470d27"),
            Uncode = "724",
            Name = "Spain",
            FullName = "Spain",
            Iso2 = "ES",
            Iso3 = "ESP",
            Latitude = 40,
            Longitude = -4,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("596f40bb-3892-44b5-b53c-baed18fd77d7"),
            Uncode = "144",
            Name = "Sri Lanka",
            FullName = "Sri Lanka",
            Iso2 = "LK",
            Iso3 = "LKA",
            Latitude = 7,
            Longitude = 81,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("72641c06-32cb-4bc1-8535-bab04edde392"),
            Uncode = "736",
            Name = "Sudan",
            FullName = "Sudan",
            Iso2 = "SD",
            Iso3 = "SDN",
            Latitude = 15,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d2037756-38a6-484d-9fc1-373898140799"),
            Uncode = "740",
            Name = "Suriname",
            FullName = "Suriname",
            Iso2 = "SR",
            Iso3 = "SUR",
            Latitude = 4,
            Longitude = -56,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("de09cfae-dcb9-4725-90f6-4ecb62ab563a"),
            Uncode = "744",
            Name = "Svalbard and Jan Mayen",
            FullName = "Svalbard and Jan Mayen",
            Iso2 = "SJ",
            Iso3 = "SJM",
            Latitude = 78,
            Longitude = 20,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d5240bc2-cd45-487c-8edb-389f8f622874"),
            Uncode = "748",
            Name = "Swaziland",
            FullName = "Swaziland",
            Iso2 = "SZ",
            Iso3 = "SWZ",
            Latitude = -26.5,
            Longitude = 31.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("aaf9d724-318e-4c09-b93b-6147f5e82c06"),
            Uncode = "752",
            Name = "Sweden",
            FullName = "Sweden",
            Iso2 = "SE",
            Iso3 = "SWE",
            Latitude = 62,
            Longitude = 15,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("cab5f713-8153-47a8-8176-e05d5e38844d"),
            Uncode = "756",
            Name = "Switzerland",
            FullName = "Switzerland",
            Iso2 = "CH",
            Iso3 = "CHE",
            Latitude = 47,
            Longitude = 8,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a0171cc9-7565-4529-ae35-10d8ef850ab9"),
            Uncode = "760",
            Name = "Syrian Arab Republic",
            FullName = "Syrian Arab Republic",
            Iso2 = "SY",
            Iso3 = "SYR",
            Latitude = 35,
            Longitude = 38,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b3f4e144-cb11-4a55-9583-85babec3b6fb"),
            Uncode = "158",
            Name = "Taiwan, Province of China",
            FullName = "Taiwan, Province of China",
            Iso2 = "TW",
            Iso3 = "TWN",
            Latitude = 23.5,
            Longitude = 121,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("e966036f-5b97-43c1-ab00-6bc0467c951c"),
            Uncode = "762",
            Name = "Tajikistan",
            FullName = "Tajikistan",
            Iso2 = "TJ",
            Iso3 = "TJK",
            Latitude = 39,
            Longitude = 71,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("92c11335-746e-40b6-b4ac-ffba90feeeeb"),
            Uncode = "834",
            Name = "Tanzania, United Republic of",
            FullName = "Tanzania, United Republic of",
            Iso2 = "TZ",
            Iso3 = "TZA",
            Latitude = -6,
            Longitude = 35,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("0d823156-4652-46ec-8651-5ce3ec969cdd"),
            Uncode = "764",
            Name = "Thailand",
            FullName = "Thailand",
            Iso2 = "TH",
            Iso3 = "THA",
            Latitude = 15,
            Longitude = 100,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("add53c71-0c5e-425a-b9e3-6f671538f643"),
            Uncode = "626",
            Name = "Timor-Leste",
            FullName = "Timor-Leste",
            Iso2 = "TL",
            Iso3 = "TLS",
            Latitude = -8.55,
            Longitude = 125.5167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("813066f9-893c-4abe-853d-cb7a5141015e"),
            Uncode = "768",
            Name = "Togo",
            FullName = "Togo",
            Iso2 = "TG",
            Iso3 = "TGO",
            Latitude = 8,
            Longitude = 1.1667,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9643cc9f-1eb7-458c-a459-32a23083f901"),
            Uncode = "772",
            Name = "Tokelau",
            FullName = "Tokelau",
            Iso2 = "TK",
            Iso3 = "TKL",
            Latitude = -9,
            Longitude = -172,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("3678b85f-aee6-4832-a29d-5f3fe17ca1e5"),
            Uncode = "776",
            Name = "Tonga",
            FullName = "Tonga",
            Iso2 = "TO",
            Iso3 = "TON",
            Latitude = -20,
            Longitude = -175,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9822575b-29ca-4f08-9782-bc917bee348e"),
            Uncode = "780",
            Name = "Trinidad and Tobago",
            FullName = "Trinidad and Tobago",
            Iso2 = "TT",
            Iso3 = "TTO",
            Latitude = 11,
            Longitude = -61,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a5082300-6547-44d1-a171-d1b36f14017f"),
            Uncode = "788",
            Name = "Tunisia",
            FullName = "Tunisia",
            Iso2 = "TN",
            Iso3 = "TUN",
            Latitude = 34,
            Longitude = 9,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("6f50909c-6a50-457b-8eff-a924e097b1c3"),
            Uncode = "792",
            Name = "Turkey",
            FullName = "Turkey",
            Iso2 = "TR",
            Iso3 = "TUR",
            Latitude = 39,
            Longitude = 35,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("335061d2-3f65-4c20-9e0e-5514622f902d"),
            Uncode = "795",
            Name = "Turkmenistan",
            FullName = "Turkmenistan",
            Iso2 = "TM",
            Iso3 = "TKM",
            Latitude = 40,
            Longitude = 60,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b568fa28-61e6-4b78-adb9-dfe1f61c1e9f"),
            Uncode = "796",
            Name = "Turks and Caicos Islands",
            FullName = "Turks and Caicos Islands",
            Iso2 = "TC",
            Iso3 = "TCA",
            Latitude = 21.75,
            Longitude = -71.5833,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("166a30bf-c14a-4b2d-bb78-e9d9cffe5f53"),
            Uncode = "798",
            Name = "Tuvalu",
            FullName = "Tuvalu",
            Iso2 = "TV",
            Iso3 = "TUV",
            Latitude = -8,
            Longitude = 178,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("fdff2de3-7b82-48bb-9f85-aeee13195667"),
            Uncode = "800",
            Name = "Uganda",
            FullName = "Uganda",
            Iso2 = "UG",
            Iso3 = "UGA",
            Latitude = 1,
            Longitude = 32,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("08262484-d296-406e-88f7-68f4ba474128"),
            Uncode = "804",
            Name = "Ukraine",
            FullName = "Ukraine",
            Iso2 = "UA",
            Iso3 = "UKR",
            Latitude = 49,
            Longitude = 32,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7f8929a4-0f44-48d1-88aa-1affa533d9ed"),
            Uncode = "784",
            Name = "United Arab Emirates",
            FullName = "United Arab Emirates",
            Iso2 = "AE",
            Iso3 = "ARE",
            Latitude = 24,
            Longitude = 54,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a227fb3a-494d-4583-8cd1-d6b616d9f11b"),
            Uncode = "826",
            Name = "United Kingdom",
            FullName = "United Kingdom of Great Britain and Northern Ireland",
            Iso2 = "GB",
            Iso3 = "GBR",
            Latitude = 54,
            Longitude = -2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f93e6de7-bb51-4b3a-a39a-bc5c4004e9fa"),
            Uncode = "840",
            Name = "United States",
            FullName = "United States",
            Iso2 = "US",
            Iso3 = "USA",
            Latitude = 38,
            Longitude = -97,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8abdaa7b-5857-4282-bd22-1a386616e0a2"),
            Uncode = "581",
            Name = "United States Minor Outlying Islands",
            FullName = "United States Minor Outlying Islands",
            Iso2 = "UM",
            Iso3 = "UMI",
            Latitude = 19.2833,
            Longitude = 166.6,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("9ebac16b-9181-40b9-90f0-57224c05439f"),
            Uncode = "858",
            Name = "Uruguay",
            FullName = "Uruguay",
            Iso2 = "UY",
            Iso3 = "URY",
            Latitude = -33,
            Longitude = -56,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("f38a877e-540d-4920-a899-f3375da456cd"),
            Uncode = "860",
            Name = "Uzbekistan",
            FullName = "Uzbekistan",
            Iso2 = "UZ",
            Iso3 = "UZB",
            Latitude = 41,
            Longitude = 64,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("bd5d07c6-1cf4-4caf-9da4-62a125321594"),
            Uncode = "548",
            Name = "Vanuatu",
            FullName = "Vanuatu",
            Iso2 = "VU",
            Iso3 = "VUT",
            Latitude = -16,
            Longitude = 167,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("8f27f125-f30d-4e14-821f-448883322288"),
            Uncode = "862",
            Name = "Venezuela, Bolivarian Republic of",
            FullName = "Venezuela, Bolivarian Republic of",
            Iso2 = "VE",
            Iso3 = "VEN",
            Latitude = 8,
            Longitude = -66,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },


        new()
        {
            Id = Guid.Parse("887e3c2b-47e5-478d-88e8-0d94992946f4"),
            Uncode = "704",
            Name = "Viet Nam",
            FullName = "Viet Nam",
            Iso2 = "VN",
            Iso3 = "VNM",
            Latitude = 16,
            Longitude = 106,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("68767fb5-ff80-44e6-8ebe-41b4a81b31ab"),
            Uncode = "092",
            Name = "Virgin Islands, British",
            FullName = "Virgin Islands, British",
            Iso2 = "VG",
            Iso3 = "VGB",
            Latitude = 18.5,
            Longitude = -64.5,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7d64cc5f-e748-4cda-8505-dd6a9c6629ea"),
            Uncode = "850",
            Name = "Virgin Islands, U.S.",
            FullName = "Virgin Islands, U.S.",
            Iso2 = "VI",
            Iso3 = "VIR",
            Latitude = 18.3333,
            Longitude = -64.8333,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("7974a2f5-a99a-4eab-ae62-7c1bb56ee0a2"),
            Uncode = "876",
            Name = "Wallis and Futuna",
            FullName = "Wallis and Futuna",
            Iso2 = "WF",
            Iso3 = "WLF",
            Latitude = -13.3,
            Longitude = -176.2,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("b304e742-e971-438a-84ab-ac8eb2459a20"),
            Uncode = "732",
            Name = "Western Sahara",
            FullName = "Western Sahara",
            Iso2 = "EH",
            Iso3 = "ESH",
            Latitude = 24.5,
            Longitude = -13,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("1288a3a4-1735-4805-a0bc-8c4d94b39779"),
            Uncode = "887",
            Name = "Yemen",
            FullName = "Yemen",
            Iso2 = "YE",
            Iso3 = "YEM",
            Latitude = 15,
            Longitude = 48,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("a1d31ddf-c6cb-4be7-b237-ef7decb1b2ad"),
            Uncode = "894",
            Name = "Zambia",
            FullName = "Zambia",
            Iso2 = "ZM",
            Iso3 = "ZMB",
            Latitude = -15,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },

        new()
        {
            Id = Guid.Parse("d03a014c-2482-4951-be41-ef35c7f9a1ff"),
            Uncode = "716",
            Name = "Zimbabwe",
            FullName = "Zimbabwe",
            Iso2 = "ZW",
            Iso3 = "ZWE",
            Latitude = -20,
            Longitude = 30,
            GmtHour = 0,
            GmtMin = 0,
            IsActive = true
        },
    };

    private async Task AddOrUpdateCountries(CancellationToken cancellationToken)
    {       
        foreach (var country in Countries)
        {
            if (await _db.Countries.Where(x => x.Id == country.Id).AnyAsync(cancellationToken))
            {
                _db.Update(country);
            }
            else
            {
                await _db.AddAsync(country);
            }
        }
    }
}