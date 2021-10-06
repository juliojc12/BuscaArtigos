using BLL.Services;
using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class Busca
    {

        private readonly string CAFE_PASSWORD = ConfigurationManager.AppSettings["CAFE_SENHA"];
        private readonly string CAFE_USERNAME = ConfigurationManager.AppSettings["CAFE_LOGIN"];

        public void Executar(string termo1, string termo2)
        {
            using (ChromeDriver driver = (ChromeDriver)WebDriver.Init())
            {
                string ifrj = "IFRJ - INSTITUTO FEDERAL DO RIO DE JANEIRO";
                driver.Navigate().GoToUrl("https://www-periodicos-capes-gov-br.ezl.periodicos.capes.gov.br/index.php?option=com_plogin");
                Thread.Sleep(3000);

                bool modal = driver.FindElement(By.ClassName("modalIndex")).Displayed;

                while (modal)
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/footer/div[2]/div[2]/div/div/div[3]/button")).Click();
                    break;
                }




                try
                {


                    driver.FindElement(By.Id("portal-siteactions")).FindElements(By.TagName("li"))[0].Click();

                    //driver.FindElement(By.XPath("//*[@id='portal - siteactions']/li[1]/a//*[@id='portal - siteactions']/li[1]/a")).Click();
                    WebDriver.WaitForLoad(driver);



                    driver.FindElement(By.Id("listaInstituicoesCafe_chosen")).Click();
                    driver.FindElement(By.XPath("//*[@id='listaInstituicoesCafe_chosen']/div/div/input")).SendKeys(ifrj + Keys.Enter);
                    driver.FindElement(By.Id("enviarInstituicaoCafe")).Click();
                    WebDriver.WaitForLoad(driver);



                    driver.FindElement(By.Id("username")).SendKeys(CAFE_USERNAME);
                    driver.FindElement(By.Name("donotcache")).Click();
                    driver.FindElement(By.Id("password")).SendKeys(CAFE_PASSWORD + Keys.Enter);

                    WebDriver.WaitForLoad(driver);




                    driver.FindElement(By.Name("_eventId_proceed")).Click();

                    WebDriver.WaitForLoad(driver);




                    driver.FindElement(By.XPath("//*[@id='assunto']/a")).Click();
                    //WebDriver.WaitForLoad(driver);


                    driver.Navigate().GoToUrl("https://capes-primo.ez140.periodicos.capes.gov.br/primo_library/libweb/action/search.do?&vid=CAPES_V1&mode=Advanced&vl(freeText0)=");


                    driver.FindElement(By.Id("input_freeText0")).SendKeys(termo1);
                    driver.FindElement(By.Id("input_freeText1")).SendKeys(termo2);
                    new SelectElement(driver.FindElement(By.Id("exlidInput_publicationDate_"))).SelectByValue("5-YEAR");
                    driver.FindElement(By.Id("goButton")).Click();
                    WebDriver.WaitForLoad(driver);






                    //XLWorkbook wb = new XLWorkbook($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Downloads\Teste.xlsx");

                    List<IWebElement> listaTitulos = driver.FindElements(By.ClassName("EXLResultTitle")).ToList();
                    bool elementoDisplayed = driver.FindElement(By.ClassName("EXLBriefResultsPaginationLinkNext")).Displayed;
                    foreach (var item in listaTitulos)
                    {
                        if (elementoDisplayed)
                        {
                            using (StreamWriter SW = new StreamWriter($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Downloads\Saida.txt", true))
                            {
                                SW.WriteLine(item.Text);
                            }

                            try { driver.FindElement(By.ClassName("EXLBriefResultsPaginationLinkNext")).Click(); WebDriver.WaitForLoad(driver); }
                            catch (Exception) { }
                           
                        }
                    }


                    //CRIA LOOGICA PARA PEGAR O TEXTO DAS TAGS 'A'
                    //E ESCREVER NO EXCEL


                }
                catch (Exception)
                {

                }


            }
        }



    }
}
