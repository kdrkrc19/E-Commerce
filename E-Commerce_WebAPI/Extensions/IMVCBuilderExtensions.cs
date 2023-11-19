using E_Commerce_WebAPI.Utilities.Formatters;

namespace E_Commerce_WebAPI.Extensions
{
    public static class IMVCBuilderExtensions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) => builder.AddMvcOptions(options => options.OutputFormatters.Add(new CSVOutputFormatter()));
    }
}
