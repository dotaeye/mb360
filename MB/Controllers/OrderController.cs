﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.AutoMapper;
using MB.Data.Models;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using SQ.Core.Data;
using System.Text;
using MB.Models;
using MB.Pay.WxPayAPI;
using System.Transactions;

namespace MB.Controllers
{
    [Authorize]
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private IOrderService OrderService;
        private ApplicationUserManager _userManager;
        private IShoppingCartItemService ShoppingCartItemService;
        public OrderController(
            IOrderService _OrderService,
            IShoppingCartItemService _ShoppingCartItemService
          )
        {
            this.OrderService = _OrderService;
            this.ShoppingCartItemService = _ShoppingCartItemService;
        }

        public OrderController(
         ApplicationUserManager userManager,
         ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<WxPayData> GetNotifyData()
        {

            Log.Info("GetNotifyData", "触发了");
            //接收从微信后台POST过来的数据
            //System.IO.Stream s = await Request.Content.ReadAsStreamAsync();
            var info = await Request.Content.ReadAsStringAsync();
            Log.Info("ReadAsStreamAsync", info);

            //int count = 0;
            //byte[] buffer = new byte[1024];
            //StringBuilder builder = new StringBuilder();
            //while ((count = s.Read(buffer, 0, 1024)) > 0)
            //{
            //    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            //}
            //s.Flush();
            //s.Close();
            //s.Dispose();


            //Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(info);
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                return res;
            }

            Log.Info(this.GetType().ToString(), "Check sign success");
            return data;
        }

