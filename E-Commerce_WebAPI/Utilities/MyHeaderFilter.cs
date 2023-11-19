using Azure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace E_Commerce_WebAPI.Utilities
{
    public class MyHeaderFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Header",
                In = ParameterLocation.Header,      
                Required = true, // set to false if this is optional
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
