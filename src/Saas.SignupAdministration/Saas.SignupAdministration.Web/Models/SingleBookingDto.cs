﻿namespace Saas.SignupAdministration.Web.Models;

public class SingleBookingDto
{

    public long BookingId { get; set; }
    public string ExternalSchemeAdmin { get; set; } = string.Empty;

    public string CourseDate { get; set; } = string.Empty;

    public string BookingType { get; set; } = string.Empty;

    public string RetirementSchemeName { get; set; } = string.Empty;
    public string SchemePosition { get; set; } = string.Empty;
    public string TrainingVenue { get; set; } = string.Empty;
    public string PaymentMode { get; set; } = string.Empty;
    public string AdditionalRequirements { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhysicalAddress { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public string? EmployerName { get; set; } = string.Empty;
    public int? Experience { get; set; } = 0;
    public string? Position { get; set; } = string.Empty;
    public string? DisabilityStatus { get; set; } = string.Empty;
    public string? IdNumber { get; set; } = string.Empty;

}