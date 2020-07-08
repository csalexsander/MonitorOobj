using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Oobj_Infra.Dto
{
    public class APIResponseDto
    {
        public APIResponseDto(bool sucesso, object data = null, IEnumerable<string> errors = null, IDictionary<string, string[]> validationErrors = null)
        {
            Ok = sucesso;
            Data = data;
            Errors = errors;
            ValidationErrors = validationErrors;
        }

        public bool Ok { get; set; }
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; set; }

        public static APIResponseDto Sucess(object data = null, IEnumerable<string> errors = null)
            => new APIResponseDto(true, data, errors);


        public static APIResponseDto Fail(object data = null, IEnumerable<string> errors = null)
            => new APIResponseDto(false, data, errors);


        public static APIResponseDto FailValidation(IDictionary<string, string[]> validationErrors)
            => new APIResponseDto(false, validationErrors: validationErrors);

        public static APIResponseDto FailValidationModelState(ModelStateDictionary validationErrors)
        {
            var errorsDictionary = new Dictionary<string, string[]>();

            var errorsList = validationErrors.GroupBy(x => x.Key).Select(x => new { key = x.Key, value = x.SelectMany(z => z.Value.Errors.Select(y => y.ErrorMessage)) });

            foreach (var errors in errorsList)
            {
                errorsDictionary.Add(errors.key, errors.value.ToArray());
            }

            return new APIResponseDto(false, validationErrors: errorsDictionary);
        }

    }
}
