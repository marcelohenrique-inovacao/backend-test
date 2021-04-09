using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Domain.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace backendtest.Domain.Data.SeedDatabase
{
    public class SeedInit
    {

        public static async Task SeedData(DatabaseContext context)
        {
            if (await context.Desenvolvedores.AnyAsync()) return;

            #region DESENVOLVEDOR

            var desenvolvedores = new List<Desenvolvedor>();

            var marcelo = new Desenvolvedor("Marcelo", "08272217627", "marcelo@gmail.com");
            var pedro = new Desenvolvedor("Pedro", "17973534032", "pedro@gmail.com");
            var bruno = new Desenvolvedor("Bruno", "67216720067", "bruno@gmail.com");
            var maria = new Desenvolvedor("Maria", "54931838030", "maria@gmail.com");
            var julia = new Desenvolvedor("Júlia", "97047332081", "julia@gmail.com");
            var barbara = new Desenvolvedor("Bárbara", "69812646094", "barbara@gmail.com");
            var alice = new Desenvolvedor("Alice", "41231744006", "alice@gmail.com");
            var yasmin = new Desenvolvedor("Yasmin", "69245256014", "yasmin@gmail.com");

            desenvolvedores.Add(marcelo);
            desenvolvedores.Add(pedro);
            desenvolvedores.Add(bruno);
            desenvolvedores.Add(maria);
            desenvolvedores.Add(julia);
            desenvolvedores.Add(barbara);
            desenvolvedores.Add(alice);
            desenvolvedores.Add(yasmin);

            context.Desenvolvedores.AddRange(desenvolvedores);

            #endregion

            #region APLICATIVO

            var aplicativos = new List<Aplicativo>();

            var shopee = new Aplicativo("Shopee", DateTime.Parse("2021-05-01"), ETipoPlataforma.Mobile);
            shopee.AdicionarResponsavel(marcelo);

            var google = new Aplicativo("Google", DateTime.Parse("2021-06-05"), ETipoPlataforma.Desktop);
            google.AdicionarResponsavel(pedro);

            var tikTok = new Aplicativo("TikTok", DateTime.Parse("2021-07-07"), ETipoPlataforma.Mobile);
            tikTok.AdicionarResponsavel(bruno);

            var picPay = new Aplicativo("PicPay", DateTime.Parse("2021-08-09"), ETipoPlataforma.Web);
            picPay.AdicionarResponsavel(maria);

            var nuBank = new Aplicativo("NuBank", DateTime.Parse("2021-09-15"), ETipoPlataforma.Mobile);
            var whatsApp = new Aplicativo("WhatsApp", DateTime.Parse("2021-10-20"), ETipoPlataforma.Web);
            var instagram = new Aplicativo("Instagram", DateTime.Parse("2021-11-25"), ETipoPlataforma.Mobile);
            var iFood = new Aplicativo("IFood", DateTime.Parse("2021-12-30"), ETipoPlataforma.Desktop);

            aplicativos.Add(shopee);
            aplicativos.Add(google);
            aplicativos.Add(tikTok);
            aplicativos.Add(picPay);
            aplicativos.Add(nuBank);
            aplicativos.Add(whatsApp);
            aplicativos.Add(instagram);
            aplicativos.Add(iFood);

            context.Aplicativos.AddRange(aplicativos);


            #endregion

            #region DESENVOLVEDORAPLICATIVO

            var desenvolvedoresAplicativos = new List<DesenvolvedorAplicativo>
            {
                //Shopee
                new DesenvolvedorAplicativo(shopee, yasmin),
                new DesenvolvedorAplicativo(shopee, alice),
                new DesenvolvedorAplicativo(shopee, barbara),
                new DesenvolvedorAplicativo(shopee, julia),

                //Google
                new DesenvolvedorAplicativo(google, yasmin),
                new DesenvolvedorAplicativo(google, bruno),
                new DesenvolvedorAplicativo(google, marcelo),

                //TikTok
                new DesenvolvedorAplicativo(tikTok, yasmin),
                new DesenvolvedorAplicativo(tikTok, pedro),

                //PicPay
                new DesenvolvedorAplicativo(picPay, barbara),
                new DesenvolvedorAplicativo(picPay, maria),
                new DesenvolvedorAplicativo(picPay, julia),
                new DesenvolvedorAplicativo(picPay, marcelo),

                //NuBank
                new DesenvolvedorAplicativo(nuBank, alice),
                new DesenvolvedorAplicativo(nuBank, marcelo),

            };

            context.DesenvolvedorAplicativo.AddRange(desenvolvedoresAplicativos);

            #endregion

            await context.SaveChangesAsync();
        }
    }
}