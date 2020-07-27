Escriba
---

Just a asp.net core http request/response logging middleware, that provides a more pretty output to console

**Escriba** (_scribe in pt-br_) is a person who serves as a professional copyist, especially one who made copies of manuscripts before the invention of automatic printing.

The profession of the scribe, previously widespread across cultures, lost most of its prominence and status with the advent of the printing press. The work of scribes can involve copying manuscripts and other texts as well as secretarial and administrative duties such as the taking of dictation and keeping of business, judicial, and historical records. (_Wikipedia definition_)

### How to use: 

Just add the reference to your project and in `Startup.cs`, import it in the `using` section like:

```C#
using Escriba.Core;
```

And then add the following in the Configure method:

```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Add this line...
    app.UseMiddleware<Escriba>();

    app.UseAuthentication();
    app.UseCors("AllowAll");
    app.UseMvc();
}
```