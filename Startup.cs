using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcoreapp
{
    public class Startup
    {

        
        private void Init() {

            int data_version = 18;

            //await Voc.InitAsync();
            
            if (!SaveAndLoad.FileExists("setting.txt"))
                SaveAndLoad.SaveText("setting.txt", "");

            Voc.setting = Voc.GetDoc("setting", false);

            if (!Voc.setting.exists("website"))
                Voc.setting.add("website", "http://joe59491.azurewebsites.net");
            if (!Voc.setting.exists("sound_url"))
                Voc.setting.add("sound_url", "http://dictionary.reference.com/browse/");
            if (!Voc.setting.exists("sound_url2"))
                Voc.setting.add("sound_url2", "http://static.sfdict.com/staticrep/dictaudio");
            if (!Voc.setting.exists("sound_type"))
                Voc.setting.add("sound_type", ".mp3");
            if (!Voc.setting.exists("data_version"))
                Voc.setting.add("data_version", "0");
            Voc.SavingSetting();

            if (!SaveAndLoad.FileExists("favorite.txt"))
                SaveAndLoad.SaveText("favorite.txt", "");
            Voc.favorite = Voc.GetDoc("favorite", false);
            if (!SaveAndLoad.FileExists("words.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("words.txt");
            if (!SaveAndLoad.FileExists("root.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("root.txt");
            if (!SaveAndLoad.FileExists("prefix.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("prefix.txt");
            if (!SaveAndLoad.FileExists("suffix.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("suffix.txt");
            if (!SaveAndLoad.FileExists("note.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("note.txt");
            if (int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.setting["data_version"] = data_version.ToString();

            Voc.words =Voc.GetDoc("words", true);
            var tmp = Voc.words.val("apple");
            foreach (var x in Voc.words.data)
            {
                var y = x;
            }

            Voc.root = Voc.GetDoc("root", true);

            Voc.prefix = Voc.GetDoc("prefix", true);

            Voc.suffix = Voc.GetDoc("suffix", true);

            Voc.note = Voc.GetDoc("note", true);
        }
        public Startup(IConfiguration configuration)
        {
            
            Init();
            Voc.inited = true;
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
