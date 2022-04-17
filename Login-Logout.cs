 Action Method:-


public IActionResult Login(LoginModel obj)
        {

			
            MytestContext database = new MytestContext();

             var res = database.Logintbl.Where(a => a.Email == obj.Email).FirstOrDefault();

         

            if (res == null)
            {

                TempData["Invalid"] = "Email is not found";
            }

            else
            {
                if(res.Email == obj.Email && res.Password == obj.Password)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, res.Name),
                                        new Claim(ClaimTypes.Email, res.Email) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    authProperties);


                   // HttpContext.Session.SetString("Name", obj.Email);

                    return RedirectToAction("Index", "Home");

                }

                else
                {

                    ViewBag.Inv = "Wrong Email Id or password";

                    return View("Login");
                }


            }


            return View("Login");
        }

//==================================================================================================================================================

Logout:-
 public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Login");
        }
		
//====================================================================================================================================================

Startup file:-
public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();

            services.AddControllersWithViews();

            services.AddDbContext<WeekendCoreBatchContext>(options => options.UseSqlServer(

                Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = new PathString("/Home/Login"));
        }
		
//==================================================================================================================================================
		
		
app.UseAuthentication();
