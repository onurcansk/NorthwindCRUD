using Base.CrossCuttingConcerns.Validation;
using Base.Utilities.Interceptors;
using Castle.DynamicProxy;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation:MethodInterception
    {
        private string _role;
        SerializeDto _employee;
        public SecuredOperation(string role)
        {
            _role = role;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            JsonDeserialization();
            if (_employee.Auth == _role)
            {
                return;
            }
            throw new Exception("Bu işlemi yapmaya yetkiniz bulunmamaktadır");
        }

        private void JsonDeserialization()
        {
            JsonSerializer js = new JsonSerializer();
            StreamReader sr = new StreamReader($@"Serialization\user.json");
            JsonReader jsonReader = new JsonTextReader(sr);
            _employee = js.Deserialize<SerializeDto>(jsonReader);
            jsonReader.Close();
            sr.Close();

        }
    }
}
