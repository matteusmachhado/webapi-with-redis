using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Project.Application.Features
{
    public abstract class Command : IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; } = null;

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
