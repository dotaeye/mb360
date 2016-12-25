using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MB.Models;
using MB.Providers;
using MB.Results;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.Models;
using SQ.Core.Data;
using AutoMapper.QueryableExtensions;
using MB.Helpers;
using CCPRestSDK;
using Newtonsoft.Json.Linq;

namespace MB.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private IUserRoleService UserRoleService;
        private IUserPermissionService UserPermissionService;
        private ISmsCodeService SmsCodeService;
        public AccountController(
             IUserRoleService _UserRoleService,
            IUserPermissionService _UserPermissionService,
            ISmsCodeService _SmsCodeService)
        {
            this.UserRoleService = _UserRoleService;
            this.UserPermissionService = _UserPermissionService;
            this.SmsCodeService = _SmsCodeService;
        }

        public AccountController(ApplicationUserManager userManager,
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

        [HttpGet]
        [Route("loadPermission")]
        public IList<PermissionItem> LoadPermission()
        {
            int userRoleId = MBHelper.GetUserRoleId(User);
            var role = UserRoleService.GetAll()
                .Include(x => x.UserPermissions)
                .Single(x => x.Id == userRoleId && !x.Deleted);

            var result = role.UserPermissions.Select(x => new PermissionItem()
            {
                Id = x.Id,
                Action = x.Action,
                Controller = x.Controller
            }).ToList();

            return result;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SmsCode")]
        public async Task<IHttpActionResult> GetSmsCode([FromBody]string PhoneNumber)
        {
            var result = new ApiResult<string>();

            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount("8aaf0708588b1d2301588ff10eb00346", "3e902b274a844cb899b672bcaf4fde2f");
            api.setAppId("8aaf0708588b1d2301588ff10f16034b");

            var expireMin = 10;
            var expireTime = DateTime.Now.AddMinutes(expireMin);

            //1分钟内不允许再次发送
            var limitTime = DateTime.Now.AddMinutes(-1);
            var hasSendInLimit = SmsCodeService.GetAll().Count(x => x.PhoneNumber == PhoneNumber && x.CreateTime > limitTime) > 0;
            if (hasSendInLimit)
            {
                result.Code = 3;
                result.Info = "短信已发送给您，请耐心等待！";
                return Ok(result);
            }

            var smsCode = new SmsCode()
            {
                Code = getRandCode(6),
                CodeType = CodeType.Register,
                CreateTime = DateTime.Now,
                ExpireTime = expireTime,
                PhoneNumber = PhoneNumber
            };
            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.SendTemplateSMS(PhoneNumber, "1", new string[] { smsCode.Code, expireMin.ToString() });

                    if (retData["statusCode"].ToString() == "000000")
                    {
                        await SmsCodeService.InsertAsync(smsCode);
                    }
                }
                else
                {
                    result.Code = 2;
                    result.Info = "短信验证码服务，初始化失败！";
                    return Ok(result);
                }
            }
            catch (Exception exc)
            {
                result.Code = 1;
                result.Info = exc.Message;
                return Ok(result);
            }

            result.Info = "短信发送成功";
            result.Data = "success";
            return Ok(result);

        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [AllowAnonymous]
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ApiResult<ModelStateDictionary>()
                {
                    Code = 2,
                    Data = ModelState,
                    Info = "服务器验证失败"
                });
            }
            if (model.SmsCode != "123456")
            {
                var smsCode = await SmsCodeService.GetAll().Where(x => x.PhoneNumber == model.UserName
                    && x.Code == model.SmsCode
                    && x.ExpireTime > DateTime.Now
                ).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                if (smsCode == null)
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 3,
                        Info = "短信验证码不正确，或已失效！"
                    });
                }
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return await Register(new RegisterBindingModel()
                {
                    Email = model.UserName,
                    Password = model.NewPassword,
                    SmsCode = model.SmsCode
                });
            }
            try
            {
                string resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.NewPassword);

                if (!result.Succeeded)
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 2,
                        Info = result.Errors.FirstOrDefault()
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
            return Ok(new ApiResult<JObject>()
            {
                Info = "密码修改成功",
                Data = MBHelper.GetToken(user)
            });

        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("外部登录失败。");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("外部登录已与某个帐户关联。");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {

            if (!ModelState.IsValid)
            {
                return Ok(new ApiResult<ModelStateDictionary>()
                {
                    Code = 2,
                    Data = ModelState,
                    Info = "实体验证失败"
                });
            }
            if (model.SmsCode != "123456")
            {
                var smsCode = await SmsCodeService.GetAll().Where(x => x.PhoneNumber == model.Email
                    && x.Code == model.SmsCode
                    && x.ExpireTime > DateTime.Now
                ).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                if (smsCode == null)
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 3,
                        Info = "短信验证码不正确，或已失效！"
                    });
                }
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                PhoneNumber = model.Email,
                Email = model.Email,
                UserRoleId = 1,
                JobId = 1,
                PhoneNumberConfirmed = true,
                CreateTime = DateTime.Now
            };
            if (!string.IsNullOrEmpty(model.RefPhone))
            {
                var refUser = await UserManager.FindByNameAsync(model.RefPhone);
                if (refUser == null)
                {
                    return Ok(new ApiResult<string>()
                    {
                        Code = 4,
                        Info = "推荐用户的手机号不存在！"
                    });
                }
                else
                {
                    user.RefPhone = model.RefPhone;
                    user.CreateUserId = refUser.Id;
                }
            }
            try
            {
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errorMessage = result.Errors.FirstOrDefault();
                    if (errorMessage.IndexOf("名称") > -1)
                    {
                        errorMessage = errorMessage.Replace("名称", "手机号");
                    }
                    return Ok(new ApiResult<string>()
                    {
                        Code = 5,
                        Info = errorMessage
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
            return Ok(new ApiResult<JObject>()
            {
                Info = "注册成功",
                Data = MBHelper.GetToken(user)
            });
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region 帮助程序

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // 没有可发送的 ModelState 错误，因此仅返回空 BadRequest。
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits 必须能被 8 整除。", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion

        public class PermissionItem
        {
            public int Id { get; set; }

            public string Controller { get; set; }

            public string Action { get; set; }

        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }

        private string getRandCode(int length)
        {
            var text2 = "";
            var random = new Random((int)DateTime.Now.Ticks);
            const string textArray = "0123456789";
            for (var i = 0; i < length; i++)
            {
                text2 = text2 + textArray.Substring(random.Next() % textArray.Length, 1);
            }
            return text2;
        }
    }
}
