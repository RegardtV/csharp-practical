using FullStack.API.Helpers;
using FullStack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Services
{
    public interface IInvoiceValidator
    {
        public IEnumerable<ValidationResult> Validate(InvoiceForManipulationModel model);
    }

    public class InvoiceValidator: IInvoiceValidator
    {
        public IEnumerable<ValidationResult> Validate(InvoiceForManipulationModel model)
        {
            var daysInMonth = DateTime.DaysInMonth(model.IssueDate.Year, model.IssueDate.Month);
            if (model.IssueDate != new DateTime(model.IssueDate.Year, model.IssueDate.Month, daysInMonth - 5))
            {
                yield return new ValidationResult(nameof(model.IssueDate), "The issue date should be 5 days before the end of a month");
            }

            var nextMonth = model.IssueDate.AddMonths(1);
            var lastDayOfNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            if (model.DueDate != new DateTime(nextMonth.Year, nextMonth.Month, lastDayOfNextMonth))
            {
                yield return new ValidationResult(nameof(model.DueDate), "The due date should be on the last day of the month following the issue date month");
            }
        }
    }
}
