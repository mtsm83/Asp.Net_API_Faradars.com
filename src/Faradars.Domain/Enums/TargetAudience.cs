namespace Faradars.Domain.Enums;

public enum TargetAudience
{
    // General/Common English Terms
    Infant = 1,         // 0–1 year
    Toddler = 2,        // 1–3 years
    Preschooler = 3,    // 3–5 years
    Child = 4,         // 5–12 years
    Adolescent = 5,    // 13–19 years
    Teenager = 5,      // 13–19 years (alias for Adolescent)
    YoungAdult = 6,    // 20–39 years
    MiddleAgedAdult = 7, // 40–59 years
    Senior = 8,        // 60–65+ years
    OlderAdult = 8     // 60–65+ years (alias for Senior)
}