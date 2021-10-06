using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using versaodriver;

namespace BLL.Services
{
    public class WebDriver
    {
        /// <summary>
        /// Initializes the specified kathalon.
        /// </summary>
        /// <param name="kathalon">Se inserir o argumento <c>true</c>, adiciona a extensão do [kathalon] ao webdriver.</param>
        /// <returns>Retorna novo IWebDriver.</returns>
        public static IWebDriver Init(List<string> optionExtras = null)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument(@"load-extension=\\srvirtual\ROBOS\BENNER\GOACT\Chrome\User Data\Default\Extensions\ieelmcmcagommplceebfedjlakkhpden\1.0.6_1");
            options.AddArgument("--start-maximized");
            if (optionExtras != null) foreach (string optionExtra in optionExtras)
                {
                    if (optionExtra == "kathalon") options.AddArgument(@"load-extension=\\srvirtual\ROBOS\Extensions\Katalon\ljdobmomdgdljniojadhoplhkpialdid\5.3.1_0");
                    if (optionExtra == "alert-accept") options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
                }

            //options.AddArgument("headless");

            vdriver versao = new vdriver();

            var chromeDriverService = ChromeDriverService.CreateDefaultService(versao.versaoChromeDriver());
            chromeDriverService.HideCommandPromptWindow = true;
            return new ChromeDriver(chromeDriverService, options);
        }

        /// <summary>
        /// Aguarda carregamento da página.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public static void WaitForLoad(IWebDriver driver)
        {
            Thread.Sleep(1500);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            int timeoutSec = 60;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        /// <summary>
        /// Verifica se o IWebElement fornecido existe. Caso o parâmetro opcional instância de "Processo" seja fornecido, após 10 tentativas é gravada mensagem de erro em seu atributo "Erro", informando qual o elemento que não foi encontrado.
        /// </summary>
        /// <param name="driver">A instância do Selenium</param>
        /// <param name="seletorTipo">O tipo de seletor (XPath, Id, Classname...). É enum do tipo "Seletores", ou seja, o respectivo namespace deve ser fornecido no topo do arquivo, numa declaração "using".</param>
        /// <param name="seletorStr">The seletor string.</param>
        /// <param name="processo">Instância de "Processo".</param>        
        
    }
}