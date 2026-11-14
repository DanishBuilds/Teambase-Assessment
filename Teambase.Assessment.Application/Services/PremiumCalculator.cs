using Teambase.Assessment.Domain.Enums;

namespace Teambase.Assessment.Application.Services;

public class PremiumCalculator
{
    public PremiumCalculationResult Calculate(
        DateOnly dateOfBirth,
        Gender gender,
        DateOnly memberAdditionDate,
        DateOnly policyStartDate,
        DateOnly policyEndDate,
        PricingModel pricingModel,
        ProrataMethod prorataMethod)
    {
        if (policyEndDate < policyStartDate)
            throw new ArgumentException("Policy end date must be on or after policy start date.");
        if (memberAdditionDate < policyStartDate || memberAdditionDate > policyEndDate)
            throw new ArgumentException("Member addition date must fall within the policy period.");

        var age = GetAge(dateOfBirth, memberAdditionDate);
        var fullPremium = CalculateFullPremium(age, gender, pricingModel);

        int remainingDays = memberAdditionDate.DayNumber <= policyEndDate.DayNumber
            ? policyEndDate.DayNumber - memberAdditionDate.DayNumber + 1
            : 0;

        int remainingMonths = (policyEndDate.Year - memberAdditionDate.Year) * 12
            + (policyEndDate.Month - memberAdditionDate.Month) + 1;

        var proratedPremium = prorataMethod switch
        {
            ProrataMethod.ByDays => Math.Round(fullPremium / 365m * remainingDays, 2, MidpointRounding.AwayFromZero),
            ProrataMethod.ByMonth => Math.Round(fullPremium / 12m * remainingMonths, 2, MidpointRounding.AwayFromZero),
            _ => throw new ArgumentOutOfRangeException(nameof(prorataMethod))
        };

        return new PremiumCalculationResult(
            fullPremium,
            proratedPremium,
            age,
            remainingDays,
            remainingMonths);
    }

    public static int GetAge(DateOnly dateOfBirth, DateOnly asOfDate)
    {
        var age = asOfDate.Year - dateOfBirth.Year;
        if (dateOfBirth > asOfDate.AddYears(-age))
            age--;
        return age;
    }

    public static decimal CalculateFullPremium(int age, Gender gender, PricingModel pricingModel)
    {
        if (age < 0)
            throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");

        return pricingModel switch
        {
            PricingModel.FlatRate => 1000m,
            PricingModel.AgeRated => CalculateAgeRatedPremium(age),
            PricingModel.GenderAgeRated => CalculateGenderAgeRatedPremium(age, gender),
            _ => throw new ArgumentOutOfRangeException(nameof(pricingModel))
        };
    }

    private static decimal CalculateAgeRatedPremium(int age)
    {
        var decade = age / 10;
        var rateMultiplier = (decade + 1) * 100;
        return age * rateMultiplier;
    }

    private static decimal CalculateGenderAgeRatedPremium(int age, Gender gender)
    {
        var premium = CalculateAgeRatedPremium(age);
        if (gender == Gender.Female && age >= 18)
            premium *= 1.5m;
        return premium;
    }
}

public record PremiumCalculationResult(
    decimal FullPremium,
    decimal ProratedPremium,
    int Age,
    int RemainingDays,
    int RemainingMonths);