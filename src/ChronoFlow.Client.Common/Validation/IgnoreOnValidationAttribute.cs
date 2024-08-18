namespace ChronoFlow.Client.Common.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class IgnoreOnValidationAttribute : Attribute
{
}