        [HttpPost]
        [Route("notify_url")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ResultNotifyPage()
        {

            Log.Info("notify_url", "触发了");

            WxPayData notifyData = await GetNotifyData();
            //微信支付回调失败
            if (notifyData.GetValue("return_code").ToString() == "FAIL")
            {
                return BadRequest(notifyData.ToXml());
            }

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                return BadRequest(res.ToXml());
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            // if (!QueryOrder(transaction_id))
            if (false)
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                return BadRequest(res.ToXml());
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                string out_trade_no = notifyData.GetValue("out_trade_no").ToString();
                var orderPaied = (int)OrderStatus.Paied;
                Log.Info(this.GetType().ToString(), "获取订单");
                var query = OrderService.GetAll()
                    .Where(x => x.OutTradeNo.Equals(out_trade_no));
                if (query.Any(x => x.OrderStatusId == orderPaied))
                {
                    //订单业务逻辑已经处理了
                    res.SetValue("return_code", "SUCCESS");
                    res.SetValue("return_msg", "OK");
                    Log.Error(this.GetType().ToString(), "订单已经处理过: " + res.ToXml());
                    return Ok(res.ToXml());
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        var order = query.SingleOrDefault();
                        order.OrderStatusId = (int)OrderStatus.Paied;
                        order.PaymentMethodSystemName = notifyData.GetValue("bank_type").ToString();
                        order.PaymentMethodDesction = notifyData.GetValue("openid").ToString() + "|" + transaction_id;
                        order.PaidDate = DateTime.Now;
                        OrderService.Update(order);

                        //当用户不是会员，并且有消费大于500，升级为会员，前端重新获取token
                        if (order.OrderTotal >= 500)
                        {
                            var member = UserManager.FindById(order.CustomerId);
                            if (member.UserRoleId < 2)
                            {
                                member.UserRoleId = 2;
                            }
                            UserManager.Update(member);
                        }
                        res.SetValue("return_code", "SUCCESS");
                        res.SetValue("return_msg", "OK");
                        Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                        scope.Complete();

                        return Ok(res.ToXml());
                    }
                    catch (Exception ex)
                    {
                        res = new WxPayData();
                        res.SetValue("return_code", "FAIL");
                        res.SetValue("return_msg", "订单事物处理失败");
                        Log.Error(this.GetType().ToString(), "订单事物处理失败: " + ex.Message);
                        return BadRequest(res.ToXml());
                    }
                }
            }
        }


        [Route("")]
        public ApiListResult<OrderDTO> Get([FromUri] OrderPageOption option = null)
        {
            var query = OrderService.GetAll()
                    .Include(x => x.ShoppingCartItems)
                    .Include(x => x.Address)
                    .Where(x => !x.Deleted);

            if (!string.IsNullOrEmpty(option.OrderNo))
            {
                query = query.Where(x => x.OutTradeNo.Equals(option.OrderNo));
            }

            if (option.OrderStatusId != 0)
            {
                query = query.Where(x => x.OrderStatusId.Equals(option.OrderStatusId));
            }

            var count = query.Count();

            var orderList = query.OrderByDescending(x => x.CreateTime).Skip((option.Page - 1) * option.Results).Take(option.Results).ToList();

            var results = orderList.Select(x => new OrderDTO()
            {
                AddressId = x.AddressId,
                AddressDTO = new AddressDTO()
                {
                    CityCodeList = x.Address.CityCodeList,
                    Detail = x.Address.Detail,
                    PhoneNumber = x.Address.PhoneNumber,
                    County = x.Address.County,
                    Province = x.Address.Province,
                    Area = x.Address.Area,
                    Name = x.Address.Name
                },
                CreateTime = x.CreateTime,
                OutTradeNo = x.OutTradeNo,
                OrderTotal = x.OrderTotal,
                Id = x.Id,
                PaidDate = x.PaidDate,
                OrderStatusId = x.OrderStatusId,
                WeChatSign = x.WeChatSign,
                TimeSpan = x.TimeSpan,
                NonceStr = x.NonceStr,
                PrePayId = x.PrePayId,
                ShopCartItems = x.ShoppingCartItems.Select(s => new ShoppingCartItemDTO()
                {
                    AttributesXml = s.AttributesXml,
                    ImageUrl = s.ImageUrl,
                    Name = s.Name,
                    UnitPrice = s.UnitPrice,
                    Quantity = s.Quantity,
                    Id = s.Id,
                    Price = s.Price

                }).ToList()

            }).ToList();

            return new ApiListResult<OrderDTO>(results, option.Page - 1, option.Results, count);
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult List(
            int pageIndex = 0,
            int pageSize = 20,
            int status = 0)
        {
            var result = new ApiResult<OrderListModal>();
            try
            {
                var userId = User.Identity.GetUserId();
                var query = OrderService.GetAll()
                    .Include(x => x.ShoppingCartItems)
                    .Where(x => !x.Deleted && x.CustomerId == userId);
                if (status != 0)
                {
                    query = query.Where(x => x.OrderStatusId == status);
                }
                var count = query.Count();
                var orderList = query.OrderByDescending(x => x.CreateTime).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                var modal = new OrderListModal();
                modal.Status = status;
                modal.Orders = orderList.Select(x => new OrderDTO()
                {
                    AddressId = x.AddressId,
                    CreateTime = x.CreateTime,
                    OutTradeNo = x.OutTradeNo,
                    OrderTotal = x.OrderTotal,
                    Id = x.Id,
                    PaidDate = x.PaidDate,
                    OrderStatusId = x.OrderStatusId,
                    WeChatSign = x.WeChatSign,
                    TimeSpan = x.TimeSpan,
                    NonceStr = x.NonceStr,
                    PrePayId = x.PrePayId,
                    ShopCartItems = x.ShoppingCartItems.Select(s => new ShoppingCartItemDTO()
                    {
                        AttributesXml = s.AttributesXml,
                        ImageUrl = s.ImageUrl,
                        Name = s.Name,
                        UnitPrice = s.UnitPrice,
                        Quantity = s.Quantity,
                        Id = s.Id,
                        Price = s.Price

                    }).ToList()
                }).ToList();
                modal.TotalCount = count;
                result.Data = modal;
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult<string>()
                {
                    Code = 1,
                    Info = "获取订单失败！",
                    Data = ex.Message
                });

            }
            result.Info = "获取订单成功！";
            return Ok(result);
        }


        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = new ApiResult<OrderDTO>();
            try
            {
                Order order = await OrderService.GetAll()
                    .Include(x => x.ShoppingCartItems)
                    .Include(x => x.Address)
                    .Where(x => x.Id == id && !x.Deleted)
                    .FirstOrDefaultAsync();

                var model = new OrderDTO()
                {
                    AddressId = order.AddressId,
                    AddressDTO = new AddressDTO()
                    {
                        CityCodeList = order.Address.CityCodeList,
                        Detail = order.Address.Detail,
                        PhoneNumber = order.Address.PhoneNumber,
                        County = order.Address.County,
                        Province = order.Address.Province,
                        Area = order.Address.Area,
                        Name = order.Address.Name
                    },
                    CreateTime = order.CreateTime,
                    OutTradeNo = order.OutTradeNo,
                    OrderTotal = order.OrderTotal,
                    Id = order.Id,
                    PaidDate = order.PaidDate,
                    OrderStatusId = order.OrderStatusId,
                    WeChatSign = order.WeChatSign,
                    TimeSpan = order.TimeSpan,
                    NonceStr = order.NonceStr,
                    PrePayId = order.PrePayId,
                    ShopCartItems = order.ShoppingCartItems.Select(s => new ShoppingCartItemDTO()
                    {
                        AttributesXml = s.AttributesXml,
                        ImageUrl = s.ImageUrl,
                        Name = s.Name,
                        UnitPrice = s.UnitPrice,
                        Quantity = s.Quantity,
                        Id = s.Id,
                        Price = s.Price

                    }).ToList()
                };
                if (order == null)
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 2,
                        Info = "不存在当前订单！"
                    });
                }
                result.Data = model;
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult<string>()
                {
                    Code = 1,
                    Info = ex.Message
                });
            }
            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]OrderDTO OrderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResult<System.Web.Http.ModelBinding.ModelStateDictionary>()
                    {
                        Code = 3,
                        Data = ModelState,
                        Info = "请仔细填写表单！"
                    });
                }

                if (!OrderDto.ShopCartIds.Any())
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 4,
                        Info = "请选择商品然后支付！"
                    });
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    var shopCartItems = ShoppingCartItemService.GetAll().Where(x => OrderDto.ShopCartIds.Contains(x.Id)).ToList();
                    decimal orderTotal = 0;
                    foreach (var item in shopCartItems)
                    {
                        orderTotal += item.UnitPrice * item.Quantity;
                    }

                    WxPayData data = new WxPayData();
                    var orderNo = WxPayApi.GenerateOutTradeNo();
                    data.SetValue("body", "麦呗商城-微信收款");
                    data.SetValue("attach", "麦呗商城");
                    data.SetValue("out_trade_no", orderNo);
                    data.SetValue("total_fee", 1);
                    // data.SetValue("total_fee", Convert.ToInt32(entity.OrderTotal*100));
                    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
                    data.SetValue("trade_type", "APP");

                    WxPayData result = WxPayApi.UnifiedOrder(data);

                    WxPayData appData = new WxPayData();
                    appData.SetValue("prepayid", result.GetValue("prepay_id").ToString());
                    WxPayData appResult = WxPayApi.AppOrder(appData);

                    var entity = OrderDto.ToEntity();
                    entity.PrePayId = result.GetValue("prepay_id").ToString();
                    entity.WeChatSign = appResult.GetValue("sign").ToString();
                    entity.NonceStr = appResult.GetValue("noncestr").ToString();
                    entity.TimeSpan = appResult.GetValue("timestamp").ToString();
                    entity.OrderGuid = Guid.NewGuid();
                    entity.OrderStatus = OrderStatus.NotPay;
                    entity.OrderTotal = orderTotal;
                    entity.OutTradeNo = orderNo;
                    entity.CustomerId = User.Identity.GetUserId();
                    entity.CustomerIp = Request.GetOwinContext().Request.RemoteIpAddress;
                    entity.CreateTime = DateTime.Now;
                    OrderService.Insert(entity);

                    //更新购物车ItemStatus
                    foreach (var shopCart in shopCartItems)
                    {
                        shopCart.OrderId = entity.Id;
                        shopCart.ShoppingCartStatus = ShoppingCartStatus.Order;

                    }

                    ShoppingCartItemService.Update(shopCartItems);

                    scope.Complete();
                    return Ok(new ApiResult<OrderDTO>()
                    {
                        Data = entity.ToModel(),
                        Info = "添加订单成功"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult<string>()
                {
                    Code = 1,
                    Info = ex.Message
                });
            }
        }


        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody]OrderStatusModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await OrderService.FindOneAsync(model.Id);
            entity.OrderStatusId = model.OrderStatusId;
            await OrderService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = new ApiResult<string>();
            var userId = User.Identity.GetUserId();
            var entity = await OrderService
                 .GetAll()
                 .Where(x => x.Id == id
                 && x.CustomerId == userId)
                 .SingleAsync();
            if (entity == null)
            {
                result.Code = 2;
                result.Info = "删除订单，服务器异常！";
                result.Data = "没有权限或该订单不存在";
                return Ok(result);
            }
            result.Data = "删除成功！";
            await OrderService.DeleteAsync(entity);
            return Ok(result);
        }

        [Route("cancel/{id:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Cancel(int id)
        {
            var result = new ApiResult<string>();
            var userId = User.Identity.GetUserId();
            var entity = await OrderService
                 .GetAll()
                 .Where(x => x.Id == id
                 && x.CustomerId == userId)
                 .SingleAsync();
            if (entity == null)
            {
                result.Code = 2;
                result.Info = "取消订单，服务器异常！";
                result.Data = "没有权限或该订单不存在";
                return Ok(result);
            }
            result.Data = "取消成功！";
            entity.OrderStatus = OrderStatus.Cancelled;
            await OrderService.UpdateAsync(entity);
            return Ok(result);
        }


    }
}




