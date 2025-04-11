namespace MarathonTrainingPlan;

public enum RunType
{
    Easy,
    Tempo,
    Long,
    Intervals,
    HillRepeats,
    MarathonPace,
    SpeedWork,
    ReducedIntervals,
    ShortTempo,
    ReducedLongRun,
}

public class RunTraining
{
    public required RunType RunType { get; set; }
    public required int MinDistance { get; set; }
    public required int MaxDistance { get; set; }
    public required string Description { get; set; }
    public bool Optional { get; set; }

    public static RunTraining ForTrainingInWeek(int week, int trainingInWeek)
    {
        return week switch
        {
            // Month 1
            <= 4 => trainingInWeek switch
            {
                1 => new()
                {
                    RunType = RunType.Easy,
                    MinDistance = 5000,
                    MaxDistance = 6000,
                    Description = "Easy Run (5-6 km)",
                    Optional = false
                },
                2 => new()
                {
                    RunType = RunType.Tempo,
                    MinDistance = 3000,
                    MaxDistance = 5000,
                    Description = "Tempo Run (3-5 km)",
                },
                3 => new()
                {
                    RunType = RunType.Long,
                    MinDistance = 9000,
                    MaxDistance = 12000,
                    Description = "Long Run (9-12 km)",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },

            // Month 2
            <= 8 => trainingInWeek switch
            {
                1 => new()
                {
                  RunType = RunType.Intervals,
                  MinDistance = 2400,
                  MaxDistance = 2400,
                  Description = "Intervals (6x400m)",
                  Optional = false
                },
                2 => new()
                {
                    RunType = RunType.Tempo,
                    MinDistance = 5000,
                    MaxDistance = 6000,
                    Description = "Tempo Run (5-6 km)",
                },
                3 => new()
                {
                    RunType = RunType.Long,
                    MinDistance = 13000,
                    MaxDistance = 16000,
                    Description = "Long Run (13-16 km)",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },

            // Month 3
            <= 12 => trainingInWeek switch
            {
                1 => new()
                {
                    RunType = RunType.Intervals,
                    MinDistance = 3200,
                    MaxDistance = 3200,
                    Description = "Intervals (4x800m)",
                    Optional = false
                },
                2 => new()
                {
                    RunType = RunType.Tempo,
                    MinDistance = 6000,
                    MaxDistance = 8000,
                    Description = "Tempo Run (6-8 km)",
                },
                3 => new()
                {
                    RunType = RunType.Long,
                    MinDistance = 19000,
                    MaxDistance = 22000,
                    Description = "Long Run (19-22 km)",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },

            // Month 4
            <= 16 => trainingInWeek switch
            {
                1 => new()
                {
                    RunType = RunType.HillRepeats,
                    MinDistance = 2400,
                    MaxDistance = 2400,
                    Description = "Hill Repeats or 6x400m",
                    Optional = false
                },
                2 => new()
                {
                    RunType = RunType.MarathonPace,
                    MinDistance = 8000,
                    MaxDistance = 10000,
                    Description = "Marathon Pace Run (8-10 km)",
                },
                3 => new()
                {
                    RunType = RunType.Long,
                    MinDistance = 24000,
                    MaxDistance = 29000,
                    Description = "Long Run (24-29 km)",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },

            // Month 5
            <= 20 => trainingInWeek switch
            {
                1 => new()
                {
                    RunType = RunType.SpeedWork,
                    MinDistance = 5000,
                    MaxDistance = 5000,
                    Description = "Speed Work (3x1 mile, approx. 5k)",
                    Optional = false
                },
                2 => new()
                {
                    RunType = RunType.MarathonPace,
                    MinDistance = 10000,
                    MaxDistance = 12000,
                    Description = "Marathon Pace Run (10-12 km)",
                },
                3 => new()
                {
                    RunType = RunType.Long,
                    MinDistance = 29000,
                    MaxDistance = 32000,
                    Description = "Long Run (29-32 km)",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },

            // Month 6
            <= 24 => trainingInWeek switch
            {
                1 => new()
                {
                    RunType = RunType.ReducedIntervals,
                    MinDistance = 0,
                    MaxDistance = 0,
                    Description = "Reduced Intervals / Pace Run",
                    Optional = false
                },
                2 => new()
                {
                    RunType = RunType.ShortTempo,
                    MinDistance = 6000,
                    MaxDistance = 8000,
                    Description = "Short Tempo (6-8 km)",
                },
                3 => new()
                {
                    RunType = RunType.ReducedLongRun,
                    MinDistance = 0,
                    MaxDistance = 0,
                    Description = "Reduced Long Run",
                    Optional = false
                },
                _ => throw new ArgumentOutOfRangeException(nameof(trainingInWeek), trainingInWeek,
                    "Invalid training in week")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(week), week, "invalid week")
        };
    }

    public static RunTraining SundayRun()
    {
        return new()
        {
            RunType = RunType.Easy,
            MinDistance = 0,
            MaxDistance = 0,
            Description = "Easy Run or Cross-Train",
            Optional = true
        };
    }
}

public class StrengthTraining
{
    public required string Description { get; set; }
}

public class ScheduledRunTraining
{
    public required DateOnly Date { get; set; }
    public required RunTraining Training { get; set; }
}

public class WeekSchedule
{
    public DateOnly WeekStartDate { get; set; }
    public int WeekNumber { get; set; }
    public ScheduledRunTraining[] Runs { get; set; } = [];
    public StrengthTraining[] StrengthTrainings { get; set; } = [];
    public required string WeekDescription { get; set; }

    public void PrintWeek()
    {
        Console.WriteLine($"Week {WeekNumber} ({WeekStartDate:yyyy-MM-dd}) - {WeekDescription}");
        foreach (var run in Runs)
        {
            Console.WriteLine($" {run.Date:dddd}: {run.Date:yyyy-MM-dd} - {run.Training.Description}");
        }

        var strengthTraining = StrengthTrainings.FirstOrDefault();
        if (strengthTraining != null)
        {
            Console.WriteLine($" Strength Training: {strengthTraining.Description}");
        }
    }
}

public class WeekScheduleBuilder
{
    private readonly Draft _draft = new();
    public WeekScheduleBuilder(int weekNumber, DateOnly initialStartDate)
    {
        _draft.WeekNumber = weekNumber;
        _draft.StartDate = initialStartDate.AddDays(7 * (weekNumber - 1));
    }

    public WeekSchedule Build()
    {
        return new WeekSchedule
        {
            WeekStartDate = _draft.StartDate,
            WeekNumber = _draft.WeekNumber,
            Runs = _draft.Runs.Select((run, index) => new ScheduledRunTraining
            {
                Date = _draft.StartDate.AddDays(Math.Min(index * 2 + 2, 7)),
                Training = run
            }).ToArray(),
            WeekDescription = _draft.WeekDescription,
            StrengthTrainings = _draft.StrengthTrainings.ToArray()
        };
    }

    public WeekScheduleBuilder
        AddRun(RunTraining run)
    {
        _draft.Runs.Add(run);
        return this;
    }

    public WeekScheduleBuilder AddStrengthTraining(StrengthTraining strengthTraining)
    {
        _draft.StrengthTrainings.Add(strengthTraining);
        return this;
    }

    public WeekScheduleBuilder WithDescription(string description)
    {
        _draft.WeekDescription = description;
        return this;
    }

    private class Draft
    {
        public int WeekNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public List<RunTraining> Runs { get; set; } = [];
        public List<StrengthTraining> StrengthTrainings { get; set; } = [];
        public string WeekDescription { get; set; } = "";
    }
}
