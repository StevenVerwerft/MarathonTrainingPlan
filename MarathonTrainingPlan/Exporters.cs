using ClosedXML.Excel;

namespace MarathonTrainingPlan;

public class ExcelExporter
{
    public string FileName { get; set; } = "export.xlsx";
    public string SheetName { get; set; } = "Marathon Training Plan";

    public void Export(WeekSchedule[] weekSchedules)
    {
        using var wb = new XLWorkbook();
        var sheet = wb.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "Week";
        sheet.Cell(1, 2).Value = "Description";
        sheet.Cell(1, 3).Value = "Day";
        sheet.Cell(1, 4).Value = "Date";
        sheet.Cell(1, 5).Value = "Run Description";
        sheet.Cell(1, 6).Value = "Training Type";
        sheet.Cell(1, 7).Value = "Min Distance";
        sheet.Cell(1, 8).Value = "Max Distance";

        var row = 2;
        foreach (var week in weekSchedules)
        {
            foreach (var run in week.Runs)
            {
                sheet.Cell(row, 1).Value = week.WeekNumber;
                sheet.Cell(row, 2).Value = week.WeekDescription;
                sheet.Cell(row, 3).Value = $"{run.Date:dddd}";
                sheet.Cell(row, 4).Value = run.Date.ToShortDateString();
                sheet.Cell(row, 5).Value = run.Training.Description;
                sheet.Cell(row, 6).Value = run.Training.RunType.ToString();
                sheet.Cell(row, 7).Value = run.Training.MinDistance;
                sheet.Cell(row, 8).Value = run.Training.MaxDistance;
                row++;
            }
        }

        sheet.Columns().AdjustToContents();
        wb.SaveAs(FileName);
    }
}
