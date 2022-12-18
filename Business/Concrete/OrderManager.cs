using Base.DependencyResolvers.Ninject;
using Base.Utilities.Results.Abstract;
using Base.Utilities.Results.Concrete;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System;
using DataAccess.Concrete.EntityFramework;
using Base.Entities;
using Base.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderRepository orderRepository;
        public OrderManager()
        {
            orderRepository = InstanceFactory.GetInstance<IOrderRepository>();
        }
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Add(Order Entity)
        {
            try
            {
                orderRepository.Add(Entity);
                return new SuccessResult("Siparişiniz başarıyla eklendi");
            }
            catch(Exception ex)
            {

                return new ErrorResult("Sipariş ekleme işlemi sırasında problem oluştu. Girdiğiniz bilgileri kontrol ediniz.");
            }
        }

       

        public IResult Delete(int id)
        {
            try
            {
                Order Entity = this.GetById(id).Result;
                orderRepository.Delete(Entity);
                return new SuccessResult("Sipariş silme işlemi başarıyla gerçekleşti.");
            }
            catch (Exception)
            {

                return new ErrorResult("Sipariş silme işlemi sırasında bir problem oluştu.");
            }
           
        }

        public IDataResult<List<Order>> GetAll()
        {
            try
            {
                var result = orderRepository.GetAll();
                return new SuccessDataResult<List<Order>>(result,"Siparişiniz başarıyla eklendi");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Order>>("Sipariş ekleme işlemi sırasında problem oluştu. Girdiğiniz bilgileri kontrol ediniz.");
            }
        }

        public IDataResult<Order> GetById(int id)
        {
            try
            {
                var result = orderRepository.Get(or => or.OrderID == id);
                return new SuccessDataResult<Order>(result, "Sipariş getirildi.");
            }
            catch (Exception)
            {
                return new ErrorDataResult<Order>("Girilen idye ait sipariş bulunamadı.");
            }
        }
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Update(Order Entity)
        {
            try
            {
                var result = this.GetById(Entity.OrderID);
                result.Result.CustomerID = Entity.CustomerID;
                result.Result.ShipCity = Entity.ShipCity;
                result.Result.ShipCountry = Entity.ShipCountry;
                result.Result.OrderDate = Entity.OrderDate;
                orderRepository.Update(result.Result);
                return new SuccessResult("Sipariş güncelleme işlemi başarıyla gerçekleşti.");
            }
            catch (Exception)
            {

                return new ErrorResult("Gönderilen sipariş numarası sistemdeki siparişlerle eşleşmedi. Güncelleme işlemi başarısız. ");
            }
          
        }
    }
}
