using Base.Aspects.Autofac.Validation;
using Base.DependencyResolvers.Ninject;
using Base.Utilities.Results.Abstract;
using Base.Utilities.Results.Concrete;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerRepository customerRepository;
        public CustomerManager()
        {
            customerRepository = InstanceFactory.GetInstance<ICustomerRepository>();
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer Entity)
        {
            try
            {
                customerRepository.Add(Entity);
                return new SuccessResult("Müşteri başarıyla eklendi.");
            }
            catch (System.Exception)
            {
                return new ErrorResult("Müşteri ekleme sırasında bir hata oluştu.");
            }
        }

   
        public IResult Delete(string id)
        {
            try
            {
                var result = this.GetById(id).Result;
                customerRepository.Delete(result);
                return new SuccessResult("Müşteriyi silme işlemi başarıyla gerçekleşti");
            }
            catch (System.Exception)
            {

                return new ErrorResult("Müşteri silme işlemi sırasında bir problem oluştu");
            }
        }

        public IDataResult<List<Customer>> GetAll()
        {
            try
            {
                var result = customerRepository.GetAll();
                return new SuccessDataResult<List<Customer>>(result, "Müşteriler listelendi");
            }
            catch (System.Exception)
            {

                return new ErrorDataResult<List<Customer>>("Müşteri listelenirken bir hata oluştu");
            }
        }

        public IDataResult<Customer> GetById(string id)
        {
            try
            {
                var result = customerRepository.Get(c => c.CustomerID == id);
                return new SuccessDataResult<Customer>(result, "Müşteri getirildi.");
            }
            catch (System.Exception)
            {

                return new ErrorDataResult<Customer>("Idye ait müşteri bulunamadı.");
            }
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer Entity)
        {
            try
            {
                var result = this.GetById(Entity.CustomerID);
                result.Result.CompanyName = Entity.CompanyName;
                result.Result.ContactName = Entity.ContactName;
                result.Result.City = Entity.City;
                result.Result.Country = Entity.Country;
                customerRepository.Update(result.Result);
                return new SuccessResult("Müşteri güncelleme işlemi başarıyla gerçekleşti.");
            }
            catch (System.Exception)
            {
                return new ErrorResult("Güncelleme sırasında bir hata oluştu.");
            }
        }

       
    }
}
