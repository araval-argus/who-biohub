﻿namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class RequestStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string HexColor { get; set; }
}
