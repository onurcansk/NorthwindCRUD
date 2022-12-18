using Base.Aspects.Autofac.Validation;
using Base.DependencyResolvers.Ninject;
using Base.Utilities.Results.Abstract;
using Base.Utilities.Results.Concrete;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        IEmployeeRepository employeeRepository;
        public EmployeeManager()
        {
            employeeRepository = InstanceFactory.GetInstance<IEmployeeRepository>();
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Add(Employee Entity)
        {
            employeeRepository.Add(Entity);
            return new SuccessResult("Personel kayıt işlemi başarıyla gerçekleştirildi.");
        }

        [SecuredOperation("admin")]
        public IResult Delete(int id)
        {
            Employee employee = employeeRepository.Get(e => e.EmployeeID == id);
            employeeRepository.Delete(employee);
            return new SuccessResult("Personel silme işlemi başarıyla gerçekeltirildi.");
        }

        public IDataResult<List<Employee>> GetAll()
        {
            var result = employeeRepository.GetAll();
            return new SuccessDataResult<List<Employee>>(result, "Personeller listelendi");
        }

        public IDataResult<Employee> GetById(int id)
        {
            var result = employeeRepository.Get(e=>e.EmployeeID == id);
            return new SuccessDataResult<Employee>(result, "Personel görüntülendi");
        }

        public IDataResult<Employee> Login(UserDto userDto)
        {
            var result = employeeRepository.Get(e => e.FirstName == userDto.FirtName && e.LastName == userDto.LastName);
            return new SuccessDataResult<Employee>(result, "Giriş yapıldı");
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Update(Employee Entity)
        {
            var result = this.GetById(Entity.EmployeeID);
            result.Result.FirstName = Entity.FirstName;
            result.Result.LastName = Entity.LastName;
            result.Result.Address = Entity.Address;
            result.Result.City = Entity.City;
            result.Result.Country = Entity.Country;
            employeeRepository.Update(result.Result);
            return new SuccessResult("Kategori güncelleme işlemi başarıyla gerçekleşti.");
        }
    }
}
