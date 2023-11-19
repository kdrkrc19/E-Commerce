using Entities.ModelsDTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace E_Commerce_WebAPI.Utilities.Formatters
{
    public class CSVOutputFormatter : TextOutputFormatter
    {
        public CSVOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        //İstenilen tipte çıktı verebilir mi
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ModelsDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<ModelsDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        //Gelen veri formatlanıyor
        private static void FormatCsv(StringBuilder buffer, ModelsDto model)
        {
            buffer.AppendLine($"{model.ModelId}, {model.ModelName}");
        }
        //response üretme
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            //Dönüşüm olabiliyor mu?
            if (context.Object is IEnumerable<ModelsDto>)
            {
                foreach (var model in (IEnumerable<ModelsDto>)context.Object)
                {
                    FormatCsv(buffer, model);
                }
            }
            else
            {
                FormatCsv(buffer, (ModelsDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
