using MarathonTrainingPlan;

var marathonDate = new DateTime(2025, 12, 7);
var startDate = marathonDate.AddDays(-7 * 24);

var weekSchedules = new List<WeekSchedule>();

for (var week = 0; week < 24; week++)
{
    var weekBuilder = new WeekScheduleBuilder(week + 1, DateOnly.FromDateTime(startDate))
        .WithDescription(GetPhase(week + 1))
        .AddRun(RunTraining.ForTrainingInWeek(week + 1, trainingInWeek: 1))
        .AddRun(RunTraining.ForTrainingInWeek(week + 1, trainingInWeek: 2))
        .AddRun(RunTraining.ForTrainingInWeek(week + 1, trainingInWeek: 3))
        .AddStrengthTraining(new StrengthTraining {Description = "1x/week"});

    if ((week + 1) % 2 == 0)
    {
        weekBuilder.AddRun(RunTraining.SundayRun());
    }

    var weekSchedule = weekBuilder.Build();
    weekSchedules.Add(weekSchedule);
    weekSchedule.PrintWeek();
    PrintWeeks(week + 1, startDate);
}
var exporter = new ExcelExporter
{
    FileName = "export.xlsx",
};

exporter.Export(weekSchedules.ToArray());

return;

static void PrintWeeks(int week, DateTime startDate)
{
    var weekStart = startDate.AddDays(7 * (week-1));

    var runType = (week % 2 == 1) ? "3 runs" : "4 runs";
    var phase = GetPhase(week);
    var tuesdayRun = GetRunType("Tuesday", week);
    var thursdayRun = GetRunType("Thursday", week);
    var saturdayRun = GetRunType("Saturday", week);

    var sundayRun = (runType == "4 runs") ? "Easy Run or Cross-Train" : "";
    Console.WriteLine($"Week {week} ({weekStart:yyyy-MM-dd}) - {phase}");
    Console.WriteLine($" Tuesday: {weekStart.AddDays(1):yyyy-MM-dd} - {tuesdayRun}");
    Console.WriteLine($" Thursday: {weekStart.AddDays(3):yyyy-MM-dd} - {thursdayRun}");
    Console.WriteLine($" Saturday: {weekStart.AddDays(5):yyyy-MM-dd} - {saturdayRun}");

    if (sundayRun != "")
    {
        Console.WriteLine($" Sunday (Optional): {weekStart.AddDays(6):yyyy-MM-dd} - {sundayRun}");
    }
    Console.WriteLine($" Strength Training: 1x/week\n");
}
static string GetPhase(int week)
{
    return week switch
    {
        <= 4 => "Base Building",
        <= 8 => "Increasing Intensity",
        <= 12 => "Endurance + Pace",
        <= 16 => "Speed Endurance",
        <= 20 => "Peak Training",
        _ => "Taper & Race Prep"
    };
}

static string GetRunType(string day, int week)
{
    return week switch
    {
        <= 4 => day switch
        {
            "Tuesday" => "Easy Run (5–6 km)",
            "Thursday" => "Tempo Run (3–5 km)",
            "Saturday" => "Long Run (9–12 km)",
            _ => ""
        },
        <= 8 => day switch
        {
            "Tuesday" => "Intervals (6x400m)",
            "Thursday" => "Tempo Run (5–6 km)",
            "Saturday" => "Long Run (13–16 km)",
            _ => ""
        },
        <= 12 => day switch
        {
            "Tuesday" => "Intervals (4x800m)",
            "Thursday" => "Tempo Run (6–8 km)",
            "Saturday" => "Long Run (19–22 km)",
            _ => ""
        },
        <= 16 => day switch
        {
            "Tuesday" => "Hill Repeats or 6x400m",
            "Thursday" => "Marathon Pace Run (8–10 km)",
            "Saturday" => "Long Run (24–29 km)",
            _ => ""
        },
        <= 20 => day switch
        {
            "Tuesday" => "Speed Work (3x1 mile)",
            "Thursday" => "Marathon Pace Run (10–12 km)",
            "Saturday" => "Long Run (29–32 km)",
            _ => ""
        },
        _ => day switch
        {
            "Tuesday" => "Reduced Intervals / Pace Run",
            "Thursday" => "Short Tempo (6–8 km)",
            "Saturday" => "Reduced Long Run",
            _ => ""
        }
    };
}
